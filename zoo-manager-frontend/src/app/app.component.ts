import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  isAuthenticated: boolean = false;

  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit(): void {
    this.authService.getAuthState().subscribe(newState => {
      this.isAuthenticated = newState;
    });
  }

  logout(): void {
    sessionStorage.removeItem("token");

    this.authService.setAuthState(false);
    this.router.navigateByUrl("/login");
  }
}
