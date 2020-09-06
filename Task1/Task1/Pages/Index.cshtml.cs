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
    public class IndexModel : PageModel
    {
        private const string baseUrl = "https://localhost:44387/api/";

        public ProductResponseDTO Product
        { get; set; }

        public IList<ProductResponseDTO> Products
        { get; set; }

        public ProductsTotalResponseDTO TotalInfo { get; set; }

        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string Category { get; set; }

        [BindProperty]
        public string Description { get; set; }

        [BindProperty]
        public double Price { get; set; }

        public void OnGet()
        {
            IServiceClient client = new JsonServiceClient(baseUrl).WithCache();

            TotalInfo = client.Get<ProductsTotalResponseDTO>("/products/total");

            Products = client.Get<IList<ProductResponseDTO>>("/products");
        }

        public async Task<IActionResult> OnPost()
        {
            IServiceClient client = new JsonServiceClient(baseUrl).WithCache();

            var result = await client.PostAsync<CreateProductResponseDTO>("/products", new CreateProductRequestDTO { Name = Name, Category = Category, Description = Description, Price = Price });

            return Redirect("/");
        }

        public async Task<IActionResult> OnPostDelete(string id)
        {
            IServiceClient client = new JsonServiceClient(baseUrl).WithCache();

            var result = await client.DeleteAsync<DeleteProductResponseDTO>($"/products/{id}");

            return Redirect("/");

        }
    }
}
