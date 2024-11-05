import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit{
  loading: boolean = false;
  model = {
    username: "",
    password: ""
  };
  
  constructor(
    private authenticationService: AuthenticationService,
    private router: Router,
  ) {}
  
  ngOnInit() {
  
  }

  login() {
    this.loading = true;
    this.authenticationService.login(this.model).subscribe({
      next: () => {
      this.router.navigateByUrl('/users')
      // this.toastr.success("Login Succussful")
      },
      error: error => {
        alert(error.error);
        // this.toastr.error(error.error.title)
        console.log(error);
        this.loading = false;
      }
    });
  }
}
