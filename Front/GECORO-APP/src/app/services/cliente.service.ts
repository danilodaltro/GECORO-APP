import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Cliente } from '../models/Cliente';
import { take } from 'rxjs/operators'

@Injectable()
export class ClienteService {

  private baseURL: string = 'https://localhost:5001/api/cliente';
  constructor(private http: HttpClient) { }

  public getClientes(): Observable<Cliente[]>{
    return this.http.get<Cliente[]>(this.baseURL).pipe(take(1));
  }

  public getClientesByNome(nome: string): Observable<Cliente[]>{
    return this.http.get<Cliente[]>(`${this.baseURL}/nome/${nome}`).pipe(take(1));
  }

  public getClientesByCPF(CPF: string): Observable<Cliente>{
    return this.http.get<Cliente>(`${this.baseURL}/CPF/${CPF}`).pipe(take(1));
  }

  public getClienteById(id: number): Observable<Cliente>{
    return this.http.get<Cliente>(`${this.baseURL}/${id}`).pipe(take(1));
  }

  public getClienteByVendedor(vendedorId: number): Observable<Cliente[]>{
    return this.http.get<Cliente[]>(`${this.baseURL}/vendedor/${vendedorId}`).pipe(take(1));
  }

  public put(cliente: Cliente): Observable<Cliente>{
    return this.http.put<Cliente>(`${this.baseURL}/${cliente.id}`, cliente).pipe(take(1));
  }

  public post(cliente: Cliente): Observable<Cliente>{
    return this.http.post<Cliente>(this.baseURL, cliente).pipe(take(1));
  }

  public delete(id: number): Observable<any>{
    return this.http.delete(`${this.baseURL}/${id}`).pipe(take(1));
  }
}
