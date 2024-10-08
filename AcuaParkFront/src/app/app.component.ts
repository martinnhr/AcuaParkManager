import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { TestBBDDComponent } from './testBBDD/components/test-bbdd/test-bbdd.component';
import { LoginComponent } from './auth/components/login/login.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, LoginComponent, TestBBDDComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'AcuaParkFront';
}
