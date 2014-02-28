using System;
using System.Collections.Generic;
using ClojureCollectionsCLR;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClojureCollectionsCLRTest
{
    [TestClass]
    public class PersistentListTest
    {
        [TestMethod]
        public void PersistentListTests()
        {
            IPersistentList<int> target = new PersistentList<int>();
            target = target.Cons(1);
            target = target.Cons(5);
            target = target.Cons(10);

            Assert.AreEqual(3, target.Count);
            Assert.AreEqual(10, target.Peek());

            target = target.Pop();

            Assert.AreEqual(2, target.Count);
            Assert.AreEqual(5, target.Peek());

            var init = new List<int> { 5, 1 };

            var target2 = new PersistentList<int>(init);

            Assert.IsTrue(target.Equiv(target2));
            Assert.AreEqual(target, target2);
            Assert.AreEqual(target.GetHashCode(), target2.GetHashCode());
            Assert.AreNotSame(target, target2);

            target = target.Without(1);
            Assert.AreEqual(1, target.Count);
            Assert.AreEqual(5, target.Peek());

            target = target.Empty();

            Assert.AreEqual(0, target.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PeekOnEmptyList()
        {
            IPersistentList<int> target = new PersistentList<int>();

            target.Peek();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PopOnEmptyList()
        {
            IPersistentList<int> target = new PersistentList<int>();

            target.Pop();
        }
    }
}
