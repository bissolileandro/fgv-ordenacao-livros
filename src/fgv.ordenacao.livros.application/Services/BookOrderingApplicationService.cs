using AutoMapper;
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
    private readonly IMapper _mapper;

    public BookOrderingApplicationService(IOrderCriteriaProvider orderCriteriaProvider, IBooksOrdererFactory booksOrdererFactory, IMapper mapper)
    {
        _orderCriteriaProvider = orderCriteriaProvider;
        _booksOrdererFactory = booksOrdererFactory;
        _mapper = mapper;
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

        var books = _mapper.Map<IReadOnlyCollection<Book>>(request.Books);
        var orderer = _booksOrdererFactory.Create(criteria);
        var orderedBooks = orderer.Order(books);

        return new OrderBooksResponse
        {
            Books = _mapper.Map<IReadOnlyCollection<BookResponse>>(orderedBooks),
            AppliedCriteria = _mapper.Map<IReadOnlyCollection<string>>(criteria)
        };
    }

    private IReadOnlyCollection<SortCriterion> ParseCriteria(IEnumerable<SortCriterionRequest> criteria)
    {
        var parsedCriteria = _mapper.Map<IReadOnlyCollection<SortCriterion>>(criteria);

        if (parsedCriteria.Count == 0)
        {
            throw new OrdenacaoException("Ao menos um critério de ordenação deve ser informado.");
        }

        return parsedCriteria;
    }
}
