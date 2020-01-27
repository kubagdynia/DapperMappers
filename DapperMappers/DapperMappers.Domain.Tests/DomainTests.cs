using Dapper.CustomTypeHandlers.Extensions;
using DapperMappers.Domain.Models;
using DapperMappers.Domain.Repositories;
using DapperMappers.Domain.Tests.DbConnection;
using DbConnectionExtensions.DbConnection;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace DapperMappers.Domain.Tests
{
    public class Tests
    {
        [Test, Order(1)]
        public void Domain_Test_Should_Be_Ok()
        {
            1.Should().Equals(1);
        }

        [Test, Order(2)]
        public async Task Book_Stored_In_Database_Should_Be_Restored_Properly()
        {
            ServiceCollection services = PrepareServiceCollection();

            ServiceProvider serviceProvider = services.BuildServiceProvider();

            using (IServiceScope scope = serviceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;

                IBookRepository bookRepository = scopedServices.GetRequiredService<IBookRepository>();
                Book book = CreateTestBook();

                // Act
                await bookRepository.SaveBook(book);
                Book retrievedBook = await bookRepository.GetBook(book.Id);
                book.InternalId = retrievedBook.InternalId;

                // Assert
                retrievedBook.Should().NotBeNull();
                retrievedBook.Should().BeEquivalentTo(book);
                retrievedBook.Description.Should().BeEquivalentTo(book.Description);
                retrievedBook.Authors.Should().BeEquivalentTo(book.Authors);
                retrievedBook.TableOfContents.Should().BeEquivalentTo(book.TableOfContents);
            }
        }

        [Test, Order(3)]
        public async Task Deleting_A_Book_Should_Delete_The_Book_From_The_Database()
        {
            ServiceCollection services = PrepareServiceCollection();

            ServiceProvider serviceProvider = services.BuildServiceProvider();

            using (IServiceScope scope = serviceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;

                IBookRepository bookRepository = scopedServices.GetRequiredService<IBookRepository>();
                Book book = CreateTestBook();

                // Act
                await bookRepository.SaveBook(book);
                Book retrievedBook = await bookRepository.GetBook(book.Id);
                await bookRepository.DeleteBook(book.Id);
                Book retrievedDeletedBook = await bookRepository.GetBook(book.Id);

                book.InternalId = retrievedBook.InternalId;

                // Assert
                retrievedBook.Should().NotBeNull();
                retrievedBook.Should().BeEquivalentTo(book);
                retrievedDeletedBook.Should().BeNull();
            }
        }

        private static Book CreateTestBook()
        {
            return new Book
            {
                Id = Guid.NewGuid().ToString(),
                Title = "Test name",
                PageCount = 100,
                Isbn = "9788301000001",
                DateOfPublication = new DateTime(2019, 01, 01),
                Authors = new BookAuthors
                {
                    Authors = new List<Author>
                    {
                        new Author
                        {
                            Name = "First Test Author Name",
                            Description = "First Test Author Description"
                        },
                        new Author
                        {
                            Name = "Second Test Author Name",
                            Description = "Second Test Author Description"
                        }
                    }
                },
                TableOfContents = new BookTableOfContents
                {
                    Chapters = new List<Chapter>
                    {
                        new Chapter
                        {
                            Number = "1",
                            Name = "Chapter one",
                            Subsections = new List<Subsection>
                            {
                                new Subsection
                                {
                                    Number = "1",
                                    Name = "First subsection of chapter one"
                                }
                            }
                        },
                        new Chapter
                        {
                            Number = "2",
                            Name = "Chapter two",
                            Subsections = new List<Subsection>
                            {
                                new Subsection
                                {
                                    Number = "1",
                                    Name = "First subsection of chapter two"
                                },
                                new Subsection
                                {
                                    Number = "2",
                                    Name = "Second subsection of chapter two"
                                }
                            }
                        }
                    }
                },
                ShortDescription = "Test short description",
                Description = new BookDescription
                {
                    Learn = new Learn
                    {
                        Points = new List<string> { "First learn point", "Second learn point" }
                    },
                    About = "Test about",
                    Features = new Features
                    {
                        Points = new List<string> { "First feature point" }
                    }
                },
                Publisher = "Test publisher",
                Url = "https://www.test.com"
            };
        }

        private static ServiceCollection PrepareServiceCollection()
        {
            ServiceCollection services = new ServiceCollection();

            IConfiguration config = GetIConfiguration();
            services.AddSingleton(config);

            // Search the specified assembly and register all classes that implement IXmlObjectType and IJsonObjectType interfaces
            services.RegisterDapperCustomTypeHandlers(new[] { typeof(Book).Assembly });

            services.AddSingleton<ICommandQuery, CommandQuery>();
            
            // services.AddTransient<IDbConnectionFactory>(x =>
            //     ActivatorUtilities.CreateInstance<BookDbConnectionFactory>(x, "DefaultConnection"));
            
            services.AddTransient<IDbConnectionFactory>(x =>
                ActivatorUtilities.CreateInstance<BookDbConnectionFactory>(x, "DefaultConnection5"));
            
            //services.AddTransient<IBookRepository, BookRepository>();

            services.AddTransient<IBookRepository>(x =>
                ActivatorUtilities.CreateInstance<BookRepository>(x, "DefaultConnection5"));

            return services;
        }

        private static IConfiguration GetIConfiguration()
        {
            var myConfiguration = new Dictionary<string, string>
            {
                {"SQLiteConnectionStrings:DefaultConnection:DbFilename", $"BookDb_{Guid.NewGuid()}.sqlite"},
                {"SQLiteConnectionStrings:DefaultConnection:DeleteDbFileOnExit", "true"},
                
                {"SQLiteConnectionStrings:DefaultConnection5:DbFilename", $"BookDb5_{Guid.NewGuid()}.sqlite"},
                {"SQLiteConnectionStrings:DefaultConnection5:DeleteDbFileOnExit", "true"}
            };
            
            return new ConfigurationBuilder()
                .AddInMemoryCollection(myConfiguration)
                .Build();
        }
    }
}