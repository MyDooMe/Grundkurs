using MongoDB.Bson;

namespace Recept.Models
{
    public class Ingredients
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string RecipeId { get; set; }
        public string Amount { get; set; }
        public string Description { get; set; }
    }
}
