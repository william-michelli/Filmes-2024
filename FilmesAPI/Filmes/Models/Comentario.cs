namespace DotnetAPI.Models
{
    public partial class Comentario
    {
        public int Id { get; set; }
        public int FilmeId { get; set; }
        public int Nota { get; set; }
        public string Observacao { get; set; } = "";


        public Comentario()
        {

        }

    }
}
