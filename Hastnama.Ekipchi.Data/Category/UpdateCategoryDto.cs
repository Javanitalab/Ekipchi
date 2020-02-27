using System.Text.Json.Serialization;

namespace Hastnama.Ekipchi.Data.Category
{
    public class UpdateCategoryDto
    {
        [JsonIgnore] public int Id { get; set; }

        public string Name { get; set; }

    }
}