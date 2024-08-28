import { Component } from '@angular/core';
import { LoginService } from '../../../Services/Login/login.service';
import { LoginCredentials } from '../../../Models/Login.model';
import { FormsModule } from '@angular/forms';
import { TokenStorageService } from '../../../Services/token/token.service';


@Component({
  selector: 'app-login-temp',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './login-temp.component.html',
  styleUrl: './login-temp.component.scss'
})
export class LoginTempComponent {

  cred: LoginCredentials = { userName: '', password: '' }

  loginSuccess = false;

  constructor(private loginService: LoginService, private sessionService: TokenStorageService) { }


  requestLogin = () => {
    if (this.cred.userName !== '' || this.cred.password !== '') {
      this.loginService.login(this.cred).subscribe({
        next: (res) => {
          if (res.isSuccess) {

            this.sessionService.saveToken(res.result.jwtToken);
            this.sessionService.saveUser(res.result.user);
            this.loginSuccess = true;
            console.log(res.result);
          }
        },
        error: (err) => console.log(err)
      })
    }
  }





}
