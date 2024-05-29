import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InicializarComponent } from './inicializar.component';

describe('InicializarComponent', () => {
  let component: InicializarComponent;
  let fixture: ComponentFixture<InicializarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [InicializarComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(InicializarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
