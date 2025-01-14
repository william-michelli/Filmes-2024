import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import {RouterModule} from '@angular/router';
import { AcervoService } from '../../Services/acervo.service';
import { Filme, Comentario } from '../../Models/acervo.models';
import {  ChangeDetectorRef } from '@angular/core';

@Component({
  selector: 'app-acervo',
  standalone: true,
  imports: [FormsModule, RouterModule],
  templateUrl: './acervo.component.html',
  styleUrl: './acervo.component.css'
})
export class AcervoComponent {
  filmes: Filme[] = [];

  novoFilme:Filme = {
    id: 0,
    nome: "",
    genero: "",
    anoLancamento:0,
    imagem: "",
    mediaNota: 0,
    streamings: "",
    comentarios: []
  }

  editFilme:Filme = {
    id: 0,
    nome: "",
    genero: "",
    anoLancamento:0,
    imagem: "",
    mediaNota: 0,
    streamings: "",
    comentarios: []
  }

  titulo: String = "";
  comentarios: Array<Comentario> = [];
  novoComentario:Comentario = {
    id: 0,
    filmeId: 0,
    nota: 0,
    observacao: "",
  }

  netflixCheckbox: Boolean = false
  primeCheckbox: Boolean = false
  hbomaxCheckbox: Boolean = false
  disneyplusCheckbox: Boolean = false

  streamings: string = ""

  pesquisaFilme: string = ""

  constructor(private acervoService: AcervoService) {}

  ngOnInit(): void{
    this.buscarTodosFilmes()
  }

  buscarTodosFilmes(){
    this.acervoService.buscarTodosfilmes()
    .subscribe({
      next: (filmes) => {
        this.filmes = filmes;
      }
    })
  }

  adicionarFilme(){
    if(this.netflixCheckbox == true){
      this.streamings += "Netflix;"
    }
    if(this.primeCheckbox == true){
      this.streamings += "Prime Video;"
    }
    if(this.hbomaxCheckbox == true){
      this.streamings += "HBO Max;"
    }
    if(this.disneyplusCheckbox == true){
      this.streamings += "Disney Plus;"
    }

    this.novoFilme.streamings = this.streamings;

    this.acervoService.adicionarFilme(this.novoFilme)
      .subscribe({
        next: () => {
          this.buscarTodosFilmes()
        }
      })

    this.novoFilme = <Filme>{};
    this.netflixCheckbox = false;
    this.primeCheckbox = false;
    this.hbomaxCheckbox = false;
    this.disneyplusCheckbox = false;
  }

  editarFilme(id: number, filme: Filme){
    if(this.netflixCheckbox == true){
      this.streamings += "Netflix;"
    }
    if(this.primeCheckbox == true){
      this.streamings += "Prime Video;"
    }
    if(this.hbomaxCheckbox == true){
      this.streamings += "HBO Max;"
    }
    if(this.disneyplusCheckbox == true){
      this.streamings += "Disney Plus;"
    }

    filme.streamings = this.streamings;

    this.acervoService.editarFilme(id, filme)
      .subscribe({
        next: () => {
          this.buscarTodosFilmes()
        }
      })
  }

  deletarFilme(filme: Filme){
    if (confirm(`Deseja deletar o filme ${filme.nome}?`)) {
      this.acervoService.deletarFilme(filme.id)
      .subscribe({
        next: () => {
          this.buscarTodosFilmes()
        }
      })
    } 
  }

  
  validarInput(comentario: Comentario): Boolean{

    if(comentario.nota > 0 && comentario.nota <= 5){
      return true
    }else{
      return false
    }
  }


  adicionarComentario() {

    if(this.validarInput(this.novoComentario)){
      this.acervoService.adicionarComentario(this.novoComentario)
      .subscribe({
        next: () => {
  
          this.buscarTodosFilmes()
  
          //Espera um pouco atualizar a lista de filmes pra atualizar a lista de comentarios
          setTimeout(() => 
            {
              this.preencheComentarioComDados(
                this.filmes.filter((filme) => {
                  return filme.id == this.novoComentario.filmeId
                })[0]
              )
            },
          1000);
        }
      })
    }else{
      alert("Nota entre 1 e 5")
    }

  }

  atualizarLista(eventData: Event) {
    let valor = (<HTMLInputElement>eventData.target).value

    this.filmes = this.filmes.filter((filme) => {
      return filme.nome.toLowerCase() == valor.toLowerCase()
    })
  }

  preencheEditarComDados(filme: Filme){
    this.editFilme.id = filme.id;
    this.editFilme.nome = filme.nome;
    this.editFilme.genero = filme.genero;
    this.editFilme.anoLancamento = filme.anoLancamento;
    this.editFilme.imagem = filme.imagem;
    this.netflixCheckbox = filme.streamings.includes("Netflix")
    this.primeCheckbox = filme.streamings.includes("Prime Video")
    this.hbomaxCheckbox = filme.streamings.includes("HBO Max")
    this.disneyplusCheckbox = filme.streamings.includes("Disney Plus")
  }

  preencheComentarioComDados(filme: Filme){
    this.novoComentario.filmeId = filme.id;
    this.titulo = filme.nome;
    this.comentarios = filme.comentarios;
  }

  verificaCampoNumero(event: Event){

    if(this.novoComentario.nota > 5|| this.novoComentario.nota < 1){
      this.novoComentario.nota = 0
    }
  }
}
