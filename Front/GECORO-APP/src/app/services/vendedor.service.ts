import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Vendedor } from '../models/Vendedor';
import { take } from 'rxjs/operators'

@Injectable()
export class VendedorService {

  private baseURL: string = 'https://localhost:5001/api/vendedor';
  constructor(private http: HttpClient) { }

  public getVendedores(): Observable<Vendedor[]>{
    return this.http.get<Vendedor[]>(this.baseURL);
  }

  public getVendedoresByNome(nome: string): Observable<Vendedor[]>{
    return this.http.get<Vendedor[]>(`${this.baseURL}/nome/${nome}`).pipe(take(1));
  }

  public getVendedoresByCodigo(codigo: string): Observable<Vendedor>{
    return this.http.get<Vendedor>(`${this.baseURL}/codigo/${codigo}`).pipe(take(1));
  }

  public getVendedorById(id: number): Observable<Vendedor>{
    return this.http.get<Vendedor>(`${this.baseURL}/${id}`).pipe(take(1));
  }

  public put(vendedor: Vendedor): Observable<Vendedor>{
    return this.http.put<Vendedor>(`${this.baseURL}/${vendedor.id}`, vendedor).pipe(take(1));
  }

  public post(vendedor: Vendedor): Observable<Vendedor>{
    return this.http.post<Vendedor>(this.baseURL, vendedor).pipe(take(1));
  }

  public delete(id: number): Observable<any>{
    return this.http.delete(`${this.baseURL}/${id}`).pipe(take(1));
  }
}
