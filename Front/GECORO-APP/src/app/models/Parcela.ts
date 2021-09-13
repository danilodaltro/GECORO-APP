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
  Paga = 'P',
  Aberta = 'A'
}
