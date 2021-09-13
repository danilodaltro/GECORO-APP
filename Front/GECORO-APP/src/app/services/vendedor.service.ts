import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Vendedor } from '../models/Vendedor';

@Injectable()
export class VendedorService {

  private baseURL: string = 'https://localhost:5001/api/vendedores';
  constructor(private http: HttpClient) { }

  public Vendedores(): Observable<Vendedor[]>{
    return this.http.get<Vendedor[]>(this.baseURL);
  }

  public VendedoresByNome(nome: string): Observable<Vendedor[]>{
    return this.http.get<Vendedor[]>(`${this.baseURL}/nome/${nome}`);
  }

  public VendedoresByCodigo(codigo: string): Observable<Vendedor>{
    return this.http.get<Vendedor>(`${this.baseURL}/codigo/${codigo}`);
  }

  public getVendedorById(id: number): Observable<Vendedor>{
    return this.http.get<Vendedor>(`${this.baseURL}/${id}`);
  }

}
