using fgv.ordenacao.livros.application.Contracts.Requests;
using fgv.ordenacao.livros.application.Contracts.Responses;

namespace fgv.ordenacao.livros.application.Interfaces;

public interface IBookOrderingApplicationService
{
    OrderBooksResponse Order(OrderBooksRequest request);
}
