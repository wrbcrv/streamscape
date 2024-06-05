import { Component, OnInit } from '@angular/core';
import { CatalogService } from '../../services/catalog.service';
import { Title } from '../../models/catalog-item.model';

@Component({
  selector: 'app-catalog-list',
  standalone: true,
  imports: [],
  templateUrl: './catalog-list.component.html',
  styleUrl: './catalog-list.component.scss'
})
export class CatalogListComponent implements OnInit {
  items: Title[] = [];

  constructor(
    private catalogService: CatalogService
  ) { }

  ngOnInit(): void {
    this.catalogService.getAll().subscribe(
      (res) => {
        this.items = res;
      },
      (err) => {
        
      }
    );
  }
}