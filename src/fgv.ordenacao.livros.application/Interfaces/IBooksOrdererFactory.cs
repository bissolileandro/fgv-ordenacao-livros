using fgv.ordenacao.livros.domain.Interfaces;
using fgv.ordenacao.livros.domain.ValueObjects;

namespace fgv.ordenacao.livros.application.Interfaces;

public interface IBooksOrdererFactory
{
    IBooksOrderer Create(IReadOnlyCollection<SortCriterion> criteria);
}
