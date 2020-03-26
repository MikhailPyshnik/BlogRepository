import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable} from 'rxjs';
import { UserLogin } from '../model/userLogin';
import { User } from '../model/user';
import { UserRegistration } from '../model/userRegistration';

@Injectable({
  providedIn: 'root'
})

export class UserService {
  Url: string;

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8'
    })
  };

  constructor(private http: HttpClient) {
    this.Url = 'https://localhost:44338/api/user/';
  }

  getAllUsers(): Observable<Array<User>> {
    return this.http.get<Array<User>>(this.Url);
  }

  loginUsers(userLogin : UserLogin): Observable<UserLogin> {
    return this.http.post<UserLogin>(this.Url + 'authenticate', JSON.stringify(userLogin), this.httpOptions);
  }

  registrationUser(registrationUser : UserRegistration): Observable<UserRegistration> {
    return this.http.post<UserRegistration>(this.Url + 'Register', JSON.stringify(registrationUser), this.httpOptions);
  }

  setNewPassword(email : string): Observable<string> {
    return this.http.post<string>(this.Url +'sendnewpassword', JSON.stringify(email), this.httpOptions);
  }
}