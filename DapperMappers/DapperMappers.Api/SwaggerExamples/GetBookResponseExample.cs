using DapperMappers.Api.Contracts.V1.Resources;
using DapperMappers.Api.Contracts.V1.Responses;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;

namespace DapperMappers.Api.SwaggerExamples
{
    public class GetBookResponseExample : IExamplesProvider<GetBookResponse>
    {
        public GetBookResponse GetExamples()
        {
            BookResource bookResource = new BookResource
            {
                Id = Guid.NewGuid().ToString(),
                Title = "Hands-On Domain-Driven Design with .NET Core",
                PageCount = 446,
                Isbn = "9781788834094",
                DateOfPublication = new DateTime(2019, 04, 30),
                Authors = new BookAuthorsResource
                {
                    Authors = new List<AuthorResource>
                    {
                        new()
                        {
                            Name = "Alexey Zimarev",
                            Description = "Alexey Zimarev is a ..."
                        }
                    }
                },
                TableOfContents = new BookTableOfContentsResource
                {
                    Chapters = new List<ChapterResource>
                    {
                        new()
                        {
                            Number = "1",
                            Name = "Why Domain-Driven Design?",
                            Subsections = new List<SubsectionResource>
                            {
                                new()
                                {
                                    Number = "1",
                                    Name = "Understanding the problem"
                                },
                                new()
                                {
                                    Number = "2",
                                    Name = "Dealing with complexity"
                                }
                            }
                        },
                        new()
                        {
                            Number = "2",
                            Name = "Language and Context"
                        },
                        new()
                        {
                            Number = "3",
                            Name = "EventStorming"
                        },
                    }
                },
                ShortDescription = "Solve complex business problems...",
                Description = new BookDescriptionResource
                {
                    Learn = new LearnResource
                    {
                        Points = new List<string>
                        {
                            "Discover and...",
                            "Avoid common...",
                        }
                    },
                    About = @"Developers across the world...",
                    Features = new FeaturesResource
                    {
                        Points = new List<string>
                        {
                            "Apply DDD principles...",
                            "Learn how DDD...",
                        }
                    }
                },
                Publisher = "Packt",
                Url = "https://www.packtpub.com/application-development/hands-domain-driven-design-net-core"
            };

            GetBookResponse response = new GetBookResponse(bookResource, StatusCodes.Status200OK);
            return response;
        }
    }
}
