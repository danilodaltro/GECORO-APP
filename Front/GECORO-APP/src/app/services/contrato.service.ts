import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Contrato } from '../models/Contrato';

@Injectable()
export class ContratoService {

  private baseURL: string = 'https://localhost:5001/api/contrato';
  constructor(private http: HttpClient) { }

  public getContratos(): Observable<Contrato[]>{
    return this.http.get<Contrato[]>(this.baseURL);
  }

  public getContratoByCodigo(numero: string): Observable<Contrato>{
    return this.http.get<Contrato>(`${this.baseURL}/numero/${numero}`);
  }

  public getContratosByCliente(clienteId: number): Observable<Contrato[]>{
    return this.http.get<Contrato[]>(`${this.baseURL}/cliente/${clienteId}`);
  }

  public getContratoById(id: number): Observable<Contrato>{
    return this.http.get<Contrato>(`${this.baseURL}/${id}`);
  }
}
