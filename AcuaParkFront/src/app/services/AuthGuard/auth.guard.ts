import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from '../../auth/services/auth.service';  // Asegúrate de ajustar la ruta correcta
import { VerifyToken } from '../VerifyToken/VerifyToken.service';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private VerifyToken: VerifyToken, private authService: AuthService, private router: Router) {}

  canActivate(): Observable<boolean> | Promise<boolean> | boolean {
    console.log("dentro de canActivate");
    const token = this.authService.getToken();
    
    if (!token) {
      console.log("empty token");
      this.router.navigate(['/']);
      return false;
    }


    return this.VerifyToken.verifyToken().pipe(
      map(response => {
        if (response.valid) {
          console.log("respuesta valida")
          return true;
        } else {
          console.log("respuesta invalida")
          this.router.navigate(['/']);
          return false;
        }
      })
    );
  }
}