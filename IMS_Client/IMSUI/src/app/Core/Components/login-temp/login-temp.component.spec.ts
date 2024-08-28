import { ComponentFixture, TestBed } from '@angular/core/testing';

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
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
