using DapperMappers.Api.Resources;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;

namespace DapperMappers.Api.SwaggerExamples
{
    public class CreateBookResourceExample : IExamplesProvider<CreateBookResource>
    {
        public CreateBookResource GetExamples()
        {
            CreateBookResource createBookResource = new CreateBookResource
            {
                Title = "Hands-On Domain-Driven Design with .NET Core",
                PageCount = 446,
                Isbn = "9781788834094",
                DateOfPublication = new DateTime(2019, 04, 30),
                Authors = new BookAuthorsResource
                {
                    Authors = new List<AuthorResource>
                    {
                        new AuthorResource
                        {
                            Name = "Alexey Zimarev",
                            Description = "Alexey Zimarev is a software architect with a present focus on domain models, Domain-Driven Design (DDD), event sourcing, message-driven systems and microservices, coaching, and mentoring. Alexey is also a contributor to several open source projects, such as RestSharp and MassTransit, and is the organizer of the DDD Norway meetup."
                        }
                    }
                },
                TableOfContents = new BookTableOfContentsResource
                {
                    Chapters = new List<ChapterResource>
                    {
                        new ChapterResource
                        {
                            Number = "1",
                            Name = "Why Domain-Driven Design?",
                            Subsections = new List<SubsectionResource>
                            {
                                new SubsectionResource
                                {
                                    Number = "1",
                                    Name = "Understanding the problem"
                                },
                                new SubsectionResource
                                {
                                    Number = "2",
                                    Name = "Dealing with complexity"
                                },
                                new SubsectionResource
                                {
                                    Number = "3",
                                    Name = "Knowledge"
                                },
                                new SubsectionResource
                                {
                                    Number = "4",
                                    Name = "Summary"
                                },
                                new SubsectionResource
                                {
                                    Number = "5",
                                    Name = "Further reading"
                                }
                            }
                        },
                        new ChapterResource
                        {
                            Number = "2",
                            Name = "Language and Context"
                        },
                        new ChapterResource
                        {
                            Number = "2",
                            Name = "Language and Context"
                        },
                        new ChapterResource
                        {
                            Number = "3",
                            Name = "EventStorming"
                        },
                        new ChapterResource
                        {
                            Number = "4",
                            Name = "Designing the Model"
                        },
                        new ChapterResource
                        {
                            Number = "5",
                            Name = "Implementing the Model"
                        }
                    }
                },
                ShortDescription = "Solve complex business problems by understanding users better, finding the right problem to solve, and building lean event-driven systems to give your customers what they really want",
                Description = new BookDescriptionResource
                {
                    Learn = new LearnResource
                    {
                        Points = new List<string>
                        {
                            "Discover and resolve domain complexity together with business stakeholders",
                            "Avoid common pitfalls when creating the domain model",
                            "Study the concept of Bounded Context and aggregate",
                            "Design and build temporal models based on behavior and not only data",
                            "Explore benefits and drawbacks of Event Sourcing",
                            "Get acquainted with CQRS and to-the-point read models with projections",
                            "Practice building one-way flow UI with Vue.js",
                            "Understand how a task-based UI conforms to DDD principles"
                        }
                    },
                    About = @"Developers across the world are rapidly adopting DDD principles to deliver powerful results when writing software that deals with complex business requirements.
This book will guide you in involving business stakeholders when choosing the software you are planning to build for them.
By figuring out the temporal nature of behavior-driven domain models, you will be able to build leaner, more agile, and modular systems.

You’ll begin by uncovering domain complexity and learn how to capture the behavioral aspects of the domain language.
You will then learn about EventStorming and advance to creating a new project in .NET Core 2.1; you’ll also and write some code to transfer your events from sticky notes to C#.
The book will show you how to use aggregates to handle commands and produce events.
As you progress, you’ll get to grips with Bounded Contexts, Context Map, Event Sourcing, and CQRS.
After translating domain models into executable C# code, you will create a frontend for your application using Vue.js.
In addition to this, you’ll learn how to refactor your code and cover event versioning and migration essentials.

By the end of this DDD book, you will have gained the confidence to implement the DDD approach in your organization and be able to explore new techniques that complement what you’ve learned from the book.",
                    Features = new FeaturesResource
                    {
                        Points = new List<string>
                        {
                            "Apply DDD principles using modern tools such as EventStorming, Event Sourcing, and CQRS",
                            "Learn how DDD applies directly to various architectural styles such as REST, reactive systems, and microservices",
                            "Empower teams to work flexibly with improved services and decoupled interactions"
                        }
                    }
                },
                Publisher = "Packt",
                Url = "https://www.packtpub.com/application-development/hands-domain-driven-design-net-core"
            };

            return createBookResource;
        }
    }
}
