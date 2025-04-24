import { inject, Injectable } from "@angular/core";
import { LoginService } from "../services/login.service";

@Injectable({
  providedIn: 'root',
})
export class LoginManager {
  private loginService: LoginService = inject(LoginService);

  login(username: string, password: string): boolean {
    this.loginService.login(username, password).subscribe({
      next: (response: any) => {
        console.log('Login successful', response);
        return true
      },
      error: (error: any) => {
        console.error('Login failed', error);
        return false
      }
    });
    return false
  }

  register(username: string, password: string, email: string) {
    this.loginService.register(username, password, email)
  }

}
