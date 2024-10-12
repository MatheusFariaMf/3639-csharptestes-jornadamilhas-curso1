using JornadaMilhasV1.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JornadaMilhas.Test
{
    public class OfertaViagemDesconto
    {
        [Fact]
        public void RetornaPrecoAtualizadoQuandoAplicadoDesconto()
        {
            // Arrange - cenário
            Rota rota = new Rota("Lafaiete", "Carrancas");
            Periodo periodo = new Periodo(new DateTime(2024, 9, 26), new DateTime(2024, 9, 21));
            double precoOriginal = 500.0;
            double desconto = 20;
            double precoComDesconto = precoOriginal - desconto;
            OfertaViagem oferta = new OfertaViagem(rota, periodo, precoOriginal);
            // Act - Ação
            oferta.Desconto = desconto;
            // Assert - Validação
            Assert.Equal(precoComDesconto, oferta.Preco);
        }

        [Theory]
        [InlineData(550)]
        [InlineData(1000)]
        [InlineData(500)]
        public void RetornaPrecoComSetentaPorCentoDescontoQuandoDescontoMaiorOuIgualPrecoOriginal(int desconto)
        {
            // Arrange - cenário
            Rota rota = new Rota("Lafaiete", "Carrancas");
            Periodo periodo = new Periodo(new DateTime(2024, 9, 26), new DateTime(2024, 9, 21));
            double precoOriginal = 500.0;
            double precoComDesconto = 500 - 500 * 0.7;
            OfertaViagem oferta = new OfertaViagem(rota, periodo, precoOriginal);
            // Act - Ação
            oferta.Desconto = desconto;
            // Assert - Validação
            Assert.Equal(precoComDesconto, oferta.Preco, 0.001);
        }

        [Theory]
        [InlineData(-20)]
        [InlineData(-1)]
        [InlineData(-500)]
        [InlineData(-2034)]
        [InlineData(0)]
        public void RetornaPrecoOriginalQuandoDescontoForNegativoOuZero(int desconto)
        {
            // Arrange - cenário
            Rota rota = new Rota("Lafaiete", "Carrancas");
            Periodo periodo = new Periodo(new DateTime(2024, 9, 20), new DateTime(2024, 9, 21));
            double precoOriginal = 500.0;
            OfertaViagem oferta = new OfertaViagem(rota, periodo, precoOriginal);
            // Act - Ação
            oferta.Desconto = desconto;
            // Assert - Validação
            Assert.Equal(precoOriginal, oferta.Preco);
        }
    }
}
