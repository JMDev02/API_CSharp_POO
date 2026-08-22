using Biblioteca.Api;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


var acervo = new Acervo();
var cadastro = new Cadastro();
Seed.Popular(acervo, cadastro);

app.MapGet("/", () => Results.Redirect("/itens"));

app.MapGet("/itens",()=>acervo.Itens);

app.Run();
