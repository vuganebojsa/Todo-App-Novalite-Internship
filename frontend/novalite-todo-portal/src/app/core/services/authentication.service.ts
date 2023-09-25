import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { MsalBroadcastService, MsalGuardConfiguration, MsalService, MSAL_GUARD_CONFIG } from '@azure/msal-angular';
import { AccountInfo, AuthenticationResult, InteractionStatus, InteractionType, PopupRequest, PublicClientApplication, RedirectRequest } from '@azure/msal-browser';
import { BehaviorSubject, catchError, filter, Observable, of, Subject, takeUntil } from 'rxjs';
import { environment } from 'src/environment/environment';
import { MyToken, User, UserResponse } from '../models/User';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  baseUrl = environment.apiHost + 'users';
 
  user$ = new BehaviorSubject(null);
  userState$ = this.user$.asObservable();

  userLoggedIn$ = new BehaviorSubject(false);
  userStateLoggedIn$ = this.userLoggedIn$.asObservable();

  constructor(private http:HttpClient) {
    this.user$.next(this.isLoggedIn());
    this.userLoggedIn$.next(this.isLoggedIn());

   }
 isLoggedIn():boolean{

    return localStorage.getItem('token') != null;
  
 }
  loginUser(): void{
    this.userLoggedIn$.next(true);
  }
  logoutUser(): void{
    this.userLoggedIn$.next(false);
    }

  loginToPortal(email: string):Observable<MyToken>{
    return this.http.post<MyToken>(this.baseUrl, {email: email});

  }

  getUsers(): Observable<UserResponse>{
    return this.http.get<UserResponse>(this.baseUrl);

  } 
  getUsersWithAdmin(): Observable<UserResponse>{
    return this.http.get<UserResponse>(this.baseUrl + '/admin');

  } 

  changeRole(email:string, role: string): Observable<User>{
    return this.http.put<User>(this.baseUrl, {email:email, role:role});

  } 
}
