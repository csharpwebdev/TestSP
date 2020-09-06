using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Task1.ServiceModel;
using Task1.ServiceModel.DTOs;

namespace Task1.Services
{
    public class ProductsService : Service
    {
        string connectionString = "mongodb://localhost";

        public object Get(ProductRequestDTO request)
        {
            Random rand = new Random();

            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("products");
            var collection = database.GetCollection<Product>("products");
            var products = collection.AsQueryable<Product>();

            //Check if the Note Id was passed in the Request.  
            if (!request.Id.HasValue)
            {
                //Note Id is not present in the Request, hence return all Notes.  
                var productsResponse = from p in products
                                    select new ProductResponseDTO
                                    {
                                        _Id = p._id,
                                        Id = p.id,
                                        Name = p.name,
                                        Category = p.category,
                                        Description = p.description,
                                        Price = p.price,
                                    };

              
                return productsResponse.ToList();
            }

            //Note Id is present in the Request. Check if there is a corresponding Note. 
            var product = products.Where(p => p._id == request.Id.Value).FirstOrDefault();
            if (product != null)
            {
                //Return the corresponding Note.  
                return new ProductResponseDTO
                {
                    _Id = product._id,
                    Name = product.name,
                    Category = product.category,
                    Description = product.description,
                    Price = product.price,
                };
            }
            else
            {
                //Return Empty string as there is no corresponding Note.  
                return string.Empty;
            }
        }

        public object Get(ProductsTotalRequestDTO request)
        {
            ProductsTotalResponseDTO result = new ProductsTotalResponseDTO();
            try
            {
                var client = new MongoClient(connectionString);
                var database = client.GetDatabase("products");
                var collection = database.GetCollection<Product>("products");

                var aggregate = collection.Aggregate()
                                     .Group(new BsonDocument { { "_id", "$category" }, { "count", new BsonDocument("$sum", 1) } })
                                     .Sort(new BsonDocument { { "_id", 1 } }).ToList();

                result.CategoryCounts = new Dictionary<string, string>();
                foreach(var c in aggregate)
                {
                    result.CategoryCounts[c["_id"].ToString()] = c["count"].ToString();
                }

                result.TotalCounts = (int)collection.Count(p=>true);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return result;
        }

        public object Post(CreateProductRequestDTO request)
        {
            CreateProductResponseDTO result = new CreateProductResponseDTO();
            try
            {
                var client = new MongoClient(connectionString);
                var database = client.GetDatabase("products");
                var collection = database.GetCollection<Product>("products");
                Product p = new Product() { name = request.Name, category = request.Category, description = request.Description, price = request.Price };
                collection.InsertOne(p);
                result._Id = p._id;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return result;
        }

        public object Put(UpdateProductRequestDTO request)
        {
            UpdateProductResponseDTO result = new UpdateProductResponseDTO();
            try
            {
                var client = new MongoClient(connectionString);
                var database = client.GetDatabase("products");
                var collection = database.GetCollection<Product>("products");

                var update = Builders<Product>.Update
                    .Set("name", request.Name)
                    .Set("category", request.Category)
                    .Set("description", request.Description)
                    .Set("price", request.Price);

                collection.UpdateOne(p => p._id == request._Id, update);
                result.Success = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                result.Success = false;
            }
            return result;
        }

        public object Delete(DeleteProductRequestDTO request)
        {
            DeleteProductResponseDTO result = new DeleteProductResponseDTO();
            try
            {
                var client = new MongoClient(connectionString);
                var database = client.GetDatabase("products");
                var collection = database.GetCollection<Product>("products");
                collection.DeleteOne(p => p._id == request._Id);
                result.Success = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                result.Success = false;
            }
            return result;
        }
    }
}
