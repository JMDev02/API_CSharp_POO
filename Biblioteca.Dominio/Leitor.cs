namespace Biblioteca.Dominio;

public class Leitor
{
    private readonly List<Emprestimo> _emprestimosAtivos = new();

    public string Nome { get; private set; }
    public DateTime DataNascimento { get; private set; }

    public Leitor(string nome, DateTime dataNascimento)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ExcecaoDominio("O nome do leitor é obrigatório.");

        Nome = nome;
        DataNascimento = dataNascimento;
    }

    public int CalcularIdade(DateTime? dataReferencia = null)
    {
        DateTime refData = dataReferencia ?? DateTime.Today;
        int idade = refData.Year - DataNascimento.Year;
        if (DataNascimento.Date > refData.AddYears(-idade)) idade--;
        return idade;
    }

    public bool PodeEmprestar() => _emprestimosAtivos.Count < 3;

    public bool TemIdadeSuficiente(int faixaEtaria) => CalcularIdade() >= faixaEtaria;

    public void AdicionarEmprestimo(Emprestimo emprestimo)
    {
        _emprestimosAtivos.Add(emprestimo);
    }

    public void RemoverEmprestimo(Emprestimo emprestimo)
    {
        _emprestimosAtivos.Remove(emprestimo);
    }
}