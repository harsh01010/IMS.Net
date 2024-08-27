import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductCardBigComponent } from './product-card-big.component';

describe('ProductCardBigComponent', () => {
  let component: ProductCardBigComponent;
  let fixture: ComponentFixture<ProductCardBigComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProductCardBigComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProductCardBigComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
