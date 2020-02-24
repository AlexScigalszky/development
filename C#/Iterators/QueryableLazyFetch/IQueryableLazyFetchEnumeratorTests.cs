using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace PalabrasAleatoriasOrigenDeDatos.Iterators.QueryableLazyFetch.Tests
{
    [TestClass()]
    public class IQueryableLazyFetchEnumeratorTests
    {

        [TestMethod()]
        public void MoveNextWitText1Test()
        {
            var items = new List<string>() {
                "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten",
            };
            MoveNextTest<string>(items.AsQueryable());
        }

        [TestMethod()]
        public void MoveNextWitText2Test()
        {
            var items = new List<string>() {
                "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten",
                "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen", "twenty",
                "twenty-one", "twenty-two",
            };
            MoveNextTest<string>(items.AsQueryable());
        }

        [TestMethod()]
        public void MoveNextWithEmptyListTest()
        {
            var items = new List<int>() {};
            MoveNextTest<int>(items.AsQueryable());
        }

        [TestMethod()]
        public void MoveNextWith1NumbersTest()
        {
            var items = new List<int>() { 1 };
            MoveNextTest<int>(items.AsQueryable());
        }

        [TestMethod()]
        public void MoveNextWith2NumbersTest()
        {
            var items = new List<int>() { 1, 2 };
            MoveNextTest<int>(items.AsQueryable());
        }

        [TestMethod()]
        public void MoveNextWith21NumbersTest()
        {
            var items = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21 };
            MoveNextTest<int>(items.AsQueryable());
        }

        [TestMethod()]
        public void MoveNextWith999NumbersTest()
        {
            var items = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21 };
            MoveNextTest<int>(items.AsQueryable());
        }

        [TestMethod()]
        public void MoveNextWith1000NumbersTest()
        {
            var items = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21 };
            MoveNextTest<int>(items.AsQueryable());
        }

        [TestMethod()]
        public void MoveNextWithMinor20ItemsTest()
        {
            
        }

        [TestMethod()]
        public void ResetTest()
        {
            var items = new List<int>() { 1, 2 };
            var enumerator = new IQueryableLazyFetchEnumerator<int>(items.AsQueryable());
            bool end = enumerator.MoveNext();
            Assert.IsTrue(end);
            
            enumerator.Reset();

            end = enumerator.MoveNext();
            Assert.IsTrue(end);
            end = enumerator.MoveNext();
            Assert.IsTrue(end);
            end = enumerator.MoveNext();
            Assert.IsFalse(end);
        }

        private static void MoveNextTest<T>(IQueryable<T> items)
        {
            var enumerator = new QueryableLazyFetch<T>(items);
            int counter = -1;
            foreach (var item in enumerator)
            {
                counter++;
                Assert.AreEqual(items.ElementAt(counter), item);
            }
        }
    }
}