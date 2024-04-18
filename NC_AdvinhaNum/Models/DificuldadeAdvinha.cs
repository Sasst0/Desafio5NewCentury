using static NC_AdvinhaNum.Controllers.AdvinhaNumController;

namespace NC_AdvinhaNum.Models
{
    public class DificuldadeAdvinha
    {
        public string? Dificuldade { get; set; }
        public string? escolha { get; set; }
        public int Tentativa { get; set; }
        public int numJog { get; set; }

    }
}
