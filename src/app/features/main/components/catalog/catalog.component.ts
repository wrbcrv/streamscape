import { Component, OnInit } from '@angular/core';
import { TituloService } from '../../services/titulo.service';
import { Titulo } from '../../../../core/models/titulo.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-catalog',
  standalone: true,
  imports: [
    CommonModule
  ],
  templateUrl: './catalog.component.html',
  styleUrl: './catalog.component.scss'
})
export class CatalogComponent implements OnInit {
  titulos: Titulo[] = [];
  image: any = {};

  constructor(private tituloService: TituloService) { }

  ngOnInit(): void {
    this.loadCatalog();
  }

  loadCatalog(): void {
    this.tituloService.getAll().subscribe((res) => {
      this.titulos = res;
      this.titulos.forEach(titulo => {
        this.downloadImageForTitulo(titulo.id);
      });
    });
  }

  downloadImageForTitulo(tituloId: number): void {
    this.tituloService.downloadImage(tituloId).subscribe(data => {
      this.createImageFromBlob(data, tituloId);
    });
  }

  createImageFromBlob(image: Blob, tituloId: number) {
    let reader = new FileReader();

    reader.addEventListener("load", () => {
      this.image[tituloId] = reader.result;
    }, false);

    if (image) {
      reader.readAsDataURL(image);
    }
  }
}