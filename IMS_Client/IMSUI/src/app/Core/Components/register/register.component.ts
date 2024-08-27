import { RouterModule } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule , FormsModule} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RegisterService } from '../../../Services/Register/register.service';

@Component({
  standalone : true,
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
  imports: [CommonModule, ReactiveFormsModule , FormsModule, RouterModule]
})
export class RegisterComponent implements OnInit {
  form: any = {
    username: null,
    email: null,
    password: null
  };
  isSuccessful = false;
  isSignUpFailed = false;
  errorMessage = '';

  constructor(private registerService: RegisterService) { }

  ngOnInit(): void {
  }

  onSubmit(): void {
    const { username, email, password } = this.form;

    this.registerService.register(username, email, password).subscribe({
      next: data => {
        console.log(data);
        this.isSuccessful = true;
        this.isSignUpFailed = false;
      },
      error: err => {
        this.errorMessage = err.error.message;
        this.isSignUpFailed = true;
      }
    });
  }
}