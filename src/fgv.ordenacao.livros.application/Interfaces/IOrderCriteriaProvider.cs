using fgv.ordenacao.livros.domain.ValueObjects;

namespace fgv.ordenacao.livros.application.Interfaces;

public interface IOrderCriteriaProvider
{
    IReadOnlyCollection<SortCriterion> GetDefaultCriteria();
}
