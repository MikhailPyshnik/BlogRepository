import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../service/user.service';
import { UserRegistration } from '../model/userRegistration';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {
  userName:string;
  email:string;
  error: any;
  password: string;
  confirmPassword: string;

  constructor(private userService: UserService, private router: Router) { }

  ngOnInit(): void {
  }

  registrationUser(){
    let registrationUser: UserRegistration= {
      userName:this.userName,
      email:this.email,
      password: this.password,
      confirmPassword: this.confirmPassword
    };
    console.log("you are logging in", this.email, this.password , registrationUser)
    debugger;
    this.userService.registrationUser(registrationUser)
    .subscribe((resp: any) => {
      this.router.navigate(['login']);
    }, error => {
      this.error = error.message.code; console.log(error);
    });;
  }

}
