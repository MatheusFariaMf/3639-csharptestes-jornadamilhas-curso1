using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JornadaMilhasV1.Validador;

namespace JornadaMilhasV1.Modelos;

public class OfertaViagem: Valida
{
    public const double DESCONTO_MAXIMO = 0.7;
    private double desconto;

    public int Id { get; set; }
    public Rota Rota { get; set; } 
    public Periodo Periodo { get; set; }
    public double Preco { get; set; }
    public double Desconto 
    { 
        get => desconto;
        set 
        {
            desconto = value;

            if (desconto < 0)
                Preco = Preco;
            else if (desconto >= Preco)
            {
                Preco *= (1 - DESCONTO_MAXIMO);
            }
            else
                Preco -= desconto;
            
        }  
    }

    public OfertaViagem(Rota rota, Periodo periodo, double preco)
    {
        Rota = rota;
        Periodo = periodo;
        Preco = preco;
        Desconto = 0;
        Validar();
    }

    public override string ToString()
    {
        return $"Origem: {Rota.Origem}, Destino: {Rota.Destino}, Data de Ida: {Periodo.DataInicial.ToShortDateString()}, Data de Volta: {Periodo.DataFinal.ToShortDateString()}, Preço: {Preco:C}";
    }

    protected override void Validar()
    {
        if (!Periodo.EhValido)
        {
            Erros.RegistrarErro(Periodo.Erros.Sumario);
        }
        if (Rota == null || Periodo == null)
        {
            Erros.RegistrarErro("A oferta de viagem não possui rota ou período válidos.");
        } 
        if(Rota != null && Rota.Origem == string.Empty)
        {
            Erros.RegistrarErro("A oferta de viagem não possui Origem da rota válida.");
        }
        if (Rota != null && Rota.Destino == string.Empty)
        {
            Erros.RegistrarErro("A oferta de viagem não possui Destino da rota válida.");
        }
        if (Preco <= 0)
        {
            Erros.RegistrarErro("O preço da oferta de viagem deve ser maior que zero.");
        }
    }
}
