import { Component, signal} from '@angular/core';
import { Autores } from "./components/autores/autores";
import { Libros } from "./components/libros/libros";
import { Ejemplares } from './components/ejemplares/ejemplares';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [Autores, Libros, Ejemplares],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class AppComponent{
  public vistaActual = signal<string>('autores');

  cambiarVista(nombreVista: string){
    this.vistaActual.set(nombreVista);
  }
}