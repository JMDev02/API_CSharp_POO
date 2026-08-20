namespace Biblioteca.Dominio;

public class Dvd(string titulo,string autor, int faixaEtaria) : ItemAcervo(titulo, autor)
{
   public int FaixaEtaria { get; } = faixaEtaria;
    public override int PrazoDevolucao => 7;
    public override decimal MultaDiaAtrasado => 2.00m;
}