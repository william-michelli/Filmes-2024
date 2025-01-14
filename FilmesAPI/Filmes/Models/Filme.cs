namespace DotnetAPI.Models
{
    public partial class Filme
    {
        public int Id { get; set; }
        public string Nome { get; set; } = "";
        public string Genero { get; set; } = "";
        public int AnoLancamento { get; set; }
        public string Imagem { get; set; }
        public decimal MediaNota { get; set; }
        public string Streamings { get; set; } = "";
        public List<Comentario> Comentarios { get; set; }

        public Filme()
        {

        }

    }
}
