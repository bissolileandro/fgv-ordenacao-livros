using fgv.ordenacao.livros.application.Common;
using fgv.ordenacao.livros.application.Contracts.Requests;
using fgv.ordenacao.livros.application.Contracts.Responses;
using fgv.ordenacao.livros.application.Interfaces;
using fgv.ordenacao.livros.domain.Entities;
using fgv.ordenacao.livros.domain.Exceptions;
using fgv.ordenacao.livros.domain.ValueObjects;

namespace fgv.ordenacao.livros.application.Services;

public sealed class BookOrderingApplicationService : IBookOrderingApplicationService
{
    private readonly IOrderCriteriaProvider _orderCriteriaProvider;
    private readonly IBooksOrdererFactory _booksOrdererFactory;

    public BookOrderingApplicationService(IOrderCriteriaProvider orderCriteriaProvider, IBooksOrdererFactory booksOrdererFactory)
    {
        _orderCriteriaProvider = orderCriteriaProvider;
        _booksOrdererFactory = booksOrdererFactory;
    }

    public OrderBooksResponse Order(OrderBooksRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (request.Books is null)
        {
            throw new OrdenacaoException("O conjunto de livros não pode ser nulo.");
        }

        var criteria = request.Criteria is { Count: > 0 }
            ? ParseCriteria(request.Criteria)
            : _orderCriteriaProvider.GetDefaultCriteria();

        var books = request.Books.Select(book => new Book(book.Title, book.AuthorName, book.EditionYear)).ToArray();
        var orderer = _booksOrdererFactory.Create(criteria);
        var orderedBooks = orderer.Order(books);

        return new OrderBooksResponse
        {
            Books = orderedBooks.Select(book => new BookResponse
            {
                Title = book.Title,
                AuthorName = book.AuthorName,
                EditionYear = book.EditionYear
            }).ToArray(),
            AppliedCriteria = criteria.Select(SortCriterionMapper.Format).ToArray()
        };
    }

    private static IReadOnlyCollection<SortCriterion> ParseCriteria(IEnumerable<SortCriterionRequest> criteria)
    {
        var parsedCriteria = criteria.Select(criterion => new SortCriterion(
            SortCriterionMapper.ParseField(criterion.Field, "Campo de ordenação inválido"),
            SortCriterionMapper.ParseDirection(criterion.Direction, "Direção de ordenação inválida"))).ToArray();

        if (parsedCriteria.Length == 0)
        {
            throw new OrdenacaoException("Ao menos um critério de ordenação deve ser informado.");
        }

        return parsedCriteria;
    }
}
