import { Cliente } from "./Cliente";
import { RegraVendedor } from "./RegraVendedor";

export interface Vendedor {
  id: number;
  codigo: string;
  nome: string;
  clientes: Cliente[];
  regraVendedor: RegraVendedor;
}
