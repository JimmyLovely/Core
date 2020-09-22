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

            return Ok(resultString);
        }
    }
}