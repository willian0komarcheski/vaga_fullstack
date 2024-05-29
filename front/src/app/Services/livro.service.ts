import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Livro } from '../Models/livro';

@Injectable({
  providedIn: 'root'
})
export class LivroService {

  private apiUrl = `${environment.apiUrl}Livro`;

  constructor(private http : HttpClient) {}

  GetLivro() : Observable<Livro[]> {
    return this.http.get<Livro[]>(this.apiUrl);
  }

  GetLivroId(id : number) : Observable<Livro> {
    return this.http.get<Livro>(`${this.apiUrl}/${id}`);
  }

  CreateLivro(Livro : Livro) : Observable<Livro[]> {
    return this.http.post<Livro[]>(`${this.apiUrl}`, Livro);
  }

  DeleteLivro(id: number): Observable<void> {
    const url = `${this.apiUrl}/${id}`;
    return this.http.delete<void>(url.replace(/\s/g, ""));
  }

  EditLivro(livro: Livro, id : number): Observable<Livro> {
    const url = `${this.apiUrl}/${id}`;
    return this.http.put<Livro>(url, livro);
  }
}
