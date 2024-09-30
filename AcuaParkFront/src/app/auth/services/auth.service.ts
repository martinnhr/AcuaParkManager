import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
@Injectable({
  providedIn: 'root'
})

export class AuthService {
  private apiUrl = 'http://localhost:5249/api/Account/login';  // URL de tu API

  constructor(private http: HttpClient, private router: Router) {}

  // Método para hacer login
  login(data: { email: string, password: string }): Observable<any> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });

    return this.http.post<any>(this.apiUrl, data, { headers });
  }

  // Método para almacenar el token JWT en el almacenamiento local
  setToken(token: string) {
    //console.log("Almacenando token en localstorage: "+token);
    localStorage.setItem('token', token);
  }

  // Método para obtener el token almacenado
  getToken(): string | null {
  try {
      return localStorage.getItem('token');
  } catch (error) {
    console.log(error);
    this.router.navigate(['/']);
    return '';
  }
  }

  // Método para eliminar el token (en caso de logout)
  clearToken() {
    localStorage.removeItem('token');
  }
}


