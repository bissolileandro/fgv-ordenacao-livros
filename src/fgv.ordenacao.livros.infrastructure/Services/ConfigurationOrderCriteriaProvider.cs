using AutoMapper;
using fgv.ordenacao.livros.application.Interfaces;
using fgv.ordenacao.livros.domain.Exceptions;
using fgv.ordenacao.livros.domain.ValueObjects;
using fgv.ordenacao.livros.infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace fgv.ordenacao.livros.infrastructure.Services;

public sealed class ConfigurationOrderCriteriaProvider : IOrderCriteriaProvider
{
    private readonly OrderSettings _orderSettings;
    private readonly IMapper _mapper;

    public ConfigurationOrderCriteriaProvider(IOptions<OrderSettings> orderSettings, IMapper mapper)
    {
        _orderSettings = orderSettings.Value;
        _mapper = mapper;
    }

    public IReadOnlyCollection<SortCriterion> GetDefaultCriteria()
    {
        if (_orderSettings.Criteria.Count == 0)
        {
            throw new OrdenacaoException("Nenhum critério padrão de ordenação foi configurado.");
        }

        return _mapper.Map<IReadOnlyCollection<SortCriterion>>(_orderSettings.Criteria);
    }
}
