import { TestBed } from '@angular/core/testing';

import { CalcularInvestimentoService } from './calcular-investimento.service';

describe('CalcularInvestimentoService', () => {
  let service: CalcularInvestimentoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CalcularInvestimentoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
