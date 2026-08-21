using Biblioteca.Dominio;

// 1. Marina (15 anos) tenta levar DVD de 16 anos -> Deve dar erro
var marina = new Leitor("Marina", DateTime.Today.AddYears(-15));
var dvd16 = new Dvd("Filme de Terror", "Diretor A", 16);

try {
    var emp1 = new Emprestimo(marina, dvd16);
} catch (ExcecaoDominio ex) {
    Console.WriteLine($"Cena 1 OK: {ex.Message}");
}

// 2 e 3. Caio pega 3 itens, tenta o 4º (falha), devolve 1 e pega outro (sucesso)
var caio = new Leitor("Caio", DateTime.Today.AddYears(-20));
var l1 = new Livro("Livro 1", "Autor A");
var l2 = new Livro("Livro 2", "Autor B");
var l3 = new Livro("Livro 3", "Autor C");
var l4 = new Livro("Livro 4", "Autor D");

var e1 = new Emprestimo(caio, l1);
var e2 = new Emprestimo(caio, l2);
var e3 = new Emprestimo(caio, l3);

try {
    var e4 = new Emprestimo(caio, l4); // 4º item
} catch (ExcecaoDominio ex) {
    Console.WriteLine($"Cena 2 OK: {ex.Message}");
}

e1.RegistrarDevolucao(); // Devolve 1
var e4Sucesso = new Emprestimo(caio, l4); // Agora consegue
Console.WriteLine("Cena 3 OK: Caio devolveu 1 item e conseguiu pegar outro.");

// 4. Tentar emprestar exemplar que já saiu -> Deve dar erro
try {
    var empDuplo = new Emprestimo(marina, l4); // l4 está com o Caio
} catch (ExcecaoDominio ex) {
    Console.WriteLine($"Cena 4 OK: {ex.Message}");
}

// 5. Bug do Sr. Elias (multa congelada ao devolver)
var elias = new Leitor("Sr. Elias", DateTime.Today.AddYears(-60));
var rev = new Revista("Revista Semanal", "Editora X");
var empElias = new Emprestimo(elias, rev);

// Simulando devolução com 6 dias de atraso no passado
empElias.RegistrarDevolucao(empElias.PrazoLimite.AddDays(6)); 
decimal multaNoDiaDevolucao = empElias.MultaAtual;

// Duas semanas depois, a multa DEVE continuar sendo exatamente a mesma
Console.WriteLine($"Cena 5 OK: Multa gravada = R$ {multaNoDiaDevolucao}. Consultada semanas depois = R$ {empElias.MultaAtual}.");




var livroNovo = new Livro("O Cortiço", "Aluísio Azevedo");
var revistaNova = new Revista("Piauí", "Alvinegra");
Console.WriteLine($"Cena 6 - {livroNovo.Titulo} e o Id {livroNovo.Id}, " +
                  $"{revistaNova.Titulo} e o Id {revistaNova.Id}");

var pessoaNova = new Leitor("Zenaide", new DateTime(1968, 4, 2));
var outraPessoa = new Leitor("Elias", new DateTime(1955, 9, 17));
Console.WriteLine($"Cena 7 - {pessoaNova.Nome} e o Id {pessoaNova.Id}, " +
                  $"{outraPessoa.Nome} e o Id {outraPessoa.Id}");