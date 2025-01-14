using Azure;
using Dapper;
using DotnetAPI.Data;
using DotnetAPI.Dtos;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Globalization;
using System.Reflection;

namespace DotnetAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        DataContextDapper _dapper;

        public FilmeController(IConfiguration config)
        {
            _dapper = new DataContextDapper(config);
        }


        [HttpGet("TestarConexao")]
        public DateTime TestarConexao()
        {
            return _dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");
        }


        [HttpGet("BuscarFilmes")]
        public IEnumerable<Filme> BuscarFilmes()
        {
            List<Filme> filmes = _dapper.LoadFilmes<Filme>();

            // Faz a média das notas do filme
            foreach (Filme filme in filmes)
            {
                int quantidadeComentarios = filme.Comentarios.Count();
                decimal totalNota = 0;

                if (filme.Comentarios[0] != null)
                {
                    foreach (Comentario comentario in filme.Comentarios)
                    {
                        totalNota += comentario.Nota;
                    }

                    filme.MediaNota = totalNota / quantidadeComentarios;
                }
                else
                {
                    filme.MediaNota = 0;
                }
            }

            return filmes;
        }


        [HttpGet("BuscarFilme/{id}")]
        public Filme BuscarFilme(int id)
        {
            string sql = $"SELECT * FROM AcervoSchema.Filme WHERE Id={id}";
            Filme filme = _dapper.LoadDataSingle<Filme>(sql); ;
            return filme;
        }

        [HttpGet("BuscarFilmePorNome/{pesquisa}")]
        public IEnumerable<Filme> BuscarFilmePorNome(string pesquisa)
        {
            string sql = $"SELECT * FROM AcervoSchema.Filme WHERE Nome LIKE '%{pesquisa}%'";
            IEnumerable<Filme> filme = _dapper.LoadData<Filme>(sql);
            return filme;
        }


        [HttpPost("AdicionarFilme")]
        public IActionResult AdicionarFilme(AdicionarFIlmeDTO filme)
        {
            string sql = $@"INSERT INTO AcervoSchema.Filme (
                                [Nome],
                                [Genero],
                                [AnoLancamento],
                                [Imagem],
                                [Streamings]
                            ) 
                            VALUES (
                                '{filme.Nome}',
                                '{filme.Genero}',
                                '{filme.AnoLancamento}',
                                '{filme.Imagem}',
                                '{filme.Streamings}'
                            )";

            if (_dapper.ExecuteSql(sql))
            {
                return Ok();
            }

            throw new Exception("Falha ao adicionar filme");
        }



        [HttpPost("AdicionarComentario")]
        public IActionResult AdicionarComentario(AdicionarComentarioDTO comentario)
        {
            string sql = $@"INSERT INTO AcervoSchema.Comentario (
                                [FilmeId],
                                [Nota],
                                [Observacao]
                            ) 
                            VALUES (
                                '{comentario.FilmeId}',
                                '{comentario.Nota}',
                                '{comentario.Observacao}'
                            )";

            if (_dapper.ExecuteSql(sql))
            {
                return Ok();
            }

            throw new Exception("Falha ao adicionar comentario");
        }


         [HttpPut("EditarFilme")]
         public IActionResult EditarFilme(AdicionarFIlmeDTO filme)
         {
            string sql = $@"UPDATE AcervoSchema.Filme
                             SET [Nome]='{filme.Nome}',
                                 [Genero]='{filme.Genero}',
                                 [AnoLancamento]={filme.AnoLancamento},
                                 [Imagem]='{filme.Imagem}',
                                 [Streamings]='{filme.Streamings}'
                                 WHERE Id={filme.Id}";

             if (_dapper.ExecuteSql(sql))
             {
                 return Ok();
             }

             throw new Exception("Falha ao editar filme");
         }


        [HttpDelete("DeletarFilme/{id}")]
        public IActionResult DeletarFilme(int id)
        {
            string sql = $"DELETE FROM AcervoSchema.Filme WHERE Id={id}";

            IList<bool> resultados = new List<bool>();
            if (_dapper.ExecuteSql(sql))
            {
                return Ok();
            }

            throw new Exception("Falha em deletar filme");
        }

    }
}


