import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class AuthService {
  private apiUrl = 'http://localhost:5249/api/Account/login';  // URL de tu API

  constructor(private http: HttpClient) {}

  // Método para hacer login
  login(data: { email: string, password: string }): Observable<any> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });

    return this.http.post<any>(this.apiUrl, data, { headers });
  }

  // Método para almacenar el token JWT en el almacenamiento local
  setToken(token: string) {
    localStorage.setItem('authToken', token);
  }

  // Método para obtener el token almacenado
  getToken(): string | null {
    return localStorage.getItem('authToken');
  }

  // Método para eliminar el token (en caso de logout)
  clearToken() {
    localStorage.removeItem('authToken');
  }
}


