import { Component } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { CatalogService } from '../../services/catalog.service';
import { Title } from '../../models/catalog-item.model';

@Component({
  selector: 'app-item-details',
  standalone: true,
  imports: [
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
            console.log(this.item);
          }
        );
      }
    });
  }

  play(episodeId: number) {
    if (this.item) {
      this.router.navigate(['/watch', this.item.id, episodeId]);
    }
  }
}