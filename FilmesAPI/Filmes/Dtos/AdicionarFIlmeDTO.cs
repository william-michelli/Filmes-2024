namespace DotnetAPI.Dtos
{
    public partial class AdicionarFIlmeDTO
    {
        public int Id { get; set; } = 0;

        public string Nome { get; set; } = "";

        public string Genero { get; set; } = "";

        public int AnoLancamento { get; set; }

        public string Imagem { get; set; } = "";

        public string Streamings { get; set; } = "";
    }
}
