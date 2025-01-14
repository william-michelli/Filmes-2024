import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Filme, Comentario } from '../Models/acervo.models';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AcervoService {
  baseApiUrl: string = "http://localhost:5066"

  constructor(private http: HttpClient) { }

  buscarTodosfilmes(): Observable<Filme[]> {
    return this.http.get<Filme[]>(this.baseApiUrl + "/Filme/BuscarFilmes")
  }

  adicionarFilme(novoFilme: Filme): Observable<Filme>{
    novoFilme.id = 0;
    return this.http.post<Filme>(this.baseApiUrl + "/Filme/AdicionarFilme/", novoFilme)
  }

  editarFilme(id: number, filme: Filme): Observable<Filme>{
    return this.http.put<Filme>(this.baseApiUrl + "/Filme/EditarFilme/", filme)
  }

  deletarFilme(id: number): Observable<Filme>{
    return this.http.delete<Filme>(this.baseApiUrl + "/Filme/DeletarFilme/" + id)
  }

  adicionarComentario(novoComentario: Comentario): Observable<Comentario>{
    novoComentario.id = 0;
    return this.http.post<Comentario>(this.baseApiUrl + "/Filme/AdicionarComentario/", novoComentario)
  }
}