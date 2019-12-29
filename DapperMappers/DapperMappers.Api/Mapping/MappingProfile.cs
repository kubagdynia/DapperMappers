using AutoMapper;
using DapperMappers.Api.Contracts.V1.Requests;
using DapperMappers.Api.Resources;
using DapperMappers.Domain.Models;

namespace DapperMappers.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to Resource
            CreateMap<Subsection, SubsectionResource>();
            CreateMap<Chapter, ChapterResource>();
            CreateMap<BookTableOfContents, BookTableOfContentsResource>();

            CreateMap<Features, FeaturesResource>();
            CreateMap<Learn, LearnResource>();
            CreateMap<BookDescription, BookDescriptionResource>();

            CreateMap<Author, AuthorResource>();
            CreateMap<BookAuthors, BookAuthorsResource>();

            CreateMap<Book, BookResource>();

            // Resource to Domain
            CreateMap<SubsectionResource, Subsection>();
            CreateMap<ChapterResource, Chapter>();
            CreateMap<BookTableOfContentsResource, BookTableOfContents>();

            CreateMap<FeaturesResource, Features>();
            CreateMap<LearnResource, Learn>();
            CreateMap<BookDescriptionResource, BookDescription>();

            CreateMap<AuthorResource, Author>();
            CreateMap<BookAuthorsResource, BookAuthors>();

            CreateMap<BookResource, Book>();
            CreateMap<AddBookRequest, Book>();
        }
    }
}
