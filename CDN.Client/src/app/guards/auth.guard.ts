import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable, map } from 'rxjs';
import { AuthenticationService } from '../services/authentication.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
constructor(private router: Router,
  private authenticationService: AuthenticationService
) {}
  canActivate(): Observable<boolean>{
    return this.authenticationService.currentUser$.pipe(
      map(user => {
        if (user) {
          return true;
        }
        this.router.navigate(['/']);
        return false;
      })
    );
  }
}
