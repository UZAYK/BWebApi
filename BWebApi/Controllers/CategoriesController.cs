using BWebApi.DAL.Context;
using BWebApi.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        public List<Category> Get()
        {
            using var context = new BWebApiContext();
            return context.Categories.ToList();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            using var context = new BWebApiContext();
            var category = context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPut]
        public IActionResult UpdateCategory(Category category)
        {
            using var context = new BWebApiContext();
            var updateCategory = context.Find<Category>(category.Id);
            if (updateCategory == null)
                return NotFound();
            updateCategory.Name = category.Name;
            context.Update(updateCategory);
            context.SaveChanges();
            return NoContent();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            using var context = new BWebApiContext();
            var deletedCategory = context.Categories.Find(id);
            if (deletedCategory == null)
                return NotFound();
            context.Remove(deletedCategory);
            context.SaveChanges();
            return NoContent();
        }

        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            using var context = new BWebApiContext();
            context.Categories.Add(category);
            context.SaveChanges();
            return Created("", category);
        }

        [HttpGet("{id}/blogs")]
        public IActionResult GetWithBlogsById(int id)
        {
            using var context = new BWebApiContext();
            var category = context.Categories.Find(id);
            if (category == null)
                return NotFound();
            var categoryWithBlogs = context.Categories.Where(I => I.Id == id).Include
                (I => I.Blogs).ToList();
            return Ok();
        }

    }
}
