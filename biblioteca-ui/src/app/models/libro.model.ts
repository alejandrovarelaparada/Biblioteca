export interface Libro {
    libroId: number;
    titulo: string;
    sinopsis: string;
    autorId: number;
    autorNombre?: string;
}