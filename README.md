# FGV - Serviço de Ordenação de Livros

## Visão geral

Solução em .NET organizada em camadas, com foco em legibilidade, baixo acoplamento e fácil manutenção.

## Estrutura

- `src/fgv.ordenacao.livros.api`: camada de apresentação
- `src/fgv.ordenacao.livros.application`: caso de uso de ordenação
- `src/fgv.ordenacao.livros.domain`: contratos e elementos de negócio
- `src/fgv.ordenacao.livros.infrastructure`: configuração e implementação do ordenamento
- `tests/fgv.ordenacao.livros.tests`: testes automatizados
- `docs`: documento de projeto, diagrama de classes e fluxo do serviço

## Endpoints

### Healthcheck

`GET /api/healthcheck`

### Ordenação

`POST /api/book-ordering/sort`

Exemplo de corpo:

```json
{
  "books": [
    { "title": "Java How to Program", "authorName": "Deitel & Deitel", "editionYear": 2007 },
    { "title": "Patterns of Enterprise Application Architecture", "authorName": "Martin Fowler", "editionYear": 2002 },
    { "title": "Head First Design Patterns", "authorName": "Elisabeth Freeman", "editionYear": 2004 },
    { "title": "Internet & World Wide Web: How to Program", "authorName": "Deitel & Deitel", "editionYear": 2007 }
  ],
  "criteria": [
    { "field": "Author", "direction": "Ascending" },
    { "field": "Title", "direction": "Descending" }
  ]
}
```

Se `criteria` não for informado, a API usa os critérios padrão do `appsettings.json`.

## Como executar

```bash
dotnet restore
dotnet build fgv-ordenacao-livros.sln
dotnet test tests/fgv.ordenacao.livros.tests/fgv.ordenacao.livros.tests.csproj
dotnet run --project src/fgv.ordenacao.livros.api/fgv.ordenacao.livros.api.csproj
```
