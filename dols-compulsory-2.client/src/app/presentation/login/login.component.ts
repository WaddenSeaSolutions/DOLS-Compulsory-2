import { Component, inject, signal, WritableSignal } from '@angular/core';
import { LoginFacade } from './login.facade';
import { FormsModule } from '@angular/forms';

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

  constructor() {
    this.loginUsernameValue = signal('');
    this.loginPasswordValue = signal('');
    this.registerUsernameValue = signal('');
    this.registerPasswordValue = signal('');
    this.registerEmailValue = signal('');
  }

  login() {
    const success: boolean = this.loginFacade.login(this.loginUsernameValue(), this.loginPasswordValue());
    if (success) {

    }
  }

  readyRegister() {
    this.registerReady = true;
  }

  register() {
    this.loginFacade.register(this.registerUsernameValue(), this.registerPasswordValue(),this.registerEmailValue());
  }

  cancelRegister() {
    this.registerReady = false;
  }
}
