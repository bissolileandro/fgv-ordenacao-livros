namespace fgv.ordenacao.livros.application.Contracts.Responses;

public sealed class BookResponse
{
    public string Title { get; init; } = string.Empty;
    public string AuthorName { get; init; } = string.Empty;
    public int EditionYear { get; init; }
}
