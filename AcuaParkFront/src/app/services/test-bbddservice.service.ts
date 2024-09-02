import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TestBBDDServiceService {

  private apiUrl = "http://localhost:5087/testBBDD"

  constructor(private http: HttpClient) { }

  getTestBBDD(): Observable<any> {

    return this.http.get(this.apiUrl, { responseType: 'text'});

  }

  
}
