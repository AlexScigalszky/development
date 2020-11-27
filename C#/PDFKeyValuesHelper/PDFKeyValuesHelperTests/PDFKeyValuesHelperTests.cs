using PDFKeyValuesHelper;
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

        [TestMethod()]
        public void CountPages22ItemsInPageWith11And11Test()
        {
            var items = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22 };
            var helper = new PDFKeyValuesHelper<int>(items, 11, 11, true);

            Assert.AreEqual(2, helper.CountPages);
            Assert.AreEqual(1, helper.CountPagesWithoutLast);
        }

        [TestMethod()]
        public void CountPages23ItemsInPageWith11And11Test()
        {
            var items = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23};
            var helper = new PDFKeyValuesHelper<int>(items, 11, 11, true);

            Assert.AreEqual(3, helper.CountPages);
            Assert.AreEqual(2, helper.CountPagesWithoutLast);
        }

        [TestMethod()]
        public void CountPages24ItemsInPageWith11And11Test()
        {
            var items = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 };
            var helper = new PDFKeyValuesHelper<int>(items, 11, 11, true);

            Assert.AreEqual(3, helper.CountPages);
            Assert.AreEqual(2, helper.CountPagesWithoutLast);
        }
        #endregion

        #region GetItemsForPage
        [TestMethod()]
        public void GetItemsForPage2ItemsInPageWith2Test()
        {
            var items = new int[] { 1, 2 };
            var helper = new PDFKeyValuesHelper<int>(items, 2);

            Assert.AreEqual(0, helper.GetItemsForPage(0).Count());
            Assert.AreEqual(2, helper.GetItemsForLastPage().Count());

            CollectionAssert.AreEqual(new int[] { }, helper.GetItemsForPage(0).ToArray());
            CollectionAssert.AreEqual(new int[] { 1, 2 }, helper.GetItemsForLastPage().ToArray());
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

        [TestMethod()]
        public void GetItemsForPage10ItemsInPageWith11And11Test()
        {
            var items = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var helper = new PDFKeyValuesHelper<int>(items, 11, 11, true);

            Assert.AreEqual(0, helper.GetItemsForPage(0).Count());
            Assert.AreEqual(10, helper.GetItemsForLastPage().Count());

            CollectionAssert.AreEqual(new int[] { }, helper.GetItemsForPage(0).ToArray());
            CollectionAssert.AreEqual(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, helper.GetItemsForLastPage().ToArray());
        }

        [TestMethod()]
        public void GetItemsForPage11ItemsInPageWith11And11Test()
        {
            var items = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
            var helper = new PDFKeyValuesHelper<int>(items, 11, 11, true);

            Assert.AreEqual(0, helper.GetItemsForPage(0).Count());
            Assert.AreEqual(11, helper.GetItemsForLastPage().Count());

            CollectionAssert.AreEqual(new int[] { }, helper.GetItemsForPage(0).ToArray());
            CollectionAssert.AreEqual(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 }, helper.GetItemsForLastPage().ToArray());
        }

        [TestMethod()]
        public void GetItemsForPage12ItemsInPageWith11And11Test()
        {
            var items = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            var helper = new PDFKeyValuesHelper<int>(items, 11, 11, true);

            Assert.AreEqual(11, helper.GetItemsForPage(0).Count());
            Assert.AreEqual(1, helper.GetItemsForPage(1).Count());

            CollectionAssert.AreEqual(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 }, helper.GetItemsForPage(0).ToArray());
            CollectionAssert.AreEqual(new int[] { 12 }, helper.GetItemsForPage(1).ToArray());
        }

        [TestMethod()]
        public void GetItemsForPage13ItemsInPageWith11And11Test()
        {
            var items = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };
            var helper = new PDFKeyValuesHelper<int>(items, 11, 11, true);

            Assert.AreEqual(11, helper.GetItemsForPage(0).Count());
            Assert.AreEqual(2, helper.GetItemsForPage(1).Count());

            CollectionAssert.AreEqual(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 }, helper.GetItemsForPage(0).ToArray());
            CollectionAssert.AreEqual(new int[] { 12, 13 }, helper.GetItemsForPage(1).ToArray());
        }

        [TestMethod()]
        public void GetItemsForPage22ItemsInPageWith11And11Test()
        {
            var items = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22 };
            var helper = new PDFKeyValuesHelper<int>(items, 11, 11, true);

            Assert.AreEqual(11, helper.GetItemsForPage(0).Count());
            Assert.AreEqual(11, helper.GetItemsForLastPage().Count());

            CollectionAssert.AreEqual(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 }, helper.GetItemsForPage(0).ToArray());
            CollectionAssert.AreEqual(new int[] { 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22 }, helper.GetItemsForLastPage().ToArray());
        }

        [TestMethod()]
        public void GetItemsForPage23ItemsInPageWith11And11Test()
        {
            var items = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23 };
            var helper = new PDFKeyValuesHelper<int>(items, 11, 11, true);

            Assert.AreEqual(11, helper.GetItemsForPage(0).Count());
            Assert.AreEqual(11, helper.GetItemsForPage(1).Count());
            Assert.AreEqual(1, helper.GetItemsForLastPage().Count());


            CollectionAssert.AreEqual(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 }, helper.GetItemsForPage(0).ToArray());
            CollectionAssert.AreEqual(new int[] { 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22 }, helper.GetItemsForPage(1).ToArray());
            CollectionAssert.AreEqual(new int[] { 23 }, helper.GetItemsForLastPage().ToArray());
        }

        [TestMethod()]
        public void GetItemsForPage24ItemsInPageWith11And11Test()
        {
            var items = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 };
            var helper = new PDFKeyValuesHelper<int>(items, 11, 11, true);

            Assert.AreEqual(11, helper.GetItemsForPage(0).Count());
            Assert.AreEqual(11, helper.GetItemsForPage(1).Count());
            Assert.AreEqual(2, helper.GetItemsForLastPage().Count());


            CollectionAssert.AreEqual(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 }, helper.GetItemsForPage(0).ToArray());
            CollectionAssert.AreEqual(new int[] { 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22 }, helper.GetItemsForPage(1).ToArray());
            CollectionAssert.AreEqual(new int[] { 23, 24 }, helper.GetItemsForLastPage().ToArray());
        }
        #endregion

        #region CountPagesWithoutLast
        [TestMethod()]
        public void CountPagesWithoutLast11ItemsInPageWith11And11Test()
        {
            var items = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
            var helper = new PDFKeyValuesHelper<int>(items, 11, 11, true);

            Assert.AreEqual(0, helper.CountPagesWithoutLast);
        }

        [TestMethod()]
        public void CountPagesWithoutLast22ItemsInPageWith11And11Test()
        {
            var items = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22};
            var helper = new PDFKeyValuesHelper<int>(items, 11, 11, true);

            Assert.AreEqual(1, helper.CountPagesWithoutLast);
        }

        [TestMethod()]
        public void CountPagesWithoutLast23ItemsInPageWith11And11Test()
        {
            var items = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23};
            var helper = new PDFKeyValuesHelper<int>(items, 11, 11, true);

            Assert.AreEqual(2, helper.CountPagesWithoutLast);
        }

        [TestMethod()]
        public void CountPagesWithoutLast24ItemsInPageWith11And11Test()
        {
            var items = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 };
            var helper = new PDFKeyValuesHelper<int>(items, 11, 11, true);

            Assert.AreEqual(2, helper.CountPagesWithoutLast);
        }
        #endregion
    }
}