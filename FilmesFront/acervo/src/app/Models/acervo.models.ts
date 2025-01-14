export interface Filme {
    id: number,
    nome: string,
    genero: string,
    anoLancamento: number,
    imagem: string,
    mediaNota: number,
    streamings: string,
    comentarios: Array<Comentario>
}

export interface Comentario {
    id: number,
    filmeId: number,
    nota: number,
    observacao: string
}