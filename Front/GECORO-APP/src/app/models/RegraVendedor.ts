import { Vendedor } from "./Vendedor";

export interface RegraVendedor {
  id: number;
  vendedorId: number;
  vendedor: Vendedor;
  saldoDevedor: number;
  parcelasPagas: number;
}
