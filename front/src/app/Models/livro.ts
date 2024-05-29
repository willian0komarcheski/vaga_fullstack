import { Categoria } from "../Models/categoria";

export interface Livro {
    Id: number;
    Nome: string;
    Preco: number;
    Faixaetaria: string;
    categorias: Categoria[];
  }
  