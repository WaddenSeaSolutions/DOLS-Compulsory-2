import { inject, Injectable } from "@angular/core";
import { LoginService } from "../services/login.service";
import { map, catchError, of } from "rxjs";

@Injectable({
  providedIn: 'root',
})
export class LoginManager {
  private loginService: LoginService = inject(LoginService);

  async login(username: string, password: string): Promise<boolean> {
    try {
      const response = await this.loginService.login(username, password);
      console.log('Login successful', response);
      return true;
    } catch (error) {
      console.error('Login failed', error);
      return false;
    }
  }

  async register(username: string, password: string, email: string) {
    try {
      const response = await this.loginService.register(username, password, email)
      console.log('register successful', response);
      return true
    } catch (error) {
      return false
    }
  }

}
