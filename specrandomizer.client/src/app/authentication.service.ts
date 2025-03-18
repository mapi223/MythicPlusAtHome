import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  private apiUrl = 'https://localhost:7174/api/auth';

  private isAdminSubject = new BehaviorSubject<boolean>(this.getAdminStatus());
  private isTokenSubject = new BehaviorSubject<boolean>(this.getTokenStatus());

  isAdmin$ = this.isAdminSubject.asObservable(); // Observable for components
  isToken$ = this.isTokenSubject.asObservable();
  constructor(private http: HttpClient) { }

  login(username: string, password: string): Observable<any> {
    return this.http.post<{ token: string, userId: string, isAdmin: boolean }>(`${this.apiUrl}/login`, { username, password }).pipe(
      tap(response => {
        localStorage.setItem('token', response.token);
        localStorage.setItem('userId', response.userId);
        response.isAdmin === true ? this.updateAdminStatus(true) : this.updateAdminStatus(false);
        response.token != null ? this.updateTokenStatus(true) : this.updateTokenStatus(false);
      })
    );
  }

  register(username: string, password: string): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/register`, { username, password }).pipe();
  }

  logout() {
    localStorage.removeItem('isAdmin');
    this.isAdminSubject.next(false);
    localStorage.removeItem('token');
    this.isTokenSubject.next(false);
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem('token');
  }

  private getAdminStatus(): boolean {
    return localStorage.getItem('isAdmin') === 'true';
  }

  private getTokenStatus(): boolean {
    return localStorage.getItem('token') != null ? true : false;
  }

  updateAdminStatus(isAdmin: boolean) {
    localStorage.setItem('isAdmin', String(isAdmin));
    this.isAdminSubject.next(isAdmin);
  }

  updateTokenStatus(isToken: boolean) {
    localStorage.setItem('isToken', String(isToken));
    this.isTokenSubject.next(isToken);
  }

}
