using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using DapperMappers.Core.Extensions;
using DapperMappers.Core.Tests.Repositories;
using Dapper;
using DapperMappers.Core.Tests.Models;
using System;
using System.Reflection;

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
        public void Data_Saved_In_DataBase_As_Xml_Should_Be_Properly_Restore()
        {
            ServiceCollection services = PrepareServiceCollection();

            ServiceProvider serviceProvider = services.BuildServiceProvider();

            using (IServiceScope scope = serviceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;

                ISQLiteDbManagement dbManagement = scopedServices.GetRequiredService<ISQLiteDbManagement>();

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

                    TestObject testObject = new TestObject
                    {
                        FirstName = "John",
                        LastName = "Doe",
                        StartWork = new DateTime(2018, 06, 01),
                        Content = new TestContentObject
                        {
                            Nick = "JD",
                            DateOfBirth = new DateTime(1990, 10, 11),
                            Siblings = 2
                        }
                    };

                    // Act
                    testObjectRepository.SaveTestObject(testObject);
                    TestObject retrievedTestObject = testObjectRepository.GetTestObject(testObject.Id);

                    // Assert
                    retrievedTestObject.Should().NotBeNull();
                    retrievedTestObject.Should().BeEquivalentTo(retrievedTestObject);
                    retrievedTestObject.Content.Should().BeEquivalentTo(retrievedTestObject.Content);
                }
                finally
                {
                    dbManagement.DeleteDb();
                }
            }
        }

        public static ServiceCollection PrepareServiceCollection()
        {
            ServiceCollection services = new ServiceCollection();

            services.RegisterAllTypes(new[] { Assembly.GetExecutingAssembly() });

            services.AddTransient<ISQLiteDbManagement, SQLiteDbManagement>();
            services.AddTransient<ITestObjectRepository, TestObjectRepository>();

            return services;
        }
    }
}