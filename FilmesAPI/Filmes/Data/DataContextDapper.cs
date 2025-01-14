using Dapper;
using DotnetAPI.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.FileSystemGlobbing;
using System.Data;
using System.Data.Common;

namespace DotnetAPI.Data
{
    public class DataContextDapper
    {
        private readonly IConfiguration _config;

        public DataContextDapper(IConfiguration config)
        {
            _config = config;
        }

        public IEnumerable<T> LoadData<T>(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return dbConnection.Query<T>(sql);
        }

        public T LoadDataSingle<T>(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return dbConnection.QuerySingle<T>(sql);
        }

        public bool ExecuteSql(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return dbConnection.Execute(sql) > 0;
        }

        public List<Filme> LoadFilmes<T>()
        {
            IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            string sql = @"SELECT * FROM AcervoSchema.Filme AS Filmes
                            LEFT JOIN AcervoSchema.Comentario AS Comentario
                            ON Comentario.FilmeId = Filmes.Id";

            var filmeDictionary = new Dictionary<int, Filme>();
            var list = dbConnection.Query<Filme, Comentario, Filme>(sql,(filme, comentario) =>
            {
                Filme filmeEntry;

                if (!filmeDictionary.TryGetValue(filme.Id, out filmeEntry))
                {
                    filmeEntry = filme;
                    filmeEntry.Comentarios = new List<Comentario>();
                    filmeDictionary.Add(filmeEntry.Id, filmeEntry);
                }

                filmeEntry.Comentarios.Add(comentario);
                return filmeEntry;
            })
            .Distinct()
            .ToList();

            return list;
        }
    }
}
