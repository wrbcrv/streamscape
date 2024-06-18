import { Component, OnInit } from '@angular/core';
import { User } from '../../../models/user.model';
import { AuthService } from '../../../services/auth.service';
import { MyList } from '../../../models/my-list.model';
import { CatalogService } from '../../../services/catalog.service';
import { DomSanitizer } from '@angular/platform-browser';
import { UserService } from '../../../services/user.service';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-my-list',
  standalone: true,
  imports: [
    RouterModule
  ],
  templateUrl: './my-list.component.html',
  styleUrl: './my-list.component.scss'
})
export class MyListComponent implements OnInit {
  user: User | null = null;

  constructor(
    private authService: AuthService,
    private catalogService: CatalogService,
    private sanitizer: DomSanitizer,
    private userService: UserService
  ) { }

  ngOnInit(): void {
    this.authService.me().subscribe(
      (res) => {
        this.user = res;

        if (this.user?.myList) {
          this.user.myList.forEach(item => {
            this.loadImage(item);
          });
        }
      },
      (err) => {
        throw new Error(err);
      }
    );
  }

  loadImage(item: MyList): void {
    const tid = item.id.toString();

    this.catalogService.thumbnail(tid).subscribe(
      (blb) => {
        const objectURL = URL.createObjectURL(blb);
        item.thumbnail = this.sanitizer.bypassSecurityTrustUrl(objectURL) as string;
      },
      (err) => {
        console.error(err);
      }
    );
  }

  removeItem(item: MyList): void {
    if (this.user && this.user.id) {
      this.userService.removeFromMyList(this.user.id, item.id).subscribe(
        (res) => {
          if (this.user && this.user.myList) {
            const index = this.user.myList.indexOf(item);
            
            if (index > -1) {
              this.user.myList.splice(index, 1);
            }
          }
        },
        (err) => {
          console.error(err);
        }
      );
    }
  }
}