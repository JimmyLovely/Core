using System;
using Microsoft.AspNetCore.Mvc;

// LINQ
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;

// Project
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
                Console.WriteLine(item);
            }

            Console.WriteLine(scoreQuery.Count());
            Console.WriteLine(scoreQuery.First());
            Console.WriteLine(scoreQuery.Sum());
            Console.WriteLine(scoreQuery.Max());
            Console.WriteLine(scoreQuery.Min());

            // lambda
            IEnumerable<int> scroresLambda = scores.Where(x => x > 3).Select(x => x + 2);

            // query syntax
            Country[] countries = new Country[]{
                new Country(){name="a", population=100},
                new Country(){name="b", population=1000},
                new Country(){name="c", population=100},
                new Country(){name="d", population=1000},
                new Country(){name="e", population=1000},
                new Country(){name="f", population=1001},
                new Country(){name="f", population=1002},
                new Country(){name="f", population=1003},
                new Country(){name="f", population=1004}
            };

            // group
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

            // lambda
            IEnumerable<IGrouping<int, Country>> countryLambdaWithGroup = countries.GroupBy(x => x.population);

            // select
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

            // lambda
            IEnumerable<TmpCountry> countryLambdaWithSelect = countries.Select(x => new TmpCountry {TmpName = x.name, TmpPopulation = x.population});

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

            // lambda
            IEnumerable<IGrouping<int, Country>> countryLambdaWithInto = countries.GroupBy(x => x.population).OrderByDescending(x => x.Key).Select(x => x);

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

            // lambda
            var countryLambdaWithMulti = countries.GroupBy(x => x.population, x => x.name);

            // better
            var countryLambdaWithMulti1 = countries.GroupBy(x => x.population).Select(x => x.Select(x => x.name));

            // multi query with select
            IEnumerable<string> countryQueryWithMultiWithSelect =
                from country in countries
                group country by country.population into groupedCountry
                from tmpGroupedCountry in groupedCountry
                select tmpGroupedCountry.name;

            foreach (var countryQuery in countryQueryWithMultiWithSelect)
            {
                Console.WriteLine(countryQuery);
            }

            // lambda
            var countryLambdaWithMultiWithSelect  = countries.GroupBy(x => x.population, x => x.name).SelectMany(x => x);
            var countryLambdaWithMultiWithSelect2 = countries.GroupBy(x => x.population).SelectMany(x => x.Select(x => x.name));

            // better
            var countryLambdaWithMultiWithSelect3 = countries.GroupBy(x => x.population).SelectMany(x => x, (x, y) => y.name);

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

            // lambda
            var productInfoLambda = categories.Join(products, c => c.id, p => p.categoryId, (c, p) => new {productId = p.id, category = c.name, productName = p.name});

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

            // lambda
            var productInMultifoLambda =
                categories.Join(
                products,
                c => new {categoryId = c.id, price = c.price},
                p => new { categoryId = p.categoryId, price = p.price },
                (c, p) => new {productId = p.id, category = c.name, productName = p.name, price = c.price});

            // join, equals for object
            IEnumerable<Object> productInMultifo2Query =
                from category in categories
                join product in products2
                on new { category = category, price = category.price }
                equals new { category = product.category, price = product.price }
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

            // lambda
            var productInMultifo2Lambda =
                categories.Join(
                products2,
                c => new {category = c, price = c.price},
                p => new {category = p.category, price = p.price },
                (c, p) => new {productId = p.id, category = c.name, productName = p.name, price = c.price});

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

            // lambda
            var productInMultifo3Lambda = categories
                .Join(products, c => c.id, p => p.categoryId, (c, p) => new {c, p})
                .Join(products2, cp => cp.p.categoryId, p2 => p2.category.id, (cp, p2) => new {
                    productId = cp.p.id,
                    category = cp.c.name,
                    productName = cp.p.name,
                    product2Id = p2.id,
                    product2Nmae = p2.name
                });

            // group join
            Category[] categoriesForGroup = new Category[] {
                new Category() {id=1, price=10, name="Book"},
                new Category() {id=2, price=20, name="WebUrl"},
                new Category() {id=3, price=30, name="Phone"},
                new Category() {id=4, price=40, name="Food"},
                new Category() {id=5, price=50, name="Not matched category"},
            };

            Product[] productsForGroup = new Product[] {
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
                new Product() {id = 17, price= 40, categoryId = 11, name = "Other"},
                new Product() {id = 18, price= 12, categoryId = 11, name = "Other 2"}
            };

            Product2[] products2ForGroup = new Product2[] {
                new Product2() {id = 1, price= 10, category = categoriesForGroup[0], name = ".Net 2"},
                new Product2() {id = 2, price= 10, category = categoriesForGroup[0], name = "Java 2"},
                new Product2() {id = 3, price= 12, category = categoriesForGroup[0], name = "JavaScript 2"},
                new Product2() {id = 4, price= 12, category = categoriesForGroup[0], name = "MongoDB 2"},
                new Product2() {id = 5, price= 20, category = categoriesForGroup[1], name = "https://github.com/JimmyLovely/Angular 2"},
                new Product2() {id = 6, price= 20, category = categoriesForGroup[1], name = "https://github.com/JimmyLovely/Book 2"},
                new Product2() {id = 7, price= 12, category = categoriesForGroup[1], name = "https://github.com/JimmyLovely/Note 2"},
                new Product2() {id = 8, price= 12, category = categoriesForGroup[1], name = "https://github.com/JimmyLovely/Core 2"},
                new Product2() {id = 9, price= 30, category = categoriesForGroup[2], name = "Lumia 950 XL 2"},
                new Product2() {id = 10, price= 30, category = categoriesForGroup[2], name = "Lumia 950 2"},
                new Product2() {id = 11, price= 12, category = categoriesForGroup[2], name = "Lumia 1020 2"},
                new Product2() {id = 12, price= 12, category = categoriesForGroup[2], name = "Surface Duo 2"},
                new Product2() {id = 13, price= 40, category = categoriesForGroup[3], name = "Egg 2"},
                new Product2() {id = 14, price= 40, category = categoriesForGroup[3], name = "Tomato 2"},
                new Product2() {id = 15, price= 12, category = categoriesForGroup[3], name = "Hot Dog 2"},
                new Product2() {id = 16, price= 12, category = categoriesForGroup[3], name = "Hamburger 2"},
                new Product2() {id = 17, price= 40, category = {id=11, price=40, name="Food"}, name = "Other"},
                new Product2() {id = 18, price= 12, category = {id=11, price=40, name="Food"}, name = "Other 2"}
            };

            // inner join without group join
            IEnumerable<Object> productInfoQueryWithoutGroup =
                from category in categoriesForGroup
                join product in productsForGroup
                on category.id
                equals product.categoryId
                select new {
                    categroyId = category.id,
                    catergoryName = category.name,
                    productId = product.id,
                    productName = product.name
                };

            foreach (var productInfo in productInfoQueryWithoutGroup)
            {
                Console.WriteLine(productInfo);
            }

            // lambda
            var productInfoLambdaWithoutGroup = categoriesForGroup.Join(productsForGroup, c => c.id, p => p.categoryId, (c, p) => new{
                    categroyId = c.id,
                    catergoryName = c.name,
                    productId = p.id,
                    productName = p.name
            });

            // inner join without group join
            IEnumerable<Object> productInfoQueryWithGroup =
                from category in categoriesForGroup
                join product in productsForGroup
                on category.id
                equals product.categoryId
                into categoryProductGroup
                from categoryProduct in categoryProductGroup
                select new {
                    categroyId = category.id,
                    catergoryName = category.name,
                    productId = categoryProduct.id,
                    productName = categoryProduct.name
                };

            foreach (var productInfo in productInfoQueryWithGroup)
            {
                Console.WriteLine(productInfo);
            }

            // lambda
            var productInfoLambdaWithGroup = categoriesForGroup.Join(productsForGroup, c => c.id, p => p.categoryId, (c, p) => new{
                c,p
            }).Select(cp => new {
                    categroyId = cp.c.id,
                    catergoryName = cp.c.name,
                    productId = cp.p.id,
                    productName = cp.p.id
            });

            // group join
            IEnumerable<GroupJoinResult> productInfoQueryWithGroup2 =
                from category in categoriesForGroup
                join product in productsForGroup
                on category.id
                equals product.categoryId
                into categoryProductGroup
                select new GroupJoinResult
                {
                    categroyId = category.id,
                    catergoryName = category.name,
                    categoryProductGroup = categoryProductGroup
                };

            foreach (var productInfo in productInfoQueryWithGroup2)
            {
                Console.WriteLine(productInfo);
            }

            // lambda
            var productInfoLambdaWithGroup2 = categoriesForGroup.GroupJoin(productsForGroup, c => c.id, p => p.categoryId, (c, p) => new{
                c,p
            }).Select(cp => new {
                    categroyId = cp.c.id,
                    catergoryName = cp.c.name,
                    categoryProductGroup = cp.p,
            });

            // group join with XML
            XElement productInfoQueryWithGroupXML = new XElement("Root",
                from category in categoriesForGroup
                join product in productsForGroup
                on category.id
                equals product.categoryId
                into categoryProductGroup
                select new XElement("Category",
                    new XAttribute("CategroyId", category.id),
                    new XAttribute("CategroyName", category.name),
                    from categoryProduct in categoryProductGroup
                    select new XElement("CategoryProduct",
                        new XElement("ProductId", categoryProduct.id),
                        new XElement("ProductName", categoryProduct.name)))
            );

            Console.WriteLine(productInfoQueryWithGroupXML);

            // left join with group join
            IEnumerable<Object> productInfoQueryWithGroupWithLeftJoin =
                from category in categoriesForGroup
                join product in productsForGroup
                on category.id
                equals product.categoryId
                into categoryProductGroup
                from categoryProduct in categoryProductGroup.DefaultIfEmpty(new Product{id= -1, name = string.Empty})
                select new {
                    categroyId = category.id,
                    catergoryName = category.name,
                    productId = categoryProduct,
                    productName = categoryProduct?.name
                };

            foreach (var productInfo in productInfoQueryWithGroupWithLeftJoin)
            {
                Console.WriteLine(productInfo);
            }

            // lambda
            var productInfoLambdaWithGroupWithLeftJoin = categoriesForGroup.GroupJoin(productsForGroup, c => c.id, p => p.categoryId, (c, p) => new{
                c,p
            }).SelectMany(cp => cp.p.DefaultIfEmpty(), (cp_c, cpps) => new {
                categroyId = cp_c.c.id,
                catergoryName = cp_c.c.name,
                productId = cpps,
                productName = cpps?.name
            });

            // concat query
            List<int> scoreList = new List<int> { 1, 2, 3, 4, 5, 7, 8 };

            IEnumerable<int> scoreListQuery =
                from score in scoreList
                select score;

            IEnumerable<int> scoreListQueryWithOrderBy =
                from score in scoreListQuery
                where score % 2 == 0
                select score;


            foreach (int score in scoreListQueryWithOrderBy)
            {
                Console.WriteLine(score);
            }

            var scoreListQuery2 =
                from score2 in
                    (from score in scoreList
                     select score)
                where score2 % 2 == 0
                select score2;

            foreach (int score in scoreListQueryWithOrderBy)
            {
                Console.WriteLine(score);
            }

            // lambda
            var scoreListLambda = scoreList.Select(x => x).Where(x => x % 2 == 0);

            // store query result in memory
            IEnumerable<int> scoreListQueryWithResult =
                from score in scoreList
                select score;

            int[] scoreIntArray= scoreListQueryWithResult.ToArray();
            var scoreIntArrayWithT = scoreListQueryWithResult.ToArray<int>();

            List<int> scoreIntList = scoreListQueryWithResult.ToList();
            var scoreIntListWithT = scoreListQueryWithResult.ToList<int>();

            // lambda
            var scoreListLambdaWithResultToArray = scoreList.Select(x => x).ToArray();
            var scoreListLambdaWithResultToList  = scoreList.Select(x => x).ToList();

            // group
            List<Student> students = new List<Student>
            {
                new Student {FirstName = "Terry", LastName = "Adams", ID = 120, ExamScores = new List<int> {99, 82, 81, 79}},
                new Student {FirstName = "Fadi", LastName = "Fakhouri", ID = 116, ExamScores = new List<int> {99, 86, 90, 94}},
                new Student {FirstName = "Hanying", LastName = "Feng", ID = 117, ExamScores = new List<int> {93, 92, 80, 87}},
                new Student {FirstName = "Cesar", LastName = "Garcia", ID = 114, ExamScores = new List<int> {97, 89, 85, 82}},
                new Student {FirstName = "Debra", LastName = "Garcia", ID = 115, ExamScores = new List<int> {35, 72, 91, 70}},
                new Student {FirstName = "Hugo", LastName = "Garcia", ID = 118, ExamScores = new List<int> {92, 90, 83, 78}},
                new Student {FirstName = "Sven", LastName = "Mortensen", ID = 113, ExamScores = new List<int> {88, 94, 65, 91}},
                new Student {FirstName = "Claire", LastName = "O'Donnell", ID = 112, ExamScores = new List<int> {75, 84, 91, 39}},
                new Student {FirstName = "Svetlana", LastName = "Omelchenko", ID = 111, ExamScores = new List<int> {97, 92, 81, 60}},
                new Student {FirstName = "Lance", LastName = "Tucker", ID = 119, ExamScores = new List<int> {68, 79, 88, 92}},
                new Student {FirstName = "Michael", LastName = "Tucker", ID = 122, ExamScores = new List<int> {94, 92, 91, 91}},
                new Student {FirstName = "Eugene", LastName = "Zabokritski", ID = 121, ExamScores = new List<int> {96, 85, 91, 60}}
            };

            // basic group
            IEnumerable<IGrouping<string, Student>> studentsGroupBasic =
                from student in students
                group student
                by student.LastName;

            foreach (IGrouping<string, Student> studentsGroup in studentsGroupBasic)
            {
                Console.WriteLine(studentsGroup.Key);
                foreach (Student student in studentsGroup)
                {
                    Console.WriteLine(student);
                }
            }

            // lambda
            var studentsGroupBasicLambda = students.GroupBy(x => x.LastName);

            // projection group
            IEnumerable<IGrouping<Char, Object>> studentsGroupProjection =
                from student in students
                group new { FirstName = student.FirstName, LastName = student.LastName } by student.LastName[0];

            foreach (IGrouping<Char, Object> studentsGroup in studentsGroupProjection)
            {
                Console.WriteLine(studentsGroup.Key);
                foreach (Object student in studentsGroup)
                {
                    Console.WriteLine(student);
                }
            }

            // lambda
            var studentsGroupProjectionLambda = students.GroupBy(x => x.LastName[0], x => new {x.FirstName, x.LastName});

            // calculate group
            IEnumerable<IGrouping<int, Object>> studentsGroupCalculate =
                from student in students
                group new { student.FirstName, student.LastName }
                by student.ExamScores.Average() > 0 ? (int)student.ExamScores.Average() / 10 : 0;

            foreach (IGrouping<int, Object> studentsGroup in studentsGroupCalculate)
            {
                Console.WriteLine(studentsGroup.Key);
                foreach (Object student in studentsGroup)
                {
                    Console.WriteLine(student);
                }
            }

            // lambda
            var studentsGroupCalculateLambda = students.GroupBy(x => x.ExamScores.Average() > 0 ? (int)x.ExamScores.Average() /10 : 0, x => new {x.FirstName, x.LastName});

            // multi-calculate group
            IEnumerable<IGrouping<Object, Object>> studentsGroupMulti =
                from student in students
                group new { student.FirstName, student.LastName }
                by new
                {
                    LastNameLetter = student.LastName[0],
                    IsPass = student.ExamScores.Average() > 80
                };

            foreach (IGrouping<Object, Object> studentsGroup in studentsGroupMulti)
            {
                Console.WriteLine(studentsGroup.Key);
                foreach (Object student in studentsGroup)
                {
                    Console.WriteLine(student);
                }
            }

            // lambda
            var studentsGroupMultiLambda = students.GroupBy(x => new {lastNameLetter = x.LastName[0], IsPass = x.ExamScores.Average() > 80}, x => new {x.FirstName, x.LastName});

            return Ok(resultString);
        }
    }
}