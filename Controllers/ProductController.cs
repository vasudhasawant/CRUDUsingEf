using CRUDUsingEf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CRUDUsingEf.Controllers
{
    [RoutePrefix("api/product")]
    public class ProductController : ApiController
    {
        B22APIDBEntities _db = new B22APIDBEntities();

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            return Ok(_db.Products.ToList());
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetProductById([FromUri] int id)
        {
            return Ok(_db.Products.Find(id));
        }

        [HttpPost]
        public IHttpActionResult CreateProduct([FromBody] Product product)
        {
            if (ModelState.IsValid)
            {
                _db.Products.Add(product);
                _db.SaveChanges();

                return Created("DefaultApi", product);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult UpdateProduct([FromUri] int id,
            [FromBody] Product product)
        {
            if (ModelState.IsValid)
            {
                if (product != null && product.Id > 0)
                {
                    if (id == product.Id)
                    {
                        Product dbProduct = _db.Products.Find(id);
                        dbProduct.Name = product.Name;
                        dbProduct.Price = product.Price;
                        dbProduct.AvailableQuantity = product.AvailableQuantity;

                        _db.SaveChanges();

                        return Ok(dbProduct);
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult DeleteProduct([FromUri] int id)
        {
            if (id > 0)
            {
                Product dbProduct = _db.Products.Find(id);

                if (dbProduct != null)
                {
                    _db.Products.Remove(dbProduct);
                    _db.SaveChanges();
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest();
            }
        }

     }
}
