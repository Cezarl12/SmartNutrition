import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { SnackBarComponent } from './shared/components/snack-bar-component/snack-bar-component';
import { Header } from './shared/components/header/header';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, SnackBarComponent, Header],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected title = 'Client';
}
