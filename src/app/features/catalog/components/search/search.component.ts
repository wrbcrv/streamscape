import { AfterViewInit, Component, ElementRef, EventEmitter, HostListener, OnInit, Output, ViewChild } from '@angular/core';
import { CatalogService } from '../../services/catalog.service';
import { Title } from '../../models/catalog-item.model';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-search',
  standalone: true,
  imports: [
    RouterModule
  ],
  templateUrl: './search.component.html',
  styleUrl: './search.component.scss'
})
export class SearchComponent implements AfterViewInit {
  @Output() closeSearch = new EventEmitter<void>();
  @ViewChild('input') input!: ElementRef<HTMLInputElement>;

  results: Title[] = [];
  search: string = '';

  constructor(
    private catalogService: CatalogService
  ) { }

  @HostListener('document:keydown.escape', ['$event'])
  handleEscape(event: KeyboardEvent) {
    this.close();
  }

  ngAfterViewInit() {
    this.input.nativeElement.focus();
  }

  close() {
    this.closeSearch.emit();
  }

  onSearch(query: string): void {
    if (query.trim().length > 0) {
      this.search = query;
      this.catalogService.search(query).subscribe(
        (res: Title[]) => {
          this.results = res;
          this.results.forEach(item => this.loadThumbnail(item));
        },
        (err) => {
          console.error(err);
        }
      );
    } else {
      this.results = [];
      this.search = '';
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
    );
  }
}