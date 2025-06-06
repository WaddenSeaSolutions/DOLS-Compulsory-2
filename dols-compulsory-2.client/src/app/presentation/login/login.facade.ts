import { inject, Injectable } from "@angular/core";
import { LoginManager } from "../../managers/login.manager";

@Injectable({
  providedIn: 'root',
})
export class LoginFacade {

  private loginManager: LoginManager = inject(LoginManager);

  login(username: string, password: string): Promise<boolean> {
    return this.loginManager.login(username, password);
  }

  register(username:string,password:string,email:string) {
    return this.loginManager.register(username, password, email);
  }
}
