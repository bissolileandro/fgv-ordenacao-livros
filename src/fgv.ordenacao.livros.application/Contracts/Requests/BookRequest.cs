namespace fgv.ordenacao.livros.application.Contracts.Requests;

public sealed class BookRequest
{
    public string Title { get; set; } = string.Empty;
    public string AuthorName { get; set; } = string.Empty;
    public int EditionYear { get; set; }
}
