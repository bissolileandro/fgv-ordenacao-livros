namespace fgv.ordenacao.livros.application.Contracts.Responses;

public sealed class OrderBooksResponse
{
    public IReadOnlyCollection<BookResponse> Books { get; init; } = [];
    public IReadOnlyCollection<string> AppliedCriteria { get; init; } = [];
}
