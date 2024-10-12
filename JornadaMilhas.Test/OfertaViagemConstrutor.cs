using JornadaMilhasV1.Modelos;

namespace JornadaMilhas.Test
{
    public class OfertaViagemConstrutor
    {
        [Theory]
        [InlineData("", null, "2024-01-01", "2024-01-05", 0, false)]
        [InlineData("OrigemTeste", "DestinoTeste", "2024-02-01", "2024-02-05", 100.0, true)]
        [InlineData("Lafaiete", "Carrancas", "2024-02-07", "2024-02-01", 100.0, false)]
        [InlineData("OrigemTeste", "DestinoTeste", "2024-02-01", "2024-02-05", -100.0, false)]
        public void RetornaEhValidoDeAcordoComDadosEntrada(string origem, string destino, string dataIda,
            string dataVolta, double preco, bool validacao)
        {
            // Cen�rio - arrange
            Rota rota = new Rota(origem, destino);
            Periodo periodo = new Periodo(DateTime.Parse(dataIda), DateTime.Parse(dataVolta));
            // A��o - act
            OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);
            // Valida��o - assert
            Assert.Equal(validacao, oferta.EhValido);
        }

        [Theory]
        [InlineData( "", "","A oferta de viagem n�o possui rota ou per�odo v�lidos.")]
        [InlineData("", "destinozada", "A oferta de viagem n�o possui Origem da rota v�lida.")]
        [InlineData("origemzada", "", "A oferta de viagem n�o possui Destino da rota v�lida.")]
        public void RetornaMsgErroQuandoRotaNulaOuStringVazia(string origem, string destino, string msgErro)
        {
            // Cen�rio - arrange
            Rota rota = new Rota(origem, destino);
            if (origem == "" && destino == "")
                rota = null;
            Periodo periodo = new Periodo(new DateTime(2024, 2, 1), new DateTime(2024, 2, 5));
            double preco = 100.0;
            // A��o - act
            OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);
            // Valida��o - assert
            Assert.Contains(msgErro, oferta.Erros.Sumario);
            Assert.False(oferta.EhValido);
        }

        [Fact]
        public void RetornaOfertaInvalidaQuandoPeriodoInvalido()
        {
            // Cen�rio - arrange
            Rota rota = new Rota("Lafaiete", "Carrancas");
            Periodo periodo = new Periodo(new DateTime(2024, 9, 26), new DateTime(2024, 9, 21));
            double preco = 500.0;
            // A��o - act
            OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);
            // Validacao
            Assert.False(oferta.EhValido);
        }

        [Theory]
        [InlineData("Carrancas", "Lafaiete", "2024-02-01", "2024-02-05", 100.0, "")]
        [InlineData("Carrancas", "Lafaiete", "2024-02-01", "2024-02-05", -100.0, "O pre�o da oferta de viagem deve ser maior que zero.")]
        [InlineData("Carrancas", "Lafaiete", "2024-02-01", "2024-02-05", 0, "O pre�o da oferta de viagem deve ser maior que zero.")]
        public void RetornaSeHouveMsgErroDePrecoMenorOuIgualZero(string origem, string destino, string dataIni,
            string dataFim, double preco, string fraseErro)
        {
            // Cen�rio - arrange
            Rota rota = new Rota(origem, destino);
            Periodo periodo = new Periodo(DateTime.Parse(dataIni), DateTime.Parse(dataFim));
            // A��o - act
            OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);
            // Validacao - assert
            Assert.Contains(fraseErro,
                oferta.Erros.Sumario);
        }

        [Fact]
        public void RetornaQuatroErrosDeValidacaoQuandoRotaPeriodoEPrecoSaoInvalidos()
        {

            // Cen�rio - arrange
            Rota rota = new Rota("", "");
            Periodo periodo = new Periodo(new DateTime(2024, 9, 26), new DateTime(2024, 9, 21));
            double preco = -500.0;
            int quantidadeEsperada = 4;
            // A��o - act
            OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

            // Validacao - assert
            Assert.Equal(quantidadeEsperada, oferta.Erros.Count());
        }
    }
}