using Microsoft.AspNetCore.Http;
using Restaurant.Helper.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Core.Models.Requests
{
    public class MealRequest
    {
        public int? ID { get; set; }

        [Required(ErrorMessage = "Please enter {0}")]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Required(ErrorMessage = "Please enter {0}")]
        public Decimal Price { get; set; }
        [Display(Name = "Category")]
        [Required(ErrorMessage = "Please select {0}")]
        public int CategoryID { get; set; }
        [AllowedExtensions(extensions: new string[] { ".jpg", ".png" }, errorMessage: "Please choose a photo!!!")]
        public IFormFile Image { get; set; }
        public string? URL { get; set; }
    }
}
