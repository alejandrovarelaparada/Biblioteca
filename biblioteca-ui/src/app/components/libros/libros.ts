import { CommonModule } from '@angular/common';
import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { LibroService } from '../../services/libro.service';
import { AutorService } from '../../services/autor.service';
import { Libro } from '../../models/libro.model';
import { Autor } from '../../models/autor.model';

@Component({
  selector: 'app-libros',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './libros.html',
  styleUrl: './libros.css',
})
export class Libros implements OnInit {
  private libroService = inject(LibroService);
  private autorService = inject(AutorService);

  public libros = signal<Libro[]>([]);
  public autores = signal<Autor[]>([]);

  public idLibroEditando = signal<number | null>(null);
  public tituloBusquedaLibro = signal<string>('');

  public formularioLibro = new FormGroup({
    titulo: new FormControl('', [Validators.required]),
    sinopsis: new FormControl('', [Validators.required]),
    autorId: new FormControl('', [Validators.required])
  });

  public librosFiltrados = computed(() => {
    const listaAutores = this.autores();
    const listaLibros = this.libros();
    const termino = this.tituloBusquedaLibro().toLowerCase().trim();

    const mapeados = listaLibros.map(libro => {
      const autor = listaAutores.find(a => a.autorId === libro.autorId);
      return { ...libro, autorNombre: autor ? autor.nombre : 'Sin autor asignado' };
    });

    if (!termino) return mapeados;
    return mapeados.filter(libro => libro.titulo.toLowerCase().includes(termino));
  });

  ngOnInit() {
    this.obtenerAutores();
    this.obtenerLibros();
  }

  obtenerAutores() {
    this.autorService.listarAutores().subscribe({
      next: (data) => this.autores.set(data)
    });
  }

  obtenerLibros() {
    this.libroService.listarLibros().subscribe({
      next: (data) => this.libros.set(data),
      error: (err) => console.error('Error al conectar con la API:', err)
    });
  }

  actualizarBusquedaLibro(event: Event) {
    const elemento = event.target as HTMLInputElement;
    this.tituloBusquedaLibro.set(elemento.value);
  }

  seleccionarLibroParaEditar(libro: Libro) {
    this.idLibroEditando.set(libro.libroId);
    this.formularioLibro.patchValue({
      titulo: libro.titulo,
      sinopsis: libro.sinopsis,
      autorId: libro.autorId.toString()
    });
  }

  cancelarEdicionLibro() {
    this.idLibroEditando.set(null);
    this.formularioLibro.reset();
    this.formularioLibro.get('autorId')?.setValue('');
  }

  guardarLibro() {
    if (this.formularioLibro.invalid) return;

    const idEditando = this.idLibroEditando();

    const datosLibro: Libro = {
      libroId: idEditando !== null ? idEditando : 0,
      titulo: this.formularioLibro.value.titulo!,
      sinopsis: this.formularioLibro.value.sinopsis!,
      autorId: Number(this.formularioLibro.value.autorId!)
    };

    if (idEditando !== null) {
      this.libroService.actualizarLibro(datosLibro).subscribe({
        next: () => {
          this.libros.update(lista =>
            lista.map(libro => libro.libroId === idEditando ? datosLibro : libro)
          );
          this.cancelarEdicionLibro();
          console.log('Libro actualizado con éxito');
        },
        error: (err) => console.error('Error al actualizar:', err)
      });
    }
    else {
      this.libroService.insertarLibro(datosLibro).subscribe({
        next: (libroCreado) => {
          this.libros.update(lista => [...lista, libroCreado]);
          this.formularioLibro.reset();
          this.formularioLibro.get('autorId')?.setValue('');
          console.log('Libro guardado con éxito');
        },
        error: (err) => console.error('Error al guardar el libro:', err)
      });
    }
  }

  eliminarLibro(id: number) {
    if (confirm('¿Estás seguro de que deseas eliminar este libro?')) {
      this.libroService.eliminarLibro(id).subscribe({
        next: () => {
          this.libros.update(lista => lista.filter(libro => libro.libroId !== id));
          console.log('Libro con ID ${id} eliminado correctamente');
        },
        error: (err) => console.error('Error al intentar eliminar el libro:', err)
      });
    }
  }
}