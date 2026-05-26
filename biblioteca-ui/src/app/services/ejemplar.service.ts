import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Ejemplar } from '../models/ejemplar.model';

@Injectable({
  providedIn: 'root',
})
export class EjemplarService {
  private apiUrl = 'https://localhost:7268/api/ejemplares';

  constructor(private http: HttpClient) { }

  listarEjemplares(): Observable<Ejemplar[]> {
    return this.http.get<Ejemplar[]>(this.apiUrl);
  }

  insertarEjemplar(ejemplar: Ejemplar): Observable<Ejemplar> {
    return this.http.post<Ejemplar>(this.apiUrl, ejemplar);
  }

  actualizarEjemplar(ejemplar: Ejemplar): Observable<void>{
    return this.http.put<void>(`${this.apiUrl}/${ejemplar.ejemplarId}`, ejemplar);
  }

  eliminarEjemplar(id: number): Observable<void>{
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}