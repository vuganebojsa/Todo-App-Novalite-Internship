import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { MsalService } from '@azure/msal-angular';
import { UserToken } from '../../models/User';
import { AuthenticationService } from '../../services/authentication.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  loginDisplay = false;


  ngOnInit(): void {
    this.login();
    this.authService.handleRedirectObservable().subscribe(response => {

        // Tokens retrieved, proceed with your app logic
        this.router.navigate(['']);
        
      }
      
    );
  }

  constructor(private authService:MsalService, private authenticationService: AuthenticationService, private router:Router)
  {

  }
  login()
  {
    this.authService.loginPopup()
      .subscribe({
        next: (result) => {
          localStorage.setItem('token', result.accessToken);
            let k = result.idTokenClaims as UserToken;
            localStorage.setItem('email' ,k.preferred_username);
            localStorage.setItem('name' ,k.name);
            this.authenticationService.loginUser();
            this.authenticationService.loginToPortal(k.preferred_username).subscribe(user =>{
            const role = user.role;
            const user_id = user.id;
            localStorage.setItem('role', role);
            localStorage.setItem('user_id', user_id);
              this.router.navigate(['lists']);
            })

        },
        error: (error) => console.log(error)
      });
  }

  setLoginDisplay() {
    this.loginDisplay = this.authService.instance.getAllAccounts().length > 0;
}
}
