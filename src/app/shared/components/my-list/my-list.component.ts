import { Component, OnInit } from '@angular/core';
import { User } from '../../../models/user.model';
import { AuthService } from '../../../services/auth.service';
import { MyList } from '../../../models/my-list.model';
import { CatalogService } from '../../../services/catalog.service';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-my-list',
  standalone: true,
  imports: [],
  templateUrl: './my-list.component.html',
  styleUrl: './my-list.component.scss'
})
export class MyListComponent implements OnInit {
  user: User | null = null;

  constructor(
    private authService: AuthService,
    private catalogService: CatalogService,
    private sanitizer: DomSanitizer
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
}