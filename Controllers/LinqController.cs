using System;
using Microsoft.AspNetCore.Mvc;

// LINQ
using System.Linq;
using System.Collections.Generic;

using NetCore.Models;

namespace NetCore.Controllers
{
    [Route("[controller]")]
    public class LinqController : ControllerBase
    {
        [HttpGet]
        [Route("demo")]
        public IActionResult Demo()
        {
            int[] scores = new int[] { 1, 2, 3, 4, 5, 7, 8 };

            // basic
            IEnumerable<int> scoreQuery =
                from score in scores
                where score > 3
                select score + 2;

            string resultString = string.Empty;
            foreach (var item in scoreQuery)
            {
                resultString += item;
            }

            Console.WriteLine(scoreQuery.Count());
            Console.WriteLine(scoreQuery.First());
            Console.WriteLine(scoreQuery.Sum());
            Console.WriteLine(scoreQuery.Max());
            Console.WriteLine(scoreQuery.Min());

            // query syntax
            Country[] countries = new Country[]{
                new Country() {name="a", population =100},
                new Country(){name="b", population=1000},
                new Country(){name="c", population=100},
                new Country(){name="d", population=1000},
                new Country(){name="e", population=1000},
                new Country(){name="f", population=1001},
                new Country(){name="f", population=1002},
                new Country(){name="f", population=1003},
                new Country(){name="f", population=1004}
            };

            //  group
            IEnumerable<IGrouping<int, Country>> countryQueryWithGroup =
                from country in countries
                group country by country.population;

            Console.WriteLine("group");
            foreach (var countryList in countryQueryWithGroup)
            {
                Console.WriteLine(countryList.Key);
                foreach (var country in countryList)
                {
                    Console.WriteLine(country.name + country.population);
                }
            }

            //  select
            IEnumerable<TmpCountry> countryQueryWithSelect =
                from country in countries
                select new TmpCountry
                {
                    TmpName = country.name,
                    TmpPopulation = country.population
                };
            Console.WriteLine("select");
            foreach (var item in countryQueryWithSelect)
            {
                Console.WriteLine(item.TmpName + item.TmpPopulation);
            }

            // into
            IEnumerable<IGrouping<int, Country>> countryQueryWithInto =
                from country in countries
                group country by country.population into groupedCountry
                orderby groupedCountry.Key descending
                select groupedCountry;

            Console.WriteLine("into");
            foreach (var countryList in countryQueryWithInto)
            {
                Console.WriteLine(countryList.Key);
                foreach (var country in countryList)
                {
                    Console.WriteLine(country.name + country.population);
                }
            }

            // multi query
            IEnumerable<IEnumerable<string>> countryQueryWithMulti =
                from country in countries
                group country by country.population into groupedCountry
                select (
                    from tmpGroupedCountry in groupedCountry
                    select tmpGroupedCountry.name
                );

            foreach (var countryQuery in countryQueryWithMulti)
            {
                Console.WriteLine(countryQuery);
                foreach (var item in countryQuery)
                {
                    Console.WriteLine(item);
                }
            }

            // join
            Category[] categories = new Category[] {
                new Category() {id=1, price=10, name="Book"},
                new Category() {id=2, price=20, name="WebUrl"},
                new Category() {id=3, price=30, name="Phone"},
                new Category() {id=4, price=40, name="Food"}
            };

            Product[] products = new Product[] {
                new Product() {id = 1, price= 10, categoryId = 1, name = ".Net"},
                new Product() {id = 2, price= 10, categoryId = 1, name = "Java"},
                new Product() {id = 3, price= 12, categoryId = 1, name = "JavaScript"},
                new Product() {id = 4, price= 12, categoryId = 1, name = "MongoDB"},
                new Product() {id = 5, price= 20, categoryId = 2, name = "https://github.com/JimmyLovely/Angular"},
                new Product() {id = 6, price= 20, categoryId = 2, name = "https://github.com/JimmyLovely/Book"},
                new Product() {id = 7, price= 12, categoryId = 2, name = "https://github.com/JimmyLovely/Note"},
                new Product() {id = 8, price= 12, categoryId = 2, name = "https://github.com/JimmyLovely/Core"},
                new Product() {id = 9, price= 30, categoryId = 3, name = "Lumia 950 XL"},
                new Product() {id = 10, price= 30, categoryId = 3, name = "Lumia 950"},
                new Product() {id = 11, price= 12, categoryId = 3, name = "Lumia 1020"},
                new Product() {id = 12, price= 12, categoryId = 3, name = "Surface Duo"},
                new Product() {id = 13, price= 40, categoryId = 4, name = "Egg"},
                new Product() {id = 14, price= 40, categoryId = 4, name = "Tomato"},
                new Product() {id = 15, price= 12, categoryId = 4, name = "Hot Dog"},
                new Product() {id = 16, price= 12, categoryId = 4, name = "Hamburger"},
                new Product() {id = 17, price= 40, categoryId = 5, name = "Other"},
                new Product() {id = 18, price= 12, categoryId = 5, name = "Other 2"}
            };

            Product2[] products2 = new Product2[] {
                new Product2() {id = 1, price= 10, category = categories[0], name = ".Net 2"},
                new Product2() {id = 2, price= 10, category = categories[0], name = "Java 2"},
                new Product2() {id = 3, price= 12, category = categories[0], name = "JavaScript 2"},
                new Product2() {id = 4, price= 12, category = categories[0], name = "MongoDB 2"},
                new Product2() {id = 5, price= 20, category = categories[1], name = "https://github.com/JimmyLovely/Angular 2"},
                new Product2() {id = 6, price= 20, category = categories[1], name = "https://github.com/JimmyLovely/Book 2"},
                new Product2() {id = 7, price= 12, category = categories[1], name = "https://github.com/JimmyLovely/Note 2"},
                new Product2() {id = 8, price= 12, category = categories[1], name = "https://github.com/JimmyLovely/Core 2"},
                new Product2() {id = 9, price= 30, category = categories[2], name = "Lumia 950 XL 2"},
                new Product2() {id = 10, price= 30, category = categories[2], name = "Lumia 950 2"},
                new Product2() {id = 11, price= 12, category = categories[2], name = "Lumia 1020 2"},
                new Product2() {id = 12, price= 12, category = categories[2], name = "Surface Duo 2"},
                new Product2() {id = 13, price= 40, category = categories[3], name = "Egg 2"},
                new Product2() {id = 14, price= 40, category = categories[3], name = "Tomato 2"},
                new Product2() {id = 15, price= 12, category = categories[3], name = "Hot Dog 2"},
                new Product2() {id = 16, price= 12, category = categories[3], name = "Hamburger 2"},
                new Product2() {id = 17, price= 40, category = {id=5, price=40, name="Food"}, name = "Other"},
                new Product2() {id = 18, price= 12, category = {id=5, price=40, name="Food"}, name = "Other 2"}
            };

            // join, equals for single property
            IEnumerable<Object> productInfoQuery =
                from category in categories
                join product in products
                on category.id
                equals product.categoryId
                select new
                {
                    productId = product.id,
                    category = category.name,
                    productName = product.name
                };

            foreach (var productInfo in productInfoQuery)
            {
                Console.WriteLine(productInfo);
            }

            // join, inner group join
            IEnumerable<Object> productInfoQueryWithGroup =
                from category in categories
                join product in products
                on category.id
                equals product.categoryId
                into categoryProducts
                from categoryProduct in categoryProducts
                select new
                {
                    productId = categoryProduct.id,
                    category = category.name,
                    productName = categoryProduct.name
                };

            foreach (var productInfoWithGroup in productInfoQueryWithGroup)
            {
                Console.WriteLine(productInfoWithGroup);
            }

            // join, equals for multi properties
            IEnumerable<Object> productInMultifoQuery =
                from category in categories
                join product in products
                on new { categoryId = category.id, price = category.price }
                equals new { categoryId = product.categoryId, price = product.price }
                select new
                {
                    productId = product.id,
                    category = category.name,
                    productName = product.name,
                    price = category.price
                };

            foreach (var productInMultifo in productInMultifoQuery)
            {
                Console.WriteLine(productInMultifo);
            }

            // join, equals for object
            IEnumerable<Object> productInMultifo2Query =
                from category in categories
                join product in products2
                on new { category = category, price = category.price }
                equals new { product.category, price = product.price }
                select new
                {
                    productId = product.id,
                    category = category.name,
                    productName = product.name,
                    price = category.price
                };

            foreach (var productInMultifo2 in productInMultifo2Query)
            {
                Console.WriteLine(productInMultifo2);
            }

            // join, equals for Multi resources
            IEnumerable<Object> productInMultifo3Query =
                from category in categories
                join product in products
                on category.id
                equals product.categoryId
                join product2 in products2
                on product.categoryId
                equals product2.category.id
                select new
                {
                    productId = product.id,
                    category = category.name,
                    productName = product.name,
                    product2Id = product2.id,
                    product2Nmae = product2.name
                };

            foreach (var productInMultifo3 in productInMultifo3Query)
            {
                Console.WriteLine(productInMultifo3);
            }

            return Ok(resultString);
        }
    }
}