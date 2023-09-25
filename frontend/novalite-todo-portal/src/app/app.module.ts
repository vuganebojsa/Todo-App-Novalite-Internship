import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { MsalModule } from '@azure/msal-angular';
import { MsalGuard, MsalInterceptor, MsalService, MsalBroadcastService } from '@azure/msal-angular';
import { MsalRedirectComponent } from '@azure/msal-angular';
import { BrowserCacheLocation, InteractionType, PublicClientApplication } from '@azure/msal-browser';
import { environment } from 'src/environment/environment';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SharedModule } from './shared/shared.module';
const isIE = window.navigator.userAgent.indexOf('MSIE ') > -1 || window.navigator.userAgent.indexOf('Trident/') > -1;

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    SharedModule,
    HttpClientModule,
    MsalModule.forRoot(
      new PublicClientApplication({
        auth:{
          clientId: environment.azureADClientId,
          authority: environment.azureADAuthority,
          redirectUri: 'http://localhost:4200'
        },
        cache:{
          cacheLocation: 'localStorage',
          storeAuthStateInCookie: isIE
        }
      }),{
        interactionType: InteractionType.Redirect
      },{
        interactionType: InteractionType.Redirect,
        protectedResourceMap: new Map([
          ['https://localhost:7147/', [environment.azureADScope]]
        ])
      }
    )
  ],
  providers: [{provide: HTTP_INTERCEPTORS, useClass: MsalInterceptor, multi: true}, MsalGuard],
  bootstrap: [AppComponent, MsalRedirectComponent]
})
export class AppModule { }
