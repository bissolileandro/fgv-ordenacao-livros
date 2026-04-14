using AutoMapper;
using fgv.ordenacao.livros.application.Contracts.Requests;
using fgv.ordenacao.livros.application.Services;
using fgv.ordenacao.livros.domain.Exceptions;
using fgv.ordenacao.livros.infrastructure.Configuration;
using fgv.ordenacao.livros.infrastructure.Services;
using Microsoft.Extensions.Options;

namespace fgv.ordenacao.livros.tests;

public sealed class BookOrderingTests
{
    [Fact]
    public void Should_Order_By_Title_Ascending()
    {
        var service = CreateService();

        var response = service.Order(new OrderBooksRequest
        {
            Books = GetBooks(),
            Criteria =
            [
                new SortCriterionRequest { Field = "Title", Direction = "Ascending" }
            ]
        });

        Assert.Equal(new[]
        {
            "Head First Design Patterns",
            "Internet & World Wide Web: How to Program",
            "Java How to Program",
            "Patterns of Enterprise Application Architecture"
        }, response.Books.Select(book => book.Title).ToArray());
    }

    [Fact]
    public void Should_Order_By_Author_Ascending_Then_Title_Descending()
    {
        var service = CreateService();

        var response = service.Order(new OrderBooksRequest
        {
            Books = GetBooks(),
            Criteria =
            [
                new SortCriterionRequest { Field = "Author", Direction = "Ascending" },
                new SortCriterionRequest { Field = "Title", Direction = "Descending" }
            ]
        });

        Assert.Equal(new[]
        {
            "Java How to Program",
            "Internet & World Wide Web: How to Program",
            "Head First Design Patterns",
            "Patterns of Enterprise Application Architecture"
        }, response.Books.Select(book => book.Title).ToArray());
    }

    [Fact]
    public void Should_Order_By_Edition_Descending_Then_Author_Descending_Then_Title_Ascending()
    {
        var service = CreateService();

        var response = service.Order(new OrderBooksRequest
        {
            Books = GetBooks(),
            Criteria =
            [
                new SortCriterionRequest { Field = "EditionYear", Direction = "Descending" },
                new SortCriterionRequest { Field = "Author", Direction = "Descending" },
                new SortCriterionRequest { Field = "Title", Direction = "Ascending" }
            ]
        });

        Assert.Equal(new[]
        {
            "Internet & World Wide Web: How to Program",
            "Java How to Program",
            "Head First Design Patterns",
            "Patterns of Enterprise Application Architecture"
        }, response.Books.Select(book => book.Title).ToArray());
    }

    [Fact]
    public void Should_Use_Default_Configuration_When_No_Criteria_Is_Provided()
    {
        var service = CreateService();

        var response = service.Order(new OrderBooksRequest
        {
            Books = GetBooks()
        });

        Assert.Equal(new[]
        {
            "Head First Design Patterns",
            "Internet & World Wide Web: How to Program",
            "Java How to Program",
            "Patterns of Enterprise Application Architecture"
        }, response.Books.Select(book => book.Title).ToArray());
    }

    [Fact]
    public void Should_Throw_When_Books_Is_Null()
    {
        var service = CreateService();

        Assert.Throws<OrdenacaoException>(() => service.Order(new OrderBooksRequest
        {
            Books = null,
            Criteria =
            [
                new SortCriterionRequest { Field = "Title", Direction = "Ascending" }
            ]
        }));
    }

    [Fact]
    public void Should_Return_Empty_When_Books_Is_Empty()
    {
        var service = CreateService();

        var response = service.Order(new OrderBooksRequest
        {
            Books = [],
            Criteria =
            [
                new SortCriterionRequest { Field = "Title", Direction = "Ascending" }
            ]
        });

        Assert.Empty(response.Books);
    }

    private static BookOrderingApplicationService CreateService()
    {
        var orderSettings = Options.Create(new OrderSettings
        {
            Criteria =
            [
                new OrderCriterionSettings { Field = "Title", Direction = "Ascending" },
                new OrderCriterionSettings { Field = "Author", Direction = "Ascending" }
            ]
        });

        var mapperConfiguration = new MapperConfiguration(configuration =>
        {
            configuration.AddMaps(typeof(BookOrderingApplicationService).Assembly);
            configuration.AddMaps(typeof(ConfigurationOrderCriteriaProvider).Assembly);
        });

        var mapper = mapperConfiguration.CreateMapper();
        var criteriaProvider = new ConfigurationOrderCriteriaProvider(orderSettings, mapper);
        var factory = new BooksOrdererFactory();
        return new BookOrderingApplicationService(criteriaProvider, factory, mapper);
    }

    private static List<BookRequest> GetBooks()
    {
        return
        [
            new BookRequest { Title = "Java How to Program", AuthorName = "Deitel & Deitel", EditionYear = 2007 },
            new BookRequest { Title = "Patterns of Enterprise Application Architecture", AuthorName = "Martin Fowler", EditionYear = 2002 },
            new BookRequest { Title = "Head First Design Patterns", AuthorName = "Elisabeth Freeman", EditionYear = 2004 },
            new BookRequest { Title = "Internet & World Wide Web: How to Program", AuthorName = "Deitel & Deitel", EditionYear = 2007 }
        ];
    }
}
