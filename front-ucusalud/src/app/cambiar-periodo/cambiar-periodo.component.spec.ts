import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CambiarPeriodoComponent } from './cambiar-periodo.component';

describe('CambiarPeriodoComponent', () => {
  let component: CambiarPeriodoComponent;
  let fixture: ComponentFixture<CambiarPeriodoComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CambiarPeriodoComponent]
    });
    fixture = TestBed.createComponent(CambiarPeriodoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
