import { ComponentFixture, TestBed } from '@angular/core/testing';

<<<<<<<< HEAD:IMS_Client/IMSUI/src/app/Core/Components/cart/cart.component.spec.ts
import { CartComponent } from './cart.component';

describe('CartComponent', () => {
  let component: CartComponent;
  let fixture: ComponentFixture<CartComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CartComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CartComponent);
========
import { LoginTempComponent } from './login-temp.component';

describe('LoginTempComponent', () => {
  let component: LoginTempComponent;
  let fixture: ComponentFixture<LoginTempComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LoginTempComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LoginTempComponent);
>>>>>>>> b2a7f300e6e7423abba8c0d0b83403a5a44d20b7:IMS_Client/IMSUI/src/app/Core/Components/login-temp/login-temp.component.spec.ts
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
