import { Contrato } from "./Contrato";

export interface Parcela {
  id: number;
  contratoId: number;
  contrato: Contrato;
  valor: number;
  nuParcela: number;
  stParcela: SituacaoParcela;
}

export enum SituacaoParcela {
  P = 0,
  A = 1
}
