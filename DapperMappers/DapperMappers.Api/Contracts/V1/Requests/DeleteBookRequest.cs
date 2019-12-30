using Microsoft.AspNetCore.Mvc;
using System;

namespace DapperMappers.Api.Contracts.V1.Requests
{
    public class DeleteBookRequest
    {
        /// <summary>
        /// Book id
        /// </summary>
        [FromRoute(Name = "id")]
        public Guid Id { get; set; }
    }
}
