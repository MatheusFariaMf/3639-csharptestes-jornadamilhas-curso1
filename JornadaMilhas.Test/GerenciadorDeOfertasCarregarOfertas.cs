using JornadaMilhasV1.Modelos;
using JornadaMilhasV1.Gerencidor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JornadaMilhas.Test
{
    public class GerenciadorDeOfertasCarregarOfertas
    {
        [Fact]
        public void RetornaTresOfertasAdicionadasAListaDeOfertas()
        {
            // Arrange - cenário
            List<OfertaViagem> listaOfertas = new List<OfertaViagem>();
            GerenciadorDeOfertas gerenciador = new GerenciadorDeOfertas(listaOfertas);

            // Act - Ação
            gerenciador.CarregarOfertas();
            // Assert - Validação
            Assert.Equal(3, gerenciador.ListaOfertas.Count);
        }
        
    }
}
