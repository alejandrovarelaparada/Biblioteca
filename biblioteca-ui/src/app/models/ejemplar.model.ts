export interface Ejemplar {
    ejemplarId: number;
    isbn: string;
    edicion: number;
    estado: string;
    libroId: number;
    libroTitulo?: string;
}