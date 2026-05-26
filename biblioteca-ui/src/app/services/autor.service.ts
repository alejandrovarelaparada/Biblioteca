import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Autor } from '../models/autor.model';

@Injectable({
  providedIn: 'root',
})
export class AutorService {
  private apiUrl = 'https://localhost:7268/api/autores';

  constructor(private http: HttpClient) { }

  listarAutores(): Observable<Autor[]> {
    return this.http.get<Autor[]>(this.apiUrl);
  }

  insertarAutor(autor: Autor): Observable<Autor> {
    return this.http.post<Autor>(this.apiUrl, autor);
  }

  actualizarAutor(autor: Autor): Observable<void>{
    return this.http.put<void>(`${this.apiUrl}/${autor.autorId}`, autor);
  }

  eliminarAutor(id: number): Observable<void>{
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}