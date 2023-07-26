using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using Recept.Models;

namespace Recept.Controllers
{
    public class RecipeController : Controller
    {
        public IActionResult Index()
        {
            MongoClient dbClient = new MongoClient();

            var database = dbClient.GetDatabase("recept-samling");
            var collection = database.GetCollection<Recipes>("recipes");

            List<Recipes> recipes = collection.Find(r => true).ToList();

            return View(recipes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Recipes recipes)
        {
            MongoClient dbClient = new MongoClient();

            var database = dbClient.GetDatabase("recept-samling");
            var collection = database.GetCollection<Recipes>("recipes");

            collection.InsertOne(recipes);

            return Redirect("/Recipe");
        }

        public IActionResult Show(string Id)
        {
            ObjectId recipeId = new ObjectId(Id);
            MongoClient dbClient = new MongoClient();

            var database = dbClient.GetDatabase("recept-samling");
            var collection = database.GetCollection<Recipes>("recipes");

            Recipes recipes = collection.Find(r => r.Id == recipeId).FirstOrDefault();

            return View(recipes);
        }

        [HttpPost]
        public IActionResult Delete(string Id)
        {
            ObjectId recipeId = new ObjectId(Id);
            MongoClient dbClient = new MongoClient();

            var database = dbClient.GetDatabase("recept-samling");
            var collection = database.GetCollection<Recipes>("recipes");
            
            collection.DeleteOne(r => r.Id == recipeId);

            return Redirect("/Recipe");
        }

        public IActionResult Edit(string Id)
        {
            ObjectId recipeId = new ObjectId(Id);
            MongoClient dbClient = new MongoClient();

            var database = dbClient.GetDatabase("recept-samling");
            var collection = database.GetCollection<Recipes>("recipes");

            Recipes recipes = collection.Find(r => r.Id == recipeId).FirstOrDefault();

            return View(recipes);
        }

        [HttpPost]
        public IActionResult Edit(string Id, Recipes recipes)
        {
            ObjectId recipeId = new ObjectId(Id);
            MongoClient dbClient = new MongoClient();

            var database = dbClient.GetDatabase("recept-samling");
            var collection = database.GetCollection<Recipes>("recipes");

            recipes.Id = recipeId;
            collection.ReplaceOne(r => r.Id == recipeId, recipes);
            return Redirect("/Recipe");
        }
    }
}
