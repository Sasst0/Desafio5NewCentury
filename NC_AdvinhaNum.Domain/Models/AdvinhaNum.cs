
namespace NC_AdvinhaNum.Domain.Models
{
    public class AdvinhaNum
    {
        public int Id { get; set; }
        public int COD_JOGADOR { get; set; }
        public int NUM_TENTATIVA { get; set; }
        public DateTime HorarioTentativa { get; set; }
        public string? Resultado { get; set; }
    }
}
