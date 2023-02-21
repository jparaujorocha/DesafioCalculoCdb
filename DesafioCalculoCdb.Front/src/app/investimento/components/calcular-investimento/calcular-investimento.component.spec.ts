import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CalcularInvestimentoComponent } from './calcular-investimento.component';

describe('CalcularInvestimentoComponent', () => {
  let component: CalcularInvestimentoComponent;
  let fixture: ComponentFixture<CalcularInvestimentoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CalcularInvestimentoComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CalcularInvestimentoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
