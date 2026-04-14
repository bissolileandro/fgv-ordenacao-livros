namespace fgv.ordenacao.livros.application.Contracts.Requests;

public sealed class OrderBooksRequest
{
    public IList<BookRequest>? Books { get; set; }
    public IList<SortCriterionRequest>? Criteria { get; set; }
}
