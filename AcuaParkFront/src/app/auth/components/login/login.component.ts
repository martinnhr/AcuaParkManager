import { Component } from '@angular/core';
import {FormControl, Validators, FormsModule, ReactiveFormsModule} from '@angular/forms';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import {MatButtonModule} from '@angular/material/button';


@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, MatFormFieldModule, MatInputModule, ReactiveFormsModule, MatButtonModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})

export class LoginComponent {

  emailFormControl = new FormControl('user@example.com', [Validators.required, Validators.email]);
  passwordControl = new FormControl('string', [Validators.required, Validators.minLength(3)])


  errorMessage: string = '';

  constructor(private authService: AuthService, private router: Router) {}

  onLogin() {
    // Verificar si el formulario es válido antes de enviar la solicitud
    console.log("Verificar si el formulario es válido antes de enviar la solicitud");
    if (this.emailFormControl.valid && this.passwordControl.valid) {
      console.log("Verificacion true");
      const loginData = {
        email: this.emailFormControl.value?? '',
        password: this.passwordControl.value?? ''
      };

      // Llamar al servicio de autenticación
      this.authService.login(loginData).subscribe({
        next: (response) => {
          this.authService.setToken(response.token);  // Guardar el token JWT
          console.log("redirigiendo a home")
          this.router.navigate(['/home']);  // Redirigir a la página principal
        },
        error: (err) => {
          console.log("ha dado error");
          this.errorMessage = 'Login failed. Please try again.' + err;
        }
      });
    } else {
      // Mostrar mensaje de error si los campos no son válidos
      console.log("Verificacion false");
      this.errorMessage = 'Please enter a valid email and password.';
    }
  }

}
