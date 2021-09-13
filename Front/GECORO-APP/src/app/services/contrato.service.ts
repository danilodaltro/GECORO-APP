import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Contrato } from '../models/Contrato';
import { take } from 'rxjs/operators'

@Injectable()
export class ContratoService {

  private baseURL: string = 'https://localhost:5001/api/contrato';
  constructor(private http: HttpClient) { }

  public getContratos(): Observable<Contrato[]>{
    return this.http.get<Contrato[]>(this.baseURL).pipe(take(1));
  }

  public getContratoByNumero(numero: string): Observable<Contrato>{
    return this.http.get<Contrato>(`${this.baseURL}/numero/${numero}`).pipe(take(1));
  }

  public getContratosByCliente(clienteId: number): Observable<Contrato[]>{
    return this.http.get<Contrato[]>(`${this.baseURL}/cliente/${clienteId}`).pipe(take(1));
  }

  public getContratosByVendedor(vendedorId: number): Observable<Contrato[]>{
    return this.http.get<Contrato[]>(`${this.baseURL}/vendedor/${vendedorId}`).pipe(take(1));
  }

  public getContratoById(id: number): Observable<Contrato>{
    return this.http.get<Contrato>(`${this.baseURL}/${id}`).pipe(take(1));
  }

  public put(contrato: Contrato): Observable<Contrato>{
    return this.http.put<Contrato>(`${this.baseURL}/${contrato.id}`, contrato).pipe(take(1));
  }

  public post(contrato: Contrato): Observable<Contrato>{
    return this.http.post<Contrato>(this.baseURL, contrato).pipe(take(1));
  }

  public delete(id: number): Observable<any>{
    return this.http.delete(`${this.baseURL}/${id}`).pipe(take(1));
  }
}
