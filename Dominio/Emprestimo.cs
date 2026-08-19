namespace Biblioteca.Dominio;

public class Emprestimo
{
    public ItemAcervo Item { get; private set; }
    public Leitor Leitor { get; private set; }
    public DateTime DataEmprestimo { get; private set; } = DateTime.Now;
    public DateTime PrazoLimite { get; }
    public DateTime? DataDevolucaoEfetiva { get; private set; }
    public decimal? ValorMultaFinal { get; private set; }

    public Emprestimo(Leitor leitor, ItemAcervo item)
    {
        if (!leitor.PodeEmprestar())
            throw new ExcecaoDominio("Leitor atingo o limite maximo de 3 itens emprestados.");

        if (item is Dvd dvd && !leitor.TemIdadeSuficiente(dvd.FaixaEtaria))
            throw new ExcecaoDominio($"Leitor nao tem idade minima ({dvd.FaixaEtaria} anos) para este DVD.");

        item.MarcarComoEmprestado();
        Item = item;
        Leitor = leitor;
        PrazoLimite = DataEmprestimo.AddDays(item.PrazoDevolucao);
        
        leitor.AdicionarEmprestimo(this);
    }

    public decimal MultaAtual
    {
        get
        {
            // Se ja foi devolvido, retorna o valor que foi congelado no dia da devolução
            if (ValorMultaFinal.HasValue) 
                return ValorMultaFinal.Value;

            return Item.CalcularMulta(QtdDiasAtrasados);
        }
    }

    public int QtdDiasAtrasados
    {
        get
        {
            DateTime dataReferencia = DataDevolucaoEfetiva ?? DateTime.Today;
            TimeSpan dias = dataReferencia - PrazoLimite;
            return dias.Days > 0 ? dias.Days : 0;
        }
    }

    public void RegistrarDevolucao(DateTime? dataDevolucao = null)
    {
        if (DataDevolucaoEfetiva != null)
            throw new ExcecaoDominio("Este emprestimo ja foi devolvido.");

        DataDevolucaoEfetiva = dataDevolucao ?? DateTime.Today;
        ValorMultaFinal = Item.CalcularMulta(QtdDiasAtrasados);
        
        Item.MarcarComoDevolvido();
        Leitor.RemoverEmprestimo(this);
    }
}