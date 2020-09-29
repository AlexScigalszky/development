using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace PDFKeyValuesHelper.Tests
{
    [TestClass()]
    public class PDFKeyValuesHelperTests
    {
        #region CountPages
        [TestMethod()]
        public void CountPages2ItemsInPageWith2Test()
        {
            var items = new int[] { 1, 2 };
            var helper = new PDFKeyValuesHelper<int>(items, 2);

            Assert.AreEqual(1, helper.CountPages);
            Assert.AreEqual(1, helper.CountPagesWithoutLast);
        }

        [TestMethod()]
        public void CountPages3ItemsInPageWith2Test()
        {
            var items = new int[] { 1, 2, 3 };
            var helper = new PDFKeyValuesHelper<int>(items, 2);

            Assert.AreEqual(2, helper.CountPages);
            Assert.AreEqual(2, helper.CountPagesWithoutLast);
        }

        [TestMethod()]
        public void CountPages4ItemsInPageWith3Test()
        {
            var items = new int[] { 1, 2, 3, 4 };
            var helper = new PDFKeyValuesHelper<int>(items, 3);

            Assert.AreEqual(2, helper.CountPages);
            Assert.AreEqual(2, helper.CountPagesWithoutLast);
        }

        [TestMethod()]
        public void CountPages5ItemsInPageWith3Test()
        {
            var items = new int[] { 1, 2, 3, 4, 5 };
            var helper = new PDFKeyValuesHelper<int>(items, 3);

            Assert.AreEqual(2, helper.CountPages);
            Assert.AreEqual(2, helper.CountPagesWithoutLast);
        }

        [TestMethod()]
        public void CountPages6ItemsInPageWith3Test()
        {
            var items = new int[] { 1, 2, 3, 4, 5, 6 };
            var helper = new PDFKeyValuesHelper<int>(items, 3);

            Assert.AreEqual(2, helper.CountPages);
            Assert.AreEqual(2, helper.CountPagesWithoutLast);
        }

        [TestMethod()]
        public void CountPages7ItemsInPageWith3Test()
        {
            var items = new int[] { 1, 2, 3, 4, 5, 6, 7 };
            var helper = new PDFKeyValuesHelper<int>(items, 3);

            Assert.AreEqual(3, helper.CountPages);
            Assert.AreEqual(3, helper.CountPagesWithoutLast);
        }

        [TestMethod()]
        public void CountPages3ItemsInPageWith3And2Test()
        {
            var items = new int[] { 1, 2, 3 };
            var helper = new PDFKeyValuesHelper<int>(items, 3, 2, true);

            Assert.AreEqual(2, helper.CountPages);
            Assert.AreEqual(1, helper.CountPagesWithoutLast);
        }

        [TestMethod()]
        public void CountPages4ItemsInPageWith3And2Test()
        {
            var items = new int[] { 1, 2, 3, 4 };
            var helper = new PDFKeyValuesHelper<int>(items, 3, 2, true);

            Assert.AreEqual(2, helper.CountPages);
            Assert.AreEqual(1, helper.CountPagesWithoutLast);
        }

        [TestMethod()]
        public void CountPages5ItemsInPageWith3And2Test()
        {
            var items = new int[] { 1, 2, 3, 4, 5 };
            var helper = new PDFKeyValuesHelper<int>(items, 3, 2, true);

            Assert.AreEqual(2, helper.CountPages);
            Assert.AreEqual(1, helper.CountPagesWithoutLast);
        }

        [TestMethod()]
        public void CountPages6ItemsInPageWith3And2Test()
        {
            var items = new int[] { 1, 2, 3, 4, 5, 6 };
            var helper = new PDFKeyValuesHelper<int>(items, 3, 2, true);

            Assert.AreEqual(3, helper.CountPages);
            Assert.AreEqual(2, helper.CountPagesWithoutLast);
            // the last one is empty
        }
        #endregion

        #region CountPages
        [TestMethod()]
        public void GetItemsForPage2ItemsInPageWith2Test()
        {
            var items = new int[] { 1, 2 };
            var helper = new PDFKeyValuesHelper<int>(items, 2);

            Assert.AreEqual(2, helper.GetItemsForPage(0).Count());
            Assert.AreEqual(0, helper.GetItemsForPage(1).Count());

            CollectionAssert.AreEqual(new int[] { 1, 2 }, helper.GetItemsForPage(0).ToArray());
            CollectionAssert.AreEqual(new int[] { }, helper.GetItemsForPage(1).ToArray());
        }

        [TestMethod()]
        public void GetItemsForPage3ItemsInPageWith2Test()
        {
            var items = new int[] { 1, 2, 3 };
            var helper = new PDFKeyValuesHelper<int>(items, 2);

            Assert.AreEqual(2, helper.GetItemsForPage(0).Count());
            Assert.AreEqual(1, helper.GetItemsForPage(1).Count());

            CollectionAssert.AreEqual(new int[] { 1, 2 }, helper.GetItemsForPage(0).ToArray());
            CollectionAssert.AreEqual(new int[] { 3 }, helper.GetItemsForPage(1).ToArray());
        }

        [TestMethod()]
        public void GetItemsForPage4ItemsInPageWith3Test()
        {
            var items = new int[] { 1, 2, 3, 4 };
            var helper = new PDFKeyValuesHelper<int>(items, 3);

            Assert.AreEqual(3, helper.GetItemsForPage(0).Count());
            Assert.AreEqual(1, helper.GetItemsForPage(1).Count());

            CollectionAssert.AreEqual(new int[] { 1, 2, 3 }, helper.GetItemsForPage(0).ToArray());
            CollectionAssert.AreEqual(new int[] { 4 }, helper.GetItemsForPage(1).ToArray());
        }

        [TestMethod()]
        public void GetItemsForPage5ItemsInPageWith3Test()
        {
            var items = new int[] { 1, 2, 3, 4, 5 };
            var helper = new PDFKeyValuesHelper<int>(items, 3);

            Assert.AreEqual(3, helper.GetItemsForPage(0).Count());
            Assert.AreEqual(2, helper.GetItemsForPage(1).Count());

            CollectionAssert.AreEqual(new int[] { 1, 2, 3 }, helper.GetItemsForPage(0).ToArray());
            CollectionAssert.AreEqual(new int[] { 4, 5 }, helper.GetItemsForPage(1).ToArray());
        }

        [TestMethod()]
        public void GetItemsForPage6ItemsInPageWith3Test()
        {
            var items = new int[] { 1, 2, 3, 4, 5, 6 };
            var helper = new PDFKeyValuesHelper<int>(items, 3);

            Assert.AreEqual(3, helper.GetItemsForPage(0).Count());
            Assert.AreEqual(3, helper.GetItemsForPage(1).Count());

            CollectionAssert.AreEqual(new int[] { 1, 2, 3 }, helper.GetItemsForPage(0).ToArray());
            CollectionAssert.AreEqual(new int[] { 4, 5, 6 }, helper.GetItemsForPage(1).ToArray());
        }

        [TestMethod()]
        public void GetItemsForPage7ItemsInPageWith3Test()
        {
            var items = new int[] { 1, 2, 3, 4, 5, 6, 7 };
            var helper = new PDFKeyValuesHelper<int>(items, 3);

            Assert.AreEqual(helper.GetItemsForPage(0).Count(), 3);
            Assert.AreEqual(helper.GetItemsForPage(1).Count(), 3);
            Assert.AreEqual(helper.GetItemsForPage(2).Count(), 1);

            CollectionAssert.AreEqual(new int[] { 1, 2, 3 }, helper.GetItemsForPage(0).ToArray());
            CollectionAssert.AreEqual(new int[] { 4, 5, 6 }, helper.GetItemsForPage(1).ToArray());
            CollectionAssert.AreEqual(new int[] { 7 }, helper.GetItemsForPage(2).ToArray());
        }

        [TestMethod()]
        public void GetItemsForPage3ItemsInPageWith3And2Test()
        {
            var items = new int[] { 1, 2, 3 };
            var helper = new PDFKeyValuesHelper<int>(items, 3, 2, true);

            Assert.AreEqual(3, helper.GetItemsForPage(0).Count());
            Assert.AreEqual(0, helper.GetItemsForPage(1).Count());

            CollectionAssert.AreEqual(new int[] { 1, 2, 3 }, helper.GetItemsForPage(0).ToArray());
            CollectionAssert.AreEqual(new int[] { }, helper.GetItemsForPage(1).ToArray());
        }

        [TestMethod()]
        public void GetItemsForPage4ItemsInPageWith3And2Test()
        {
            var items = new int[] { 1, 2, 3, 4 };
            var helper = new PDFKeyValuesHelper<int>(items, 3, 2, true);

            Assert.AreEqual(3, helper.GetItemsForPage(0).Count());
            Assert.AreEqual(1, helper.GetItemsForPage(1).Count());

            CollectionAssert.AreEqual(new int[] { 1, 2, 3 }, helper.GetItemsForPage(0).ToArray());
            CollectionAssert.AreEqual(new int[] { 4 }, helper.GetItemsForPage(1).ToArray());
        }

        [TestMethod()]
        public void GetItemsForPage5ItemsInPageWith3And2Test()
        {
            var items = new int[] { 1, 2, 3, 4, 5 };
            var helper = new PDFKeyValuesHelper<int>(items, 3, 2, true);

            Assert.AreEqual(3, helper.GetItemsForPage(0).Count());
            Assert.AreEqual(2, helper.GetItemsForPage(1).Count());


            CollectionAssert.AreEqual(new int[] { 1, 2, 3 }, helper.GetItemsForPage(0).ToArray());
            CollectionAssert.AreEqual(new int[] { 4, 5 }, helper.GetItemsForPage(1).ToArray());
        }

        [TestMethod()]
        public void GetItemsForPage6ItemsInPageWith3And2Test()
        {
            var items = new int[] { 1, 2, 3, 4, 5, 6 };
            var helper = new PDFKeyValuesHelper<int>(items, 3, 2, true);

            Assert.AreEqual(3, helper.GetItemsForPage(0).Count());
            Assert.AreEqual(3, helper.GetItemsForPage(1).Count());
            Assert.AreEqual(0, helper.GetItemsForPage(2).Count());

            CollectionAssert.AreEqual(new int[] { 1, 2, 3 }, helper.GetItemsForPage(0).ToArray());
            CollectionAssert.AreEqual(new int[] { 4, 5, 6 }, helper.GetItemsForPage(1).ToArray());
            CollectionAssert.AreEqual(new int[] { }, helper.GetItemsForPage(2).ToArray());
        }
        #endregion
    }
}