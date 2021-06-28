import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Config } from 'src/config';
import { AuthRequest } from 'src/models/authrequest';
import { AuthResponse } from 'src/models/authresponse';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private controllerURL: string = "Auth";
  private authState: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

  constructor(private http: HttpClient) {}

  getAuthState(): Observable<boolean> {
    return this.authState.asObservable();
  }

  setAuthState(newState: boolean): void {
    this.authState.next(newState);
  }

  login(authRequest: AuthRequest): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(Config.getURL(this.controllerURL), authRequest);
  }

  validate(): Observable<void> {
    return this.http.get<void>(Config.getURL(this.controllerURL), {
      headers: {
        Authorization: `Bearer ${sessionStorage.getItem("token")}`
      }
    });
  }
}
