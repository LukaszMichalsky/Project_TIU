import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { AuthRequest } from 'src/models/authrequest';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styles: [
  ]
})
export class AuthComponent implements OnInit {
  errorMessage: string = '';

  @ViewChild("usernameInput") usernameInput: ElementRef | undefined;
  @ViewChild("passwordInput") passwordInput: ElementRef | undefined;
  @ViewChild("buttonModalError") buttonModalError: ElementRef | undefined;

  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit(): void {
  }

  login(): void {
    let username: string = this.usernameInput?.nativeElement.value;
    let password: string = this.passwordInput?.nativeElement.value;

    let authRequest: AuthRequest = {
      username, password
    }

    this.authService.login(authRequest).subscribe(authResponse => {
      sessionStorage.setItem("token", authResponse.token);

      this.authService.setAuthState(true);
      this.router.navigateByUrl('/');
    }, () => {
      this.errorMessage = "Invalid username or password. Please try again.";
      this.buttonModalError?.nativeElement.click();
    });
  }
}
