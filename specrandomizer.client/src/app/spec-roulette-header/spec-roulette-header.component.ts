import { Component } from '@angular/core';
import { AuthenticationService } from '../authentication.service';

@Component({
  selector: 'app-spec-roulette-header',
  templateUrl: './spec-roulette-header.component.html',
  styleUrls: ['./spec-roulette-header.component.css']
})
export class SpecRouletteHeaderComponent {

  isMenuOpen = false;
  username = '';
  password = '';

  constructor(private authService: AuthenticationService) { }

  toggleMenu() {
    this.isMenuOpen = !this.isMenuOpen;
  }

  login() {
    this.authService.login(this.username, this.password).subscribe(response => {
      localStorage.setItem('token', response.token);
      this.toggleMenu(); // Close the menu after logging in
    }, error => {
      console.error('Login failed', error);
    });
  }

  goHome() {

  }

}
