import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { AutorService } from '../../services/autor.service';
import { Autor } from '../../models/autor.model';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-autores',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './autores.html',
  styleUrl: './autores.css',
})
export class Autores implements OnInit {
  private autorService = inject(AutorService);

  public autores = signal<Autor[]>([]);

  public idAutorEditando = signal<number | null>(null);
  public nombreBusquedaAutor = signal<string>('');
  public mostrarForm = signal<boolean>(false);

  public paginaActual = signal<number>(1);
  public autoresPorPagina = signal<number>(10);

  public formularioAutor = new FormGroup({
    nombre: new FormControl('', [Validators.required]),
    nacionalidad: new FormControl('', [Validators.required])
  });

  public autoresFiltrados = computed(() => {
    const nombre = this.nombreBusquedaAutor().toLowerCase().trim();

    if (!nombre) return this.autores();

    return this.autores().filter(autor =>
      autor.nombre.toLowerCase().includes(nombre)
    );
  });

  public totalAutores = computed(() => {
    const totalItems = this.autoresFiltrados().length;
    return Math.ceil(totalItems / this.autoresPorPagina());
  });

  public autoresPaginados = computed(() => {
    const inicio = (this.paginaActual() - 1) * this.autoresPorPagina();
    const fin = inicio + this.autoresPorPagina();
    return this.autoresFiltrados().slice(inicio, fin);
  });

  ngOnInit() {
    this.obtenerAutores();
  }

  abrirForm() {
    this.idAutorEditando.set(null);
    this.formularioAutor.reset();
    this.mostrarForm.set(true);
  }

  obtenerAutores() {
    this.autorService.listarAutores().subscribe({
      next: (data) => this.autores.set(data),
      error: (err) => console.error('Error al conectar con la API:', err)
    });
  }

  actualizarBusquedaAutor(event: Event) {
    const elemento = event.target as HTMLInputElement;
    this.nombreBusquedaAutor.set(elemento.value);
    this.paginaActual.set(1);
  }

  seleccionarAutorParaEditar(autor: Autor) {
    this.idAutorEditando.set(autor.autorId);
    this.formularioAutor.patchValue({
      nombre: autor.nombre,
      nacionalidad: autor.nacionalidad
    });
    this.mostrarForm.set(true);
  }

  cancelarEdicionAutor() {
    this.idAutorEditando.set(null);
    this.formularioAutor.reset();
    this.mostrarForm.set(false);
  }

  guardarAutor() {
    if (this.formularioAutor.invalid) return;

    const idEditando = this.idAutorEditando();

    if (idEditando !== null) {
      const autorEditado: Autor = {
        autorId: idEditando,
        nombre: this.formularioAutor.value.nombre!,
        nacionalidad: this.formularioAutor.value.nacionalidad!
      };

      this.autorService.actualizarAutor(autorEditado).subscribe({
        next: () => {
          this.autores.update(lista =>
            lista.map(autor => autor.autorId === idEditando ? autorEditado : autor)
          );
          this.cancelarEdicionAutor();
          console.log('Autor actualizado con éxito');
        },
        error: (err) => console.error('Error al actualizar:', err)
      });
    }
    else {
      const nuevoAutor = this.formularioAutor.value as Autor;

      this.autorService.insertarAutor(nuevoAutor).subscribe({
        next: (autorCreado) => {
          this.autores.update(lista => [...lista, autorCreado]);
          this.formularioAutor.reset();
          console.log('Autor guardado con éxito');
        },
        error: (err) => console.error('Error al guardar el autor:', err)
      });
    }
  }

  eliminarAutor(id: number) {
    if (confirm('¿Estás seguro de que deseas eliminar este autor?')) {
      this.autorService.eliminarAutor(id).subscribe({
        next: () => {
          this.autores.update(lista => lista.filter(autor => autor.autorId !== id));
          console.log('Autor con ID ${id} eliminado correctamente');
        },
        error: (err) => console.error('Error al intentar eliminar el autor:', err)
      });
    }
  }

  siguientePagina() {
    if (this.paginaActual() < this.totalAutores()) {
      this.paginaActual.update(p => p + 1);
    }
  }

  paginaAnterior() {
    if (this.paginaActual() > 1) {
      this.paginaActual.update(p => p - 1);
    }
  }

  irAPagina(pagina: number) {
    this.paginaActual.set(pagina);
  }
}