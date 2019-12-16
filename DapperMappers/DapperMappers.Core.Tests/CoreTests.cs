using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using DapperMappers.Core.Extensions;
using DapperMappers.Core.Tests.Repositories;
using Dapper;
using DapperMappers.Core.Tests.Models;
using System;
using System.Reflection;
using System.Collections.Generic;

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

                ISqLiteDbManagement dbManagement = scopedServices.GetRequiredService<ISqLiteDbManagement>();

                try
                {
                    dbManagement.CreateDb(conn =>
                    {
                        conn.Execute(
                            @"create table Test_Objects
                              (
                                 ID                                  integer primary key AUTOINCREMENT,
                                 FirstName                           varchar(100) not null,
                                 LastName                            varchar(100) not null,
                                 StartWork                           datetime not null,
                                 Content                             TEXT
                              )");
                    });

                    ITestObjectRepository testObjectRepository = scopedServices.GetRequiredService<ITestObjectRepository>();
                    testObjectRepository.ConnectToDb(dbManagement);

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
                finally
                {
                    dbManagement.DeleteDb();
                }
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

                ISqLiteDbManagement dbManagement = scopedServices.GetRequiredService<ISqLiteDbManagement>();

                try
                {
                    dbManagement.CreateDb(conn =>
                    {
                        conn.Execute(
                            @"create table Test_Objects
                              (
                                 ID                                  integer primary key AUTOINCREMENT,
                                 FirstName                           varchar(100) not null,
                                 LastName                            varchar(100) not null,
                                 StartWork                           datetime not null,
                                 Content                             TEXT
                              )");
                    });

                    ITestObjectRepository testObjectRepository = scopedServices.GetRequiredService<ITestObjectRepository>();
                    testObjectRepository.ConnectToDb(dbManagement);

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
                finally
                {
                    dbManagement.DeleteDb();
                }
            }
        }

        private static ServiceCollection PrepareServiceCollection()
        {
            ServiceCollection services = new ServiceCollection();

            // Search the specified assembly and register all classes that implement IXmlObjectType and IJsonObjectType interfaces
            services.RegisterAllTypes(new[] { Assembly.GetExecutingAssembly() });

            services.AddTransient<ISqLiteDbManagement, SqLiteDbManagement>();
            services.AddTransient<ITestObjectRepository, TestObjectRepository>();

            return services;
        }
    }
}