import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductCardSmallComponent } from './product-card-small.component';

describe('ProductCardSmallComponent', () => {
  let component: ProductCardSmallComponent;
  let fixture: ComponentFixture<ProductCardSmallComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProductCardSmallComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProductCardSmallComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
