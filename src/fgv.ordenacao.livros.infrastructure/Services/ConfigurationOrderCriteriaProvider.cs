using fgv.ordenacao.livros.application.Common;
using fgv.ordenacao.livros.application.Interfaces;
using fgv.ordenacao.livros.domain.Exceptions;
using fgv.ordenacao.livros.domain.ValueObjects;
using fgv.ordenacao.livros.infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace fgv.ordenacao.livros.infrastructure.Services;

public sealed class ConfigurationOrderCriteriaProvider : IOrderCriteriaProvider
{
    private readonly OrderSettings _orderSettings;

    public ConfigurationOrderCriteriaProvider(IOptions<OrderSettings> orderSettings)
    {
        _orderSettings = orderSettings.Value;
    }

    public IReadOnlyCollection<SortCriterion> GetDefaultCriteria()
    {
        if (_orderSettings.Criteria.Count == 0)
        {
            throw new OrdenacaoException("Nenhum critério padrão de ordenação foi configurado.");
        }

        return _orderSettings.Criteria.Select(criterion => new SortCriterion(
            SortCriterionMapper.ParseField(criterion.Field, "Campo configurado inválido"),
            SortCriterionMapper.ParseDirection(criterion.Direction, "Direção configurada inválida"))).ToArray();
    }
}
