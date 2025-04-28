import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { LoginRequest } from "../models/login-request.model";
import { User } from "../models/user.model";
import { RegisterRequest } from "../models/register-request.model";
import { firstValueFrom } from "rxjs/internal/firstValueFrom";

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  private apiUrl: string = 'http://localhost:80/api/User/';
  private http: HttpClient = inject(HttpClient);


  async login(username: string, password: string): Promise<any> {
    const loginRequest: LoginRequest = { Username: username, Password: password };

    return firstValueFrom(this.http.post(this.apiUrl + 'login', loginRequest));
  }

  async register(username: string, password: string, email: string) {
    const user: RegisterRequest = {
      Username: username,
      Password: password,
      Email: email
    }
    const response = this.http.post(this.apiUrl + 'register', user)
    return firstValueFrom(response);
  }

}
