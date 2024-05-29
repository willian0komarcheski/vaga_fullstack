import { ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';
import { LivroService } from '../../Services/livro.service';
import { Livro } from '../../Models/livro';
import { CommonModule, NgFor } from '@angular/common';

@Component({
  selector: 'app-inicializar',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './inicializar.component.html',
  styleUrl: './inicializar.component.css'
})
export class InicializarComponent implements OnInit{
  teste: string = "vai tomar no cu";
  livros?: Livro[] = [];
  livrosGeral: Livro[] = [];

  constructor(private livroservice : LivroService, private cdr: ChangeDetectorRef) { 
    // this.carregarLivros();
    // console.log(this.livros);
  }
  
  
  ngOnInit(): void {
    this.carregarLivros();
    console.log(this.livros)
  }


  carregarLivros(): void {
    this.livroservice.GetLivro().subscribe((data) => {
      this.livros = data;
      this.livrosGeral = data;
      this.cdr.detectChanges(); 
    });
  }

}
