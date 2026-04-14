using fgv.ordenacao.livros.domain.Entities;

namespace fgv.ordenacao.livros.domain.Interfaces;

public interface IBooksOrderer
{
    IReadOnlyCollection<Book> Order(IEnumerable<Book> books);
}
