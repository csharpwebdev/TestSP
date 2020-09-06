using MongoDB.Bson;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task1.ServiceModel.DTOs
{
    [Route("/products/total", "GET")]
    public class ProductsTotalRequestDTO
    {
    }

    public class ProductsTotalResponseDTO
    {
        public Dictionary<string, string> CategoryCounts { get; set; }
        public int TotalCounts { get; set; }
    }

    [Route("/products", "GET")]
    [Route("/products/{Id}", "GET")]
    public class ProductRequestDTO
    {
        public ObjectId? Id { get; set; }
    }

    public class ProductResponseDTO
    {
        public ObjectId _Id { get; set; }
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
    }

    [Route("/products", "POST")]
    public class CreateProductRequestDTO
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
    }

    public class CreateProductResponseDTO
    {
        public ObjectId _Id { get; set; }
        public int? Id { get; set; }
    }

    [Route("/products", "PUT")]
    public class UpdateProductRequestDTO
    {
        public ObjectId _Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
    }

    public class UpdateProductResponseDTO
    {
        public bool Success { get; set; }
    }

    [Route("/products/{_Id}", "DELETE")]
    public class DeleteProductRequestDTO
    {
        public ObjectId _Id { get; set; }
    }

    public class DeleteProductResponseDTO
    {
        public bool Success { get; set; }
    }

}
