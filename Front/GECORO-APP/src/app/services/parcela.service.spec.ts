/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { ParcelaService } from './parcela.service';

describe('Service: Parcela', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ParcelaService]
    });
  });

  it('should ...', inject([ParcelaService], (service: ParcelaService) => {
    expect(service).toBeTruthy();
  }));
});
