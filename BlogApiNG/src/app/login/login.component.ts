import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../service/user.service';
import { UserLogin } from '../model/userLogin';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent implements OnInit {
  resetPassword = false;
  email:string;
  error: any;
  password: string;

  constructor(private userService: UserService, private router: Router) { }

  ngOnInit() {
    this.resetPassword = false;
  }

  loginUser() {
    let login: UserLogin= {
      email:this.email,
      password: this.password
    };
    console.log("you are logging in", this.email, this.password , login)
    this.userService.loginUsers(login)
    .subscribe((resp: any) => {
      //success handling
      //data.token
      //this.router.navigate(['profile']);
      localStorage.setItem('auth_token', resp.token);
      localStorage.setItem('user_name', resp.userName);
      this.router.navigate(['profile']);
      //debugger;
    }, error => {
      this.error = error.message.code; console.log(error);
    });;
  }
 
  public get logIn(): boolean {
    return (localStorage.getItem('auth_token') !== null);
  }

  isResetPassword(value:boolean)
  {
    this.resetPassword = value;
  }

  newPasswordToUserEmail(email:string)
  {
    this.userService.setNewPassword(email).subscribe((resp: string) => {
      this.resetPassword = false;
    }, error => {
      this.error = error.message.code; console.log(error);
      confirm("error "+ error)
    });;
  }
}
