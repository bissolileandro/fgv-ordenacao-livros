using fgv.ordenacao.livros.application.Interfaces;
using fgv.ordenacao.livros.domain.Interfaces;
using fgv.ordenacao.livros.domain.ValueObjects;

namespace fgv.ordenacao.livros.infrastructure.Services;

public sealed class BooksOrdererFactory : IBooksOrdererFactory
{
    public IBooksOrderer Create(IReadOnlyCollection<SortCriterion> criteria)
    {
        return new BooksOrderer(criteria);
    }
}
