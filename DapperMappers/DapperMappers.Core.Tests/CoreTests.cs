using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using DapperMappers.Core.Extensions;
using DapperMappers.Core.Tests.Repositories;
using DapperMappers.Core.Tests.Models;
using System;
using System.Reflection;
using System.Collections.Generic;
using DapperMappers.Core.DbConnection;
using DapperMappers.Core.Tests.DbConnection;

namespace DapperMappers.Core.Tests
{
    public class Tests
    {
        [Test]
        public void Should_Be_Ok()
        {
            1.Should().Equals(1);
        }

        [Test]
        public void Xml_Data_Saved_In_DataBase_Should_Be_Properly_Restored()
        {
            ServiceCollection services = PrepareServiceCollection();

            ServiceProvider serviceProvider = services.BuildServiceProvider();

            using (IServiceScope scope = serviceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;

                ITestObjectRepository testObjectRepository = scopedServices.GetRequiredService<ITestObjectRepository>();

                TestXmlObject testObject = new TestXmlObject
                {
                    FirstName = "John",
                    LastName = "Doe",
                    StartWork = new DateTime(2018, 06, 01),
                    Content = new TestXmlContentObject
                    {
                        Nick = "JD",
                        DateOfBirth = new DateTime(1990, 10, 11),
                        Siblings = 2,
                        FavoriteDaysOfTheWeek = new List<string>
                            {
                                "Friday",
                                "Saturday"
                            },
                        FavoriteNumbers = new List<int> { -502, 444, 0, 777777 }
                    }
                };

                // Act
                testObjectRepository.SaveTestObject(testObject);
                TestXmlObject retrievedTestObject = testObjectRepository.GetTestObject(testObject.Id);

                // Assert
                retrievedTestObject.Should().NotBeNull();
                retrievedTestObject.Should().BeEquivalentTo(testObject);
                retrievedTestObject.Content.Should().BeEquivalentTo(testObject.Content);
            }
        }

        [Test]
        public void Json_Data_Saved_In_DataBase_Should_Be_Properly_Restored()
        {
            ServiceCollection services = PrepareServiceCollection();

            ServiceProvider serviceProvider = services.BuildServiceProvider();

            using (IServiceScope scope = serviceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;

                ITestObjectRepository testObjectRepository = scopedServices.GetRequiredService<ITestObjectRepository>();

                TestJsonObject testObject = new TestJsonObject
                {
                    FirstName = "John",
                    LastName = "Doe",
                    StartWork = new DateTime(2018, 06, 01),
                    Content = new TestJsonContentObject
                    {
                        Nick = "JD",
                        DateOfBirth = new DateTime(1990, 10, 11),
                        Siblings = 2,
                        FavoriteDaysOfTheWeek = new List<string>
                            {
                                "Friday",
                                "Saturday",
                                "Sunday"
                            },
                        FavoriteNumbers = new List<int> { 10, 15, 1332, 5555 }
                    }
                };

                // Act
                testObjectRepository.SaveTestJsonObject(testObject);
                TestJsonObject retrievedTestObject = testObjectRepository.GetTestJsonObject(testObject.Id);

                // Assert
                retrievedTestObject.Should().NotBeNull();
                retrievedTestObject.Should().BeEquivalentTo(testObject);
                retrievedTestObject.Content.Should().BeEquivalentTo(testObject.Content);
            }
        }

        private static ServiceCollection PrepareServiceCollection()
        {
            ServiceCollection services = new ServiceCollection();

            // Search the specified assembly and register all classes that implement IXmlObjectType and IJsonObjectType interfaces
            services.RegisterAllTypes(new[] { Assembly.GetExecutingAssembly() });

            services.AddTransient<IDbConnectionFactory, SqliteConnectionFactory>();
            services.AddTransient<ITestObjectRepository, TestObjectRepository>();

            return services;
        }
    }
}