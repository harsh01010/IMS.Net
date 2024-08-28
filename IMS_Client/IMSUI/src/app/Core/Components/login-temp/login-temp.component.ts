import { Component, EventEmitter, Output } from '@angular/core';
import { LoginService } from '../../../Services/Login/login.service';
import { LoginCredentials } from '../../../Models/Login.model';
import { FormsModule } from '@angular/forms';
import { TokenStorageService } from '../../../Services/token/token.service';
import { CommonModule } from '@angular/common';
import { LoaderComponent } from "../reuseable/loader/loader.component";


@Component({
  selector: 'app-login-temp',
  standalone: true,
  imports: [FormsModule, LoaderComponent, CommonModule],
  templateUrl: './login-temp.component.html',
  styleUrl: './login-temp.component.scss'
})
export class LoginTempComponent {

  @Output() close = new EventEmitter<void>();
  @Output() loginStatus = new EventEmitter<boolean>();

  cred: LoginCredentials = { userName: '', password: '' }
  processing = false;

  loginSuccess = false;

  constructor(private loginService: LoginService, private sessionService: TokenStorageService) { }


  requestLogin = () => {
    console.log("requested");
    if (this.cred.userName !== '' || this.cred.password !== '') {
      this.processing = true
      this.loginService.login(this.cred).subscribe({

        next: (res) => {
          if (res.isSuccess) {

            this.sessionService.saveToken(res.result.jwtToken);
            this.sessionService.saveUser(res.result.user);
            this.loginSuccess = true;
            console.log(res.result);

          }
        },
        error: (err) => console.log(err),
        complete: () => { this.loginStatus.emit(this.loginSuccess); this.processing = false }
      })
    }
  }


  closeLogin = () => {
    this.close.emit();
  }






}
