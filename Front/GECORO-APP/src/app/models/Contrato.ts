import { Cliente } from "./Cliente";
import { Parcela } from "./Parcela";

export interface Contrato {
  id: number;
  clienteId: number;
  cliente: Cliente;
  nuContrato: string;
  parcelas: Parcela[];
  saldoDevedor: number;
  valorTotal: number;
}
