using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using Recept.Models;

namespace Recept.Controllers
{
    public class IngredientsController : Controller
    {
        public IActionResult Index()
        {
            return View(Index);
        }

        public IActionResult Create()
        {
            MongoClient dbClient = new MongoClient();

            var database = dbClient.GetDatabase("recept-samling");
            var collection = database.GetCollection<Recipes>("recipes");

            List<Recipes> recipes = collection.Find(r => true).ToList();

            return View(recipes);
        }

        [HttpPost]
        public IActionResult Create(Ingredients ingredients)
        {
            MongoClient dbClient = new MongoClient();

            var database = dbClient.GetDatabase("recept-samling");
            var collection = database.GetCollection<Ingredients>("ingredients");

            collection.InsertOne(ingredients);

            return Redirect("/Ingredients/Create");
        }


        public IActionResult Edit(string Id)
        {
            ObjectId recipeId = new ObjectId(Id);
            MongoClient dbClient = new MongoClient();

            var database = dbClient.GetDatabase("recept-samling");
            var collection = database.GetCollection<Ingredients>("ingredients");

            Ingredients ingredients = collection.Find(i => i.Id == recipeId).FirstOrDefault();


            return View(ingredients);
        }
        [HttpPost]
        public IActionResult Edit(string Id, Ingredients ingredients)
        {
            ObjectId ingredientsId = new ObjectId(Id);
            MongoClient dbClient = new MongoClient();

            var database = dbClient.GetDatabase("recept-samling");
            var collection = database.GetCollection<Ingredients>("ingredients");


            ingredients.RecipeId = collection.Find(i => i.Id == ingredientsId).FirstOrDefault()?.RecipeId;

            collection.ReplaceOne(i => i.Id == ingredientsId, ingredients);
            return Redirect("/Recipe");
        }


        public IActionResult Show(string Id)
        {
            ObjectId recipeId = new ObjectId(Id);
            MongoClient dbClient = new MongoClient();

            var database = dbClient.GetDatabase("recept-samling");
            var collection = database.GetCollection<Ingredients>("ingredients");

            Ingredients ingredients = collection.Find(i => i.Id == recipeId).FirstOrDefault();

            return View(ingredients);
        }

        [HttpPost]
        public IActionResult Delete(string Id)
        {
            ObjectId recipeId = new ObjectId(Id);
            MongoClient dbClient = new MongoClient();

            var database = dbClient.GetDatabase("recept-samling");
            var collection = database.GetCollection<Ingredients>("ingredients");

            collection.DeleteOne(i => i.Id == recipeId);

            return Redirect("/Recipe/Index");
        }

        public IActionResult ShowAll(string Id)
        {
            if (ObjectId.TryParse(Id, out ObjectId recipeId))
            {
                MongoClient dbClient = new MongoClient();
                var database = dbClient.GetDatabase("recept-samling");
                var recipesCollection = database.GetCollection<Recipes>("recipes");
                var ingredientsCollection = database.GetCollection<Ingredients>("ingredients");

                var filter = Builders<Ingredients>.Filter.Eq(i => i.RecipeId, Id);
                List<Ingredients> ingredients = ingredientsCollection.Find(filter).ToList();


                Recipes recipe = recipesCollection.Find(r => r.Id == recipeId).FirstOrDefault();
                ViewBag.RecipeName = recipe?.Name;
                ViewBag.RecipeDescription = recipe?.Description;

                return View(ingredients);
            }

            return RedirectToAction("ShowAll");
        }


    }
}
