import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { CatalogService } from '../../services/catalog.service';
import { Title } from '../../models/catalog-item.model';
import { CommonModule } from '@angular/common';
import { UserService } from '../../services/user.service';
import { AuthService } from '../../services/auth.service';
import { User } from '../../models/user.model';

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
  private timeoutId: any = null;

  @ViewChild('container') container!: ElementRef<HTMLDivElement>;

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
            console.log(this.item.episodes.length)
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

        if (this.timeoutId)
          clearTimeout(this.timeoutId);

        this.timeoutId = setTimeout(() => {
          this.message = null;
        }, 3000);
      },
      (err) => {
        console.log(err);
      }
    );
  }

  scroll(amount: number) {
    this.container.nativeElement.scrollBy({ left: amount, behavior: 'smooth' })
  }
}