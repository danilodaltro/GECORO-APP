import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Parcela } from '../models/Parcela';
import { take } from 'rxjs/operators'

@Injectable()
export class ParcelaService {

  private baseURL: string = 'https://localhost:5001/api/parcela';
  constructor(private http: HttpClient) { }

  public getParcelasByContrato(idContrato: number): Observable<Parcela[]>{
    return this.http.get<Parcela[]>(`${this.baseURL}/contrato/${idContrato}`).pipe(take(1));
  }

  public getParcelaById(id: number): Observable<Parcela>{
    return this.http.get<Parcela>(`${this.baseURL}/${id}`).pipe(take(1));
  }

  public put(parcela: Parcela): Observable<Parcela>{
    return this.http.put<Parcela>(`${this.baseURL}/${parcela.id}`, parcela).pipe(take(1));
  }

  public post(parcela: Parcela): Observable<Parcela>{
    return this.http.post<Parcela>(this.baseURL, parcela).pipe(take(1));
  }

  public delete(id: number): Observable<any>{
    return this.http.delete(`${this.baseURL}/${id}`).pipe(take(1));
  }
}
