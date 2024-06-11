import { Component } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { CatalogService } from '../../services/catalog.service';
import { Title } from '../../models/catalog-item.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-item-details',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule
  ],
  templateUrl: './item-details.component.html',
  styleUrl: './item-details.component.scss'
})
export class ItemDetailsComponent {
  item: Title | null = null;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private catalogService: CatalogService) { }

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      const id = params.get('id');

      if (id) {
        this.catalogService.getById(id).subscribe(
          (data) => {
            this.item = data;
            this.loadThumbnail(this.item);
          }
        );
      }
    });
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

  play(episodeId: number) {
    if (this.item) {
      this.router.navigate(['/watch', this.item.id, episodeId]);
    }
  }
}