using JornadaMilhasV1.Modelos;

namespace JornadaMilhas.Test
{
    public class OfertaViagemConstrutor
    {
        [Fact]
        public void RetornaOfertaValidaQuandoDadosValidos()
        {
            // Cenário
            Rota rota = new Rota("OrigemTeste", "DestinoTeste");
            Periodo periodo = new Periodo(new DateTime(2024, 2, 1), new DateTime(2024, 2, 5));
            double preco = 100.0;
            var validacao = true;
            // Ação
            OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);
            // Validação
            Assert.Equal(validacao, oferta.EhValido);
        }

        [Fact]
        public void RetornaMsgErroQuandoRotaNula()
        {
            // Cenário - arrange
            Rota rota = null;
            Periodo periodo = new Periodo(new DateTime(2024, 2, 1), new DateTime(2024, 2, 5));
            double preco = 100.0;
            // Ação - act
            OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);
            // Validação - assert
            Assert.Contains("A oferta de viagem não possui rota ou período válidos.", oferta.Erros.Sumario);
            Assert.False(oferta.EhValido);
        }

        [Fact]
        public void RetornaOfertaInvalidaQuandoPeriodoInvalido()
        {
            // Cenário - arrange
            Rota rota = new Rota("Lafaiete", "Carrancas");
            Periodo periodo = new Periodo(new DateTime(2024, 9, 26), new DateTime(2024, 9, 21));
            double preco = 500.0;
            // Ação - act
            OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);
            // Validacao
            Assert.False(oferta.EhValido);
        }

        [Fact]
        public void RetornaMsgErroQuandoPrecoMenorQueZero()
        {
            // Cenário - arrange
            Rota rota = new Rota("Lafaiete", "Carrancas");
            Periodo periodo = new Periodo(new DateTime(2024, 9, 26), new DateTime(2024, 9, 30));
            double preco = -500.0;
            // Ação - act
            OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);
            // Validacao
            Assert.Contains("O preço da oferta de viagem deve ser maior que zero.",
                oferta.Erros.Sumario);
        }
    }
}