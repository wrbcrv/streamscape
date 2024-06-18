import { Component, OnDestroy, OnInit } from '@angular/core';
import { CatalogService } from '../../services/catalog.service';
import { Title } from '../../models/catalog-item.model';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-catalog-list',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule
  ],
  templateUrl: './catalog-list.component.html',
  styleUrl: './catalog-list.component.scss'
})
export class CatalogListComponent implements OnInit, OnDestroy {
  items: Title[] = [];
  index: number = 0;
  interval: any;
  url: string | undefined;

  constructor(
    private catalogService: CatalogService
  ) { }

  ngOnInit(): void {
    this.catalogService.getAll().subscribe(
      (res) => {
        this.items = res;
        this.items.forEach(item => this.loadThumbnail(item));
        this.slide();
      },
      (err) => {
        console.log(err);
      }
    );
  }

  ngOnDestroy(): void {
    if (this.interval) {
      clearInterval(this.interval);
    }
  }

  loadThumbnail(item: Title): void {
    this.catalogService.thumbnail(item.id.toString()).subscribe(
      (res) => {
        const url = window.URL.createObjectURL(res);
        item.thumbnail = url;
      },
      (err) => {
        console.log(err);
      }
    )
  }

  next(): void {
    this.index = (this.index + 1) % this.items.length;
  }

  prev(): void {
    this.index = (this.index - 1 + this.items.length) % this.items.length;
  }

  slide(): void {
    this.interval = setInterval(() => {
      this.next();
    }, 10000);
  }

  trackByFn(index: number, item: Title): number {
    return item.id;
  }
}