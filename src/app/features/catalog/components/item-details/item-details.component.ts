import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { CatalogService } from '../../services/catalog.service';
import { Title } from '../../models/catalog-item.model';
import { CommonModule } from '@angular/common';
import { UserService } from '../../../../core/services/user.service';
import { AuthService } from '../../../../core/services/auth.service';
import { User } from '../../../../core/models/user.model';

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
export class ItemDetailsComponent implements OnInit {
  item: Title | null = null;
  user: User | null = null;
  message: string | null = null;

  constructor(
    private authService: AuthService,
    private route: ActivatedRoute,
    private router: Router,
    private catalogService: CatalogService,
    private userService: UserService) { }

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

    this.authService.me().subscribe(
      (res) => {
        this.user = res;
      },
      (err) => {
      }
    )
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

  addToMyList(uid: number, tid: number): void {
    this.userService.addToMyList(uid, tid).subscribe(
      (res) => {
        this.message = res.message;
        setTimeout(() => {
          this.message = null;
        }, 3000);
      },
      (err) => {
        console.log(err);
      }
    )
  }
}