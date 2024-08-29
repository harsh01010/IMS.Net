import { RouterModule } from '@angular/router';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ReactiveFormsModule, FormsModule, NgForm } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RegisterService } from '../../../Services/Register/register.service';
import { Register } from '../../../Models/Register.model';
import { Subscription } from 'rxjs';

@Component({
  standalone: true,
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
  imports: [CommonModule, FormsModule, RouterModule, ReactiveFormsModule]
})
export class RegisterComponent implements OnInit {
  register: Register = {
    email: '',
    name: '',
    phoneNumber: '',
    role: '',
    password: ''
  }
  isSuccessful = false;
  isSignUpFailed = false;
  errorMessage = '';

  @Output() close = new EventEmitter<void>();
  @Output() openLoginForm = new EventEmitter<boolean>();

  constructor(private registerService: RegisterService) { }

  ngOnInit(): void {
  }

  subsciption?: Subscription

  onSubmit(form: NgForm) {
    console.log(form);
    if (form.valid) {
      console.log("form valid");
      this.subsciption = this.registerService.register(this.register).subscribe({
        next: (response) => {
          console.log(response);
          form.resetForm();
          this.isSuccessful = true;
          this.isSignUpFailed = false;
        },
        error: (err) => {
          this.errorMessage = err.error.message;
          this.isSignUpFailed = true;
        }
      })
    };
  }
  closeRegister=()=>{
   this.close.emit();
  }
  openLogin=()=>{
  this.close.emit();
  //console.log("testing")
  this.openLoginForm.emit(true);
  }
}