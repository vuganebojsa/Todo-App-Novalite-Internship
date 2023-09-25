import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RouteGuard implements CanActivate {
  constructor(private router: Router) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    // Check if the user's role is stored in localStorage
    const userRole = localStorage.getItem('role');

    if (userRole !== null && userRole !== undefined) {
      return true;
    } else {
      this.router.navigate(['']);
      return false;
    }
  }
  
}
