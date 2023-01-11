using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using PizzaRecipes.Models;

namespace PizzaRecipes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private static Recipe[] _recipes = 
        {
            new() {Title = "Chicken Wings"},
            new() {Title = "Curry Madras"},
            new() {Title = "Mamma's Italian Spaghetti" }
        };
        
        [HttpGet]
        public ActionResult GetRecipes([FromQuery]int count)
        {
            if (!_recipes.Any())
                return NotFound();

            return Ok(_recipes.Take(count));
        }

        [HttpDelete]
        public ActionResult DeleteRecipes([FromBody]Recipe name)
        {
            var indexOf = Array.IndexOf(_recipes, name.Title);

            if (indexOf < 0)
            {
                return BadRequest();
            }

            //var list = _recipes.ToList();
            //list.Remove(name);
            //_recipes = list.ToArray();


            var newRecipes = new Recipe[_recipes.Length - 1];
            Array.Copy(_recipes, 0, newRecipes, 0, indexOf);
            Array.Copy(_recipes, indexOf + 1, newRecipes, indexOf, _recipes.Length - indexOf - 1);

            _recipes = newRecipes;

            return NoContent();
        }

        [HttpPost]
        public ActionResult CreateNewRecipes([FromBody]Recipe name)
        {
            var indexOf = Array.IndexOf(_recipes, name.Title);

            if (indexOf >= 0)
            {
                return Conflict(indexOf);
            }

            var newRecipes = new Recipe[_recipes.Length + 1];
            Array.Copy(_recipes, newRecipes, _recipes.Length);
            newRecipes[newRecipes.Length - 1] = name;
            _recipes = newRecipes;

            return Created("", name);

        }

        [HttpPut]
        public ActionResult AppendRecipes(uint index, string name)
        {
            if ( index > _recipes.Length - 1 )
            {
                return NotFound();
            }

            var curRecipe = _recipes[index];
            curRecipe.Title = name;
            _recipes[index].Title = name;
            return Content(name);
        }
    }
}
