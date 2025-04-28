import { Component, inject, signal, WritableSignal } from '@angular/core';
import { LoginFacade } from './login.facade';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  loginUsernameValue: WritableSignal<string>;
  loginPasswordValue: WritableSignal<string>;
  registerUsernameValue: WritableSignal<string>;
  registerPasswordValue: WritableSignal<string>;
  registerEmailValue: WritableSignal<string>;
  registerReady: boolean = false
  private loginFacade: LoginFacade = inject(LoginFacade)


  constructor(private router: Router) {
    this.loginUsernameValue = signal('');
    this.loginPasswordValue = signal('');
    this.registerUsernameValue = signal('');
    this.registerPasswordValue = signal('');
    this.registerEmailValue = signal('');
  }

  async login() {
    const username = this.loginUsernameValue();
    const password = this.loginPasswordValue();

    const success = await this.loginFacade.login(username, password);
    if (success) {
      // only runs when login() resolved true
      this.router.navigate(['/home']);
    } else {
      console.error('Login failed');
      // show a UI error if you wish
    }
  }

  readyRegister() {
    this.registerReady = true;
  }

  async register() {
    const success = await this.loginFacade.register(this.registerUsernameValue(), this.registerPasswordValue(), this.registerEmailValue());
    if (success) {
      this.registerReady = false;
    }
  }

  cancelRegister() {
    this.registerReady = false;
  }
}
