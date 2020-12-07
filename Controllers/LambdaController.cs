using System;

using Microsoft.AspNetCore.Mvc;

// Lambda
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace NetCore.Controllers
{
    [Route("[controller]")]
    public class LambdaController : ControllerBase
    {
        [HttpGet]
        [Route("demo")]
        public IActionResult Demo()
        {
            Func<int, int> square = x => x * x;
            Console.WriteLine(square(5));
            Console.WriteLine(square);

            Expression<Func<int, int>> squareExpression = x => x * x;
            Console.WriteLine(square);
            Console.WriteLine(square(5));

            int[] tmpArray = { 1, 2, 3, 4, 5 };
            IEnumerable<int> filterArray = tmpArray.Select(x => x * x);
            foreach (int item in filterArray)
            {
                Console.WriteLine(item);
            }

            var filterArray2 = Enumerable.Select<int, int>(tmpArray, x => x * x);
            foreach (int item in filterArray2)
            {
                Console.WriteLine(item);
            }
            var filterArray3 = Enumerable.Select<int, int>(tmpArray, square);
            foreach (int item in filterArray3)
            {
                Console.WriteLine(item);
            }

            Action<int> line = (x) => Console.WriteLine(x);

            Func<string> Func1String = () => string.Empty;

            Func<int, string> Func2String = (x) => (x * x).ToString();

            Func<int, string> Func2StringWithout = x => (x * x).ToString();

            Func<int, int, string> Func3String = (x, y) => (x * y).ToString();

            Func<int, int, int> Func3StringWithoutParameter = (_, x) => _ * 3;

            // Func<int, int, int, int> Func3StringWithoutParameter2 = (_, _, x) => x * 3;

            // tuple
            Func<(int a, int b, int c), (int, int, int)> Func3IntWithTuple = abc => (abc.a * 2, abc.b * 2, abc.Item3 * 2);

            (int, int, int) tuple3Int = (1, 2, 3);
            (int, int, int) tuple3IntResult = Func3IntWithTuple(tuple3Int);
            Console.WriteLine(tuple3Int);
            Console.WriteLine(tuple3IntResult);

            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            // Count
            Func<int, bool> numberWithCountFunc = n => n > 6;

            int numberWithCount1 = numbers.Count(n => n > 6);
            int numberWithCount2 = numbers.Count(numberWithCountFunc);
            int numberWithCount3 = Enumerable.Count<int>(numbers, numberWithCountFunc);

            // TakeWhile
            Func<int, int, bool> numberWithTakeWhileFunc = (n, index) => n >= index;

            IEnumerable<int> numberWithTakeWhile1 = numbers.TakeWhile((n, index) => n >= index);
            IEnumerable<int> numberWithTakeWhile2 = numbers.TakeWhile(numberWithTakeWhileFunc);
            IEnumerable<int> numberWithTakeWhile3 = Enumerable.TakeWhile<int>(numbers, numberWithTakeWhileFunc);

            return Ok();
        }
    }
}