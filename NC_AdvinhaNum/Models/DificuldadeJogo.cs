namespace NC_AdvinhaNum.Models
{
    public class DificuldadeJogo
    {
        public string Nome { get; set; }
        public int TotalTentativas { get; set; }

        public DificuldadeJogo(string nome, int totalTentativas)
        {
            Nome = nome;
            TotalTentativas = totalTentativas;
        }
    }
}
