import { Component, OnInit } from '@angular/core';
import { MsalService } from '@azure/msal-angular';
import { AuthenticationService } from 'src/app/core/services/authentication.service';
import * as jwt_decode from 'jwt-decode';
import { TokenDecoderService } from 'src/app/core/services/token-decoder.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit{

  role:string = '';
  isLoggedIn:boolean = false;
  family_name: string = '';
  given_name: string = '';
  ngOnInit(): void {
   this.authenticationService.userStateLoggedIn$.subscribe(res =>{
      this.isLoggedIn = res;
      if(!this.isLoggedIn) {
        this.router.navigate(['']);
      }
   });
   
  }
  constructor(private authService: MsalService, 
    private authenticationService: AuthenticationService, private tokenDecoderService: TokenDecoderService, private router: Router){

  }
  logout():void{
  //   this.authService.logoutPopup({
  //     mainWindowRedirectUri: "/",
  // });
    
  this.authenticationService.logoutUser();

    localStorage.clear();
    sessionStorage.clear();
  }
}
