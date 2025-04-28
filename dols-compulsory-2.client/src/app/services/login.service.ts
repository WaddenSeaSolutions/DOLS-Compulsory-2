import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { LoginRequest } from "../models/login-request.model";
import { User } from "../models/user.model";

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  private apiUrl: string = 'https://localhost:44310/api/';
  private http: HttpClient = inject(HttpClient);


  login(username: string, password: string) {
    const loginRequest: LoginRequest = {
      Username: username,
      Password: password

    }
    const response = this.http.post(this.apiUrl+'Users/Login',loginRequest)
    return response
  }

  register(username: string, password: string, email: string) {
    const user: User = {
      Id : 1,
      Username: username,
      Password: password,
      Email: email
    }
    this.http.post(this.apiUrl + 'Users/Register', user)
  }

}
