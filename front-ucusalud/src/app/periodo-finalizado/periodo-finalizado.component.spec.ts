import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PeriodoFinalizadoComponent } from './periodo-finalizado.component';

describe('PeriodoFinalizadoComponent', () => {
  let component: PeriodoFinalizadoComponent;
  let fixture: ComponentFixture<PeriodoFinalizadoComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PeriodoFinalizadoComponent]
    });
    fixture = TestBed.createComponent(PeriodoFinalizadoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
