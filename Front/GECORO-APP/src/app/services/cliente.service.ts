import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Cliente } from '../models/Cliente';

@Injectable()
export class ClienteService {

  private baseURL: string = 'https://localhost:5001/api/cliente';
  constructor(private http: HttpClient) { }

  public getClientes(): Observable<Cliente[]>{
    return this.http.get<Cliente[]>(this.baseURL);
  }

  public getClientesByNome(nome: string): Observable<Cliente[]>{
    return this.http.get<Cliente[]>(`${this.baseURL}/nome/${nome}`);
  }

  public getClientesByCPF(CPF: string): Observable<Cliente>{
    return this.http.get<Cliente>(`${this.baseURL}/CPF/${CPF}`);
  }

  public getClienteById(id: number): Observable<Cliente>{
    return this.http.get<Cliente>(`${this.baseURL}/${id}`);
  }
}
