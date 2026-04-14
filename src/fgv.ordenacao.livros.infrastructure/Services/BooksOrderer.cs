using fgv.ordenacao.livros.domain.Entities;
using fgv.ordenacao.livros.domain.Enums;
using fgv.ordenacao.livros.domain.Exceptions;
using fgv.ordenacao.livros.domain.Interfaces;
using fgv.ordenacao.livros.domain.ValueObjects;

namespace fgv.ordenacao.livros.infrastructure.Services;

public sealed class BooksOrderer : IBooksOrderer
{
    private static readonly IReadOnlyDictionary<SortField, Func<IOrderedEnumerable<Book>?, IEnumerable<Book>, SortDirection, IOrderedEnumerable<Book>>> OrderingStrategies =
        new Dictionary<SortField, Func<IOrderedEnumerable<Book>?, IEnumerable<Book>, SortDirection, IOrderedEnumerable<Book>>>
        {
            [SortField.Title] = (orderedBooks, source, direction) => Apply(orderedBooks, source, book => book.Title, direction),
            [SortField.Author] = (orderedBooks, source, direction) => Apply(orderedBooks, source, book => book.AuthorName, direction),
            [SortField.EditionYear] = (orderedBooks, source, direction) => Apply(orderedBooks, source, book => book.EditionYear, direction)
        };

    private readonly IReadOnlyCollection<SortCriterion> _criteria;

    public BooksOrderer(IReadOnlyCollection<SortCriterion> criteria)
    {
        _criteria = criteria;
    }

    public IReadOnlyCollection<Book> Order(IEnumerable<Book> books)
    {
        if (books is null)
        {
            throw new OrdenacaoException("O conjunto de livros não pode ser nulo.");
        }

        var materializedBooks = books.ToArray();

        if (materializedBooks.Length == 0)
        {
            return Array.Empty<Book>();
        }

        if (_criteria.Count == 0)
        {
            throw new OrdenacaoException("Ao menos um critério de ordenação deve ser informado.");
        }

        IOrderedEnumerable<Book>? orderedBooks = null;

        foreach (var criterion in _criteria)
        {
            orderedBooks = ApplyOrdering(orderedBooks, materializedBooks, criterion);
        }

        return orderedBooks?.ToArray() ?? materializedBooks;
    }

    private static IOrderedEnumerable<Book> ApplyOrdering(
        IOrderedEnumerable<Book>? orderedBooks,
        IEnumerable<Book> source,
        SortCriterion criterion)
    {
        if (!OrderingStrategies.TryGetValue(criterion.Field, out var orderingStrategy))
        {
            throw new OrdenacaoException($"Campo de ordenação não suportado: {criterion.Field}.");
        }

        return orderingStrategy(orderedBooks, source, criterion.Direction);
    }

    private static IOrderedEnumerable<Book> Apply<TKey>(
        IOrderedEnumerable<Book>? orderedBooks,
        IEnumerable<Book> source,
        Func<Book, TKey> keySelector,
        SortDirection direction)
    {
        if (orderedBooks is null)
        {
            return direction == SortDirection.Ascending
                ? source.OrderBy(keySelector)
                : source.OrderByDescending(keySelector);
        }

        return direction == SortDirection.Ascending
            ? orderedBooks.ThenBy(keySelector)
            : orderedBooks.ThenByDescending(keySelector);
    }
}
