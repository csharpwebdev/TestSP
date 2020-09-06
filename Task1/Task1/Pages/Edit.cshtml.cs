using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Bson;
using ServiceStack;
using Task1.ServiceModel.DTOs;

namespace Task1.Pages
{
    public class EditModel : PageModel
    {
        private const string baseUrl = "https://localhost:44387/api/";

        [BindProperty]
        public ProductResponseDTO Product
        { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }

        //[BindProperty]
        //public string Id { get; set; }

        //[BindProperty]
        //public string Name { get; set; }

        //[BindProperty]
        //public string Category { get; set; }

        //[BindProperty]
        //public string Description { get; set; }

        //[BindProperty]
        //public double Price { get; set; }

        public void OnGet()
        {
            IServiceClient client = new JsonServiceClient(baseUrl).WithCache();

            Product = client.Get<ProductResponseDTO>($"/products/{Id}");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Product != null)
            {
                IServiceClient client = new JsonServiceClient(baseUrl).WithCache();

                var result = await client.PutAsync<UpdateProductResponseDTO>("/products", new UpdateProductRequestDTO { _Id = new ObjectId(Id), Name = Product.Name, Category = Product.Category, Description = Product.Description, Price = Product.Price });
            }

            return Redirect("/");
        }
    }
}
