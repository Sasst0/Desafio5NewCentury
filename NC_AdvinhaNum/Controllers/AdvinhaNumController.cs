using Microsoft.AspNetCore.Mvc;
using NC_AdvinhaNum.Models;
using System.Diagnostics;

namespace NC_AdvinhaNum.Controllers
{
    public class AdvinhaNumController : Controller
    {
        #region Injeção de Dependencia
        private readonly ILogger<AdvinhaNumController> _logger;
        private readonly Contexto _contexto;
        public AdvinhaNumController(ILogger<AdvinhaNumController> logger, Contexto contexto)
        {
            _logger = logger;
            _contexto = contexto;
        }
        #endregion

        #region Index
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        #endregion

        #region Enum Resultado
        public enum Resultado
        {
            SUCCESS,
            WRONG
        }
        #endregion

        #region Salvar Tentativa
        public string TentarComc(string dif, int NUM_TENTATIVA, int COD_JOGADOR, string esc)
        {

            var numeroSecreto = GerarNumeroSecreto(dif);

            var tentativa = PegarTentativa(COD_JOGADOR);

            string retorno = numeroSecreto == NUM_TENTATIVA ? Resultado.SUCCESS.ToString() : Resultado.WRONG.ToString();

            var AdicionarNC = new AdvinhaNumerocs
            {
                COD_JOGADOR = COD_JOGADOR,
                NUM_TENTATIVA = tentativa.Count + 1,
                HorarioTentativa = DateTime.Now.ToLocalTime(),
                Resultado = retorno,
            };
            _contexto.AdvinhaNC.Add(AdicionarNC);
            _contexto.SaveChanges();
            return retorno;
        }
        #endregion

        #region Rank
        public static Rank[] Ranks = new Rank[]
       {
            new Rank(80, 100, "A"),
            new Rank(60, 79, "B"),
            new Rank(40, 59, "C"),
            new Rank(20, 39, "D"),
            new Rank(0, 19, "E"),
       };

        public string ObterRank(int numeroJogador)
        {
            var jogador = PegarTentativa(numeroJogador);
            var porcentagemDeVitoria = CalcularPorcentagemDeVitoria(jogador);

            foreach (var rank in Ranks)
            {
                if (porcentagemDeVitoria >= rank.Minimo && porcentagemDeVitoria <= rank.Maximo)
                {
                    return rank.Descricao;
                }
            }

            return "Sem Rank";
        }
        #endregion

        #region Popula histórico Detalhado
        public IEnumerable<AdvinhaNumerocs> HistoricoDetalhado(int COD_JOGADOR)
        {
            var pegarhistoricoDetalhado = new AdvinhaNumerocs
            {
                COD_JOGADOR = COD_JOGADOR,
            };

            var dadodetalhado = _contexto.AdvinhaNC.Where(mt => mt.COD_JOGADOR == COD_JOGADOR).ToList();

            var DadosLimpos = dadodetalhado.Select(rr => new AdvinhaNumerocs
            {
                COD_JOGADOR = rr.COD_JOGADOR,
                NUM_TENTATIVA = rr.NUM_TENTATIVA,
                DataFormatada = rr.HorarioTentativa.ToString("dd/MM/yyyy, HH:mm:ss"),
                Porcentagem = CalcularPorcentagemDeVitoriaJogador(rr.COD_JOGADOR),
                Resultado = rr.Resultado
            });
            return DadosLimpos;
        }
        #endregion

        #region Limpar Histórico
        public void LimpaHistorico()
        {
            var dados = _contexto.AdvinhaNC.ToList();
            foreach (var dado in dados)
            {
                _contexto.AdvinhaNC.Remove(dado);
                _contexto.SaveChanges();

            }
        }
        #endregion

        #region Calculo Porcentagem de Vitória
        public double CalcularPorcentagemDeVitoria(List<AdvinhaNumerocs> jogador)
        {
            int totalJogos = jogador.Count;
            if (totalJogos == 0)
                return 0;

            int sucesso = jogador.Count(rr => rr.Resultado == "SUCCESS");
            return (double)sucesso / totalJogos * 100;
        }
        public double CalcularPorcentagemDeVitoriaJogador(int jogador)
        {
            var dadosjogador = PegarTentativa(jogador);

            int sucesso = dadosjogador.Count(rr => rr.Resultado == "SUCCESS");
            return (double)sucesso / dadosjogador.Count * 100;
        }
        #endregion

        #region Busca Jogador
        public List<AdvinhaNumerocs> PegarTentativa(int COD_JOGADOR)
        {
            var pegarTentativa = new AdvinhaNumerocs
            {
                COD_JOGADOR = COD_JOGADOR,
            };

            return _contexto.AdvinhaNC.Where(mt => mt.COD_JOGADOR == COD_JOGADOR).ToList();

        }
        #endregion

        #region Busca Histórico Geral
        public IEnumerable<AdvinhaNumerocs> HistoricoGeral()
        {
            var pesquisa = _contexto.AdvinhaNC.ToList();
            var dadosAgrupados = pesquisa
                .GroupBy(rr => rr.COD_JOGADOR)
                .Select(group => new
                {
                    COD_JOGADOR = group.Key,
                    NUM_TENTATIVA = group.Count(),
                    HorarioTentativa = group.Select(gr => gr.HorarioTentativa).FirstOrDefault()
                });

            var DadosLimpos = dadosAgrupados.Select(rr => new AdvinhaNumerocs
            {
                COD_JOGADOR = rr.COD_JOGADOR,
                NUM_TENTATIVA = rr.NUM_TENTATIVA,
                DataFormatada = rr.HorarioTentativa.ToString("dd/MM/yyyy"),
                Porcentagem = CalcularPorcentagemDeVitoriaJogador(rr.COD_JOGADOR),
                Resultado = ObterRank(rr.COD_JOGADOR)
            });


            return DadosLimpos;
        }
        #endregion

        #region Gera Número Aleatório
        private int GerarNumeroSecreto(string dif)
        {
            Random random = new Random();
            int numero = 0;

            if (dif == "Facil")
            {
                numero = random.Next(1, 5);
                return numero;

            }
            else if (dif == "Medio")
            {
                numero = random.Next(1, 10);
                return numero;

            }
            else if (dif == "Dificil")
            {
                numero = random.Next(1, 20);
                return numero;

            }
            return numero;
        }
        #endregion
    }
}