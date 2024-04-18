namespace NC_AdvinhaNum.Models
{
    public class AdvinhaNumerocs
    {
        public int Id { get; set; }
        public int COD_JOGADOR { get; set; }
        public int NUM_TENTATIVA { get; set; }
        public DateTime HorarioTentativa { get; set; }
        public string? Resultado { get; set; }
        public double? Porcentagem { get; set; }
        public string? DataFormatada { get; set; }
    }
}
