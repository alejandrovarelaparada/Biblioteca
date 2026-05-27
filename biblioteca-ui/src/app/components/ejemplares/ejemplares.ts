import { CommonModule } from '@angular/common';
import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { LibroService } from '../../services/libro.service';
import { EjemplarService } from '../../services/ejemplar.service';
import { Ejemplar } from '../../models/ejemplar.model';
import { Libro } from '../../models/libro.model';

@Component({
  selector: 'app-ejemplares',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './ejemplares.html',
  styleUrl: './ejemplares.css',
})
export class Ejemplares implements OnInit {
  private ejemplarService = inject(EjemplarService);
  private libroService = inject(LibroService);

  public ejemplares = signal<Ejemplar[]>([]);
  public libros = signal<Libro[]>([]);

  public idEjemplarEditando = signal<number | null>(null);
  public terminoBusquedaEjemplar = signal<string>('');
  public mostrarForm = signal<boolean>(false);

  public formularioEjemplar = new FormGroup({
    isbn: new FormControl('', [Validators.required]),
    edicion: new FormControl('', [Validators.required]),
    estado: new FormControl('', [Validators.required]),
    libroId: new FormControl('', [Validators.required])
  });

  public listaEstados = ['Disponible', 'Prestado', 'En Mantenimiento', 'Perdido'];

  public ejemplaresFiltrados = computed(() => {
    const listaEjemplares = this.ejemplares();
    const listaLibros = this.libros();
    const termino = this.terminoBusquedaEjemplar().toLowerCase().trim();

    const mapeados = listaEjemplares.map(ejemplar => {
      const libro = listaLibros.find(l => l.libroId === ejemplar.libroId);
      return { ...ejemplar, libroTitulo: libro ? libro.titulo : 'Sin libro asignado' };
    });

    if (!termino) return mapeados;
    return mapeados.filter(ejemplar =>
      ejemplar.isbn.toLowerCase().includes(termino) ||
      ejemplar.libroTitulo.toLowerCase().includes(termino)
    );
  });

  ngOnInit() {
    this.obtenerLibros();
    this.obtenerEjemplares();
  }

  abrirForm() {
    this.idEjemplarEditando.set(null);
    this.formularioEjemplar.reset();
    this.formularioEjemplar.get('libroId')?.setValue('');
    this.formularioEjemplar.get('estado')?.setValue('');
    this.mostrarForm.set(true);
  }

  obtenerLibros() {
    this.libroService.listarLibros().subscribe({
      next: (data) => this.libros.set(data)
    });
  }

  obtenerEjemplares() {
    this.ejemplarService.listarEjemplares().subscribe({
      next: (data) => this.ejemplares.set(data),
      error: (err) => console.error('Error al conectar con la API:', err)
    });
  }

  actualizarBusquedaEjemplar(event: Event) {
    const elemento = event.target as HTMLInputElement;
    this.terminoBusquedaEjemplar.set(elemento.value);
  }

  seleccionarEjemplarParaEditar(ejemplar: Ejemplar) {
    this.idEjemplarEditando.set(ejemplar.libroId);
    this.formularioEjemplar.patchValue({
      isbn: ejemplar.isbn,
      edicion: ejemplar.edicion.toString(),
      estado: ejemplar.estado,
      libroId: ejemplar.libroId.toString()
    });
    this.mostrarForm.set(true);
  }

  cancelarEdicionEjemplar() {
    this.idEjemplarEditando.set(null);
    this.formularioEjemplar.reset();
    this.formularioEjemplar.get('libroId')?.setValue('');
    this.formularioEjemplar.get('estado')?.setValue('');
    this.mostrarForm.set(false);
  }

  guardarEjemplar() {
    if (this.formularioEjemplar.invalid) return;

    const idEditando = this.idEjemplarEditando();

    const datosEjemplar: Ejemplar = {
      ejemplarId: idEditando !== null ? idEditando : 0,
      isbn: this.formularioEjemplar.value.isbn!,
      edicion: Number(this.formularioEjemplar.value.edicion!),
      estado: this.formularioEjemplar.value.estado!,
      libroId: Number(this.formularioEjemplar.value.libroId!)
    };

    if (idEditando !== null) {
      this.ejemplarService.actualizarEjemplar(datosEjemplar).subscribe({
        next: () => {
          this.ejemplares.update(lista =>
            lista.map(ejemplar => ejemplar.libroId === idEditando ? datosEjemplar : ejemplar)
          );
          this.cancelarEdicionEjemplar();
          console.log('Ejemplar actualizado con éxito');
        },
        error: (err) => console.error('Error al actualizar:', err)
      });
    }
    else {
      this.ejemplarService.insertarEjemplar(datosEjemplar).subscribe({
        next: (ejemplarCreado) => {
          this.ejemplares.update(lista => [...lista, ejemplarCreado]);
          this.formularioEjemplar.reset();
          this.formularioEjemplar.get('libroId')?.setValue('');
          this.formularioEjemplar.get('estado')?.setValue('');
          console.log('Ejemplar guardado con éxito');
        },
        error: (err) => console.error('Error al guardar el ejemplar:', err)
      });
    }
  }

  eliminarEjemplar(id: number) {
    if (confirm('¿Estás seguro de que deseas eliminar este ejemplar?')) {
      this.ejemplarService.eliminarEjemplar(id).subscribe({
        next: () => {
          this.ejemplares.update(lista => lista.filter(ejemplar => ejemplar.ejemplarId !== id));
          console.log('Ejemplar con ID ${id} eliminado correctamente');
        },
        error: (err) => console.error('Error al intentar eliminar el ejemplar:', err)
      });
    }
  }
}