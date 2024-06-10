import { Component, OnInit } from '@angular/core';
import { CatalogService } from '../../services/catalog.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-player',
  standalone: true,
  imports: [],
  templateUrl: './player.component.html',
  styleUrl: './player.component.scss'
})
export class PlayerComponent implements OnInit {
  url: string | undefined;
  tid: string | null = null;
  eid: string | null = null;

  constructor(
    private acivatedRoute: ActivatedRoute,
    private catalogService: CatalogService
  ) { }

  ngOnInit(): void {
    this.tid = this.acivatedRoute.snapshot.paramMap.get('tid');
    this.eid = this.acivatedRoute.snapshot.paramMap.get('eid');
    this.download();
  }

  download(): void {
    if (this.tid && this.eid) {
      this.catalogService.download(this.tid, this.eid).subscribe(
        (res) => {
          const url = window.URL.createObjectURL(res);
          this.url = url;
        },
        (err) => {
          console.log(err);
        }
      );
    }
  }
}