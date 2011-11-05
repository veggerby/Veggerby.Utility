using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Veggerby.Utility.Extensions;

namespace Veggerby.Utility.Test
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class ListExtensionsTest
    {
        [TestMethod]
        public void Contains_CharListContainsSubList_ReturnsIndex()
        {
            //var list = new[] { 'P', 'A', 'R', 'T', 'I', 'C', 'I', 'P', 'A', 'T', 'E', ' ', 'I', 'N', ' ', 'P', 'A', 'R', 'A', 'C', 'H', 'U', 'T', 'E' };
            var list = new[] { 'A', 'B', 'C', ' ', 'A', 'B', 'C', 'D', 'A', 'B', ' ', 'A', 'B', 'C', 'D', 'A', 'B', 'C', 'D', 'A', 'B', 'D', 'E' };
            var subList = new[] { 'A', 'B', 'C', 'D', 'A', 'B', 'D' };
            var index = list.IndexOf(subList);
            Assert.AreEqual(15, index);
        }

        [TestMethod]
        public void Contains_CharListDoesNotContainsSubList_ReturnsMinusOne()
        {
            //var list = new[] { 'P', 'A', 'R', 'T', 'I', 'C', 'I', 'P', 'A', 'T', 'E', ' ', 'I', 'N', ' ', 'P', 'A', 'R', 'A', 'C', 'H', 'U', 'T', 'E' };
            var list = new[] { 'A', 'B', 'C', ' ', 'A', 'B', 'C', 'D', 'A', 'B', ' ', 'A', 'B', 'C', 'D', 'A', 'B', 'C', 'D', 'A', 'B', 'D', 'E' };
            var subList = new[] { 'A', 'B', 'C', 'D', 'A', 'B', 'D', 'F' };
            var index = list.IndexOf(subList);
            Assert.AreEqual(-1, index);
        }

        [TestMethod]
        public void Contains_StringListContainsSubList_ReturnsIndex()
        {
            var list = new[] { "The", "quick", "brown", "fox", "jumps", "over", "the", "lazy", "dog" };
            var subList = new[] { "brown", "fox", "jumps" };
            var index = list.IndexOf(subList);
            Assert.AreEqual(2, index);
        }

        [TestMethod]
        public void Contains_StringListContainsSubListWithOffset_ReturnsIndex()
        {
            var list = new[] { "The", "quick", "brown", "fox", "jumps", "over", "the", "lazy", "dog" };
            var subList = new[] { "brown", "fox", "jumps" };
            var index = list.IndexOf(subList, offset: 2);
            Assert.AreEqual(2, index);
        }

        [TestMethod]
        public void Contains_StringListContainsSubListWithOffsetAndEarlierMatch_ReturnsIndex()
        {
            var list = new[] { "The", "quick", "brown", "fox", "jumps", "brown", "fox", "jumps", "over", "the", "lazy", "dog" };
            var subList = new[] { "brown", "fox", "jumps" };
            var index = list.IndexOf(subList, offset: 3);
            Assert.AreEqual(5, index);
        }

        [TestMethod]
        public void Contains_StringListContainsSubListWithOffsetAfterMatch_ReturnsMinusOne()
        {
            var list = new[] { "The", "quick", "brown", "fox", "jumps", "over", "the", "lazy", "dog" };
            var subList = new[] { "brown", "fox", "jumps" };
            var index = list.IndexOf(subList, offset: 3);
            Assert.AreEqual(-1, index);
        }
    }
}
