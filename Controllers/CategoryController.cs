using CRUDUsingEf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace CRUDUsingEf.Controllers
{
    public class CategoryController : ApiController
    {
        B22APIDBEntities _db = new B22APIDBEntities();

        // get all categories
        [HttpGet]
        [ResponseType(typeof(IEnumerable<Category>))]
        public IHttpActionResult GetAll()
        {
            var categories = _db.Categories.ToList();
            return Ok(categories);
        }

        [HttpGet]
        [ResponseType(typeof(Category))]
        public IHttpActionResult GetCategoryById([FromUri] int id)
        {
            if (id > 0)
            {
                var category = _db.Categories.Find(id);

                if (category != null)
                {
                    return Ok(category);
                }
                else
                {
                    return NotFound();
                }
            }

            return BadRequest();
        }

        [HttpPost]
        public IHttpActionResult CreateCategory([FromBody] Category category)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Add(category);
                _db.SaveChanges();

                return CreatedAtRoute("DefaultApi", "", category);
            }

            return BadRequest();
        }

        [HttpPut]
        public IHttpActionResult UpdateCategory([FromUri] int id, [FromBody] Category category)
        {
            if (ModelState.IsValid)
            {
                if (id == category.Id)
                {
                    var dbCategory = _db.Categories.Find(id);
                    dbCategory.Name = category.Name;

                    _db.SaveChanges();

                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }

        [HttpDelete]
        public IHttpActionResult DeleteCategory([FromUri] int id)
        {
            if (id > 0)
            {
                var category = _db.Categories.Find(id);

                if (category != null)
                {
                    _db.Categories.Remove(category);
                    _db.SaveChanges();

                    return Ok();
                }

                return NotFound();
            }

            return BadRequest();
        }
    }
}
