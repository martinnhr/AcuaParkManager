import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { TestBBDDComponent } from './components/test-bbdd/test-bbdd.component';
import { LoginComponent } from './auth/components/login/login.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, TestBBDDComponent, LoginComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'AcuaParkFront';
}
