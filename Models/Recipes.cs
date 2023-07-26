using MongoDB.Bson;

namespace Recept.Models
{
    public class Recipes
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
