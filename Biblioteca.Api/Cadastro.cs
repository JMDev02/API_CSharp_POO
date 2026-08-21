using Biblioteca.Dominio;

namespace Biblioteca.Api;

public class Cadastro
{
    private readonly List<Leitor> _leitor =[];

    public IReadOnlyList<Leitor> Itens =>_leitor;

    public void Adicionar(Leitor leitor)
    {
        _leitor.Add(leitor);
    }

    public Leitor? BuscarPorId(int id)
    {
        return _leitor.FirstOrDefault(leitor=> leitor.Id == id);
    }
}