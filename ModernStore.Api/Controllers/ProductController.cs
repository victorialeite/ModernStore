using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModernStore.Domain.Repositories;

namespace ModernStore.Api.Controllers
{
    public class ProductController : Controller
    {
        // Requisições Http serão direcionadas para os controllers
        // Requisições: CRUD - Post, Get, Put (variação: Patch), Delete

        private readonly IProductRepository _repository;

        public ProductController(IProductRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("v1/products")]
        [AllowAnonymous]
        public IActionResult Get()
        {
            return Ok(_repository.Get());
        }

        //[HttpGet]
        //[Route("v1/products/{id}")]
        //public IActionResult Get(Guid id)
        //{
        //    return Ok($"Produto {id}");
        //}

        //[HttpPost]
        //[Route("v1/products")]
        //public IActionResult Post()
        //{
        //    return Ok("Criando um novo produto");
        //}
    }
}
