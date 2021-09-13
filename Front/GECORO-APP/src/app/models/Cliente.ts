import { Contrato } from "./Contrato";
import { Vendedor } from "./Vendedor";

export interface Cliente {
  id: number;
  cpf: string
  nome: string
  contratos: Contrato[];
  vendedorId: number;
  vendedor: Vendedor;
}
