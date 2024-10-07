import { Component, OnInit } from '@angular/core';
import { LoginComponent } from './components/login/login.component';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-auth',
  standalone: true,
  imports: [LoginComponent],
  templateUrl: './auth.component.html',
  styleUrl: './auth.component.scss'
})
export class AuthComponent implements OnInit{
  constructor( private authService: AuthService) {}

  ngOnInit(): void {
    this.authService.clearToken();
  }

}
