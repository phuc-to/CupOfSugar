﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using CupOfSugar.WebSite.Models;
using Microsoft.AspNetCore.Hosting;

namespace CupOfSugar.WebSite.Services
{
    /// <summary>
    /// JsonFileProductService
    /// Accesses, adds and modifies products in Products.json
    /// </summary>
    public class JsonFileProductService
    {
        /// <summary>
        /// JsonFileProductService constructor
        /// Initializes the webHostEnvironment object
        /// </summary>
        /// <param name="webHostEnvironment"></param>
        public JsonFileProductService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; } //Provides info about application's webhosting env

        /// <summary>
        /// method that returns the path of the products.json file
        /// </summary>
        private string JsonFileName
        {
            //returns path string
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "products.json"); } 
        }

        /// <summary>
        /// Gets all products from Products.json
        /// </summary>
        /// <returns>IEnumerable list of products</returns>
        public IEnumerable<Product> GetProducts()
        {
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                //converts into a list of products
                return JsonSerializer.Deserialize<Product[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
        }

        /// <summary>
        /// Updating a product rating in Products.json
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="rating"></param>
        public void AddRating(string productId, int rating)
        {
            var products = GetProducts(); //stores all products
            var query = products.First(x => x.Id == productId); //Gets the first product with a matching Id

            //adds a new int array when there are no ratings for a product
            if(query.Ratings == null)
            {
                query.Ratings = new int[] { rating };
            }

            //adds rating to int rating array
            else
            {
                var ratings = query.Ratings.ToList();
                ratings.Add(rating);
                query.Ratings = ratings.ToArray();
            }

            using(var outputStream = File.OpenWrite(JsonFileName))
            {
                JsonSerializer.Serialize<IEnumerable<Product>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    {
                        SkipValidation = true,
                        Indented = true
                    }),
                    products
                );
            }
        }
    }
}