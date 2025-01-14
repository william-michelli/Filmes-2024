import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { AcervoComponent } from './components/acervo/acervo.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, AcervoComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'acervo';
}
