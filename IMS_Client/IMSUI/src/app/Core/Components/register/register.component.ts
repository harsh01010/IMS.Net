import { RouterModule } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule , FormsModule , NgForm } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RegisterService } from '../../../Services/Register/register.service';
import { Register } from '../../../Models/Register.model';
import {  Subscription } from 'rxjs';

@Component({
  standalone : true,
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
  imports: [CommonModule,  FormsModule, RouterModule, ReactiveFormsModule]
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

  constructor(private registerService: RegisterService) { }

  ngOnInit(): void {
  }

  subsciption?:Subscription

  onSubmit(form:NgForm){
    console.log(this.register);
    if(form.valid){
   this.subsciption=this.registerService.register(this.register).subscribe({
    next: (response) => {
      console.log(response);
      form.resetForm();
      this.isSuccessful = true;
      this.isSignUpFailed = false;
    },
    error: (err) => {
      this.errorMessage = err.error.message;
      this.isSignUpFailed = true;
    }  })};
   }
  
}