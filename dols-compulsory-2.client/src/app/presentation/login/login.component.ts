import { Component, signal, WritableSignal } from '@angular/core';

@Component({
  selector: 'app-login',
  standalone: false,
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  loginUsernameValue: WritableSignal<string>;
  loginPasswordValue: WritableSignal<string>;
  registerUsernameValue: WritableSignal<string>;
  registerPasswordValue: WritableSignal<string>;
  registerReady:boolean = false

  constructor() {
    this.loginUsernameValue = signal('');
    this.loginPasswordValue = signal('');
    this.registerUsernameValue = signal('');
    this.registerPasswordValue = signal('');
  }

  login() {
    
  }

  readyRegister() {
    this.registerReady = true;
  }

  register() {

  }

  cancelRegister() {
    this.registerReady = false;
  }
}
