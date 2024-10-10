using JornadaMilhasV1.Modelos;

namespace JornadaMilhas.Test
{
    public class OfertaViagemConstrutor
    {
        [Fact]
        public void RetornaOfertaValidaQuandoDadosValidos()
        {
            // Cen�rio
            Rota rota = new Rota("OrigemTeste", "DestinoTeste");
            Periodo periodo = new Periodo(new DateTime(2024, 2, 1), new DateTime(2024, 2, 5));
            double preco = 100.0;
            var validacao = true;
            // A��o
            OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);
            // Valida��o
            Assert.Equal(validacao, oferta.EhValido);
        }

        [Fact]
        public void RetornaMsgErroQuandoRotaNula()
        {
            // Cen�rio - arrange
            Rota rota = null;
            Periodo periodo = new Periodo(new DateTime(2024, 2, 1), new DateTime(2024, 2, 5));
            double preco = 100.0;
            // A��o - act
            OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);
            // Valida��o - assert
            Assert.Contains("A oferta de viagem n�o possui rota ou per�odo v�lidos.", oferta.Erros.Sumario);
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

        [Fact]
        public void RetornaMsgErroQuandoPrecoMenorQueZero()
        {
            // Cen�rio - arrange
            Rota rota = new Rota("Lafaiete", "Carrancas");
            Periodo periodo = new Periodo(new DateTime(2024, 9, 26), new DateTime(2024, 9, 30));
            double preco = -500.0;
            // A��o - act
            OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);
            // Validacao
            Assert.Contains("O pre�o da oferta de viagem deve ser maior que zero.",
                oferta.Erros.Sumario);
        }
    }
}