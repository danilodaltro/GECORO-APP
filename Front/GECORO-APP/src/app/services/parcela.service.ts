import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Parcela } from '../models/Parcela';

@Injectable()
export class ParcelaService {

  private baseURL: string = 'https://localhost:5001/api/parcelas';
  constructor(private http: HttpClient) { }

  public getParcelaByContrato(numeroContrato: string): Observable<Parcela[]>{
    return this.http.get<Parcela[]>(`${this.baseURL}/contrato/${numeroContrato}`);
  }

  public getParcelaById(id: number): Observable<Parcela>{
    return this.http.get<Parcela>(`${this.baseURL}/${id}`);
  }
}
