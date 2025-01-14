namespace DotnetAPI.Dtos
{
    public partial class AdicionarComentarioDTO
    {
        public int FilmeId { get; set; }

        public int Nota { get; set; }

        public string Observacao { get; set; } = "";
    }
}
