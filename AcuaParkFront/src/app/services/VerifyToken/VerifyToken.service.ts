import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class VerifyToken {

  private verifyTokenUrl = 'http://localhost:5249/api/Token/VerifyToken';

  constructor(private http: HttpClient) { }

  verifyToken(): Observable<any> {
    try {
      
      const token = localStorage.getItem('token');
      if (!token) {
        return of({ valid: false, message: 'No token found' });
      }
      
      const headers = new HttpHeaders({
        'Authorization': `Bearer ${token}`
      });
      return this.http.get(this.verifyTokenUrl, { headers }).pipe(
        catchError(error => {
          console.error('Error verifying token', error);
          return of({ valid: false, message: 'Token verification failed' });
        })
      );
    } catch (error) {
      console.error('Error verifying token', error);
      return of({ valid: false, message: 'Token no encontrado en local storage' });
    }

  }
}