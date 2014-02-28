using System;
using System.Collections.Generic;
using ClojureCollectionsCLR;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClojureCollectionsCLRTest
{
    [TestClass]
    public class PersistentVectorTest
    {
        [TestMethod]
        public void PersistentVectorTests()
        {
            IPersistentVector<int> target = new PersistentVector<int>();

            target = target.Cons(1);
            target = target.Cons(5);
            target = target.Cons(10);
            target = target.Cons(20);

            Assert.AreEqual(4, target.Count);
            Assert.AreEqual(4, target.Length());
            Assert.AreEqual(20, target.Peek());
            Assert.AreEqual(5, target.Nth(1));
            Assert.AreEqual(5, target.ValAt(1));
            Assert.AreEqual(5, target.ValAt(1, 666));
            Assert.AreEqual(666, target.ValAt(5, 666));
            Assert.AreEqual(5, target.EntryAt(1).Value);
            Assert.IsTrue(target.ContainsKey(2));


            target = target.Pop();

            Assert.AreEqual(3, target.Count);
            Assert.AreEqual(10, target.Peek());

            target = target.AssocN(1, 9);
            Assert.AreEqual(3, target.Count);
            Assert.AreEqual(9, target.Nth(1));

            try
            {
                target = target.AssocN(15, 8);
                Assert.Fail();
            }
            catch (Exception)
            {
                //Expected exception
            }

            IPersistentVector<int> target2 = new PersistentVector<int>(new[] { 3, 56, 55, 8 });
            IPersistentVector<int> target3 = new PersistentVector<int>(new[] { 3, 56, 55, 8 });

            Assert.IsTrue(target2.Equiv(target3));
            Assert.AreEqual(target2, target3);
            Assert.AreEqual(target2.GetHashCode(), target3.GetHashCode());

            target = target.Cons(-9);
            target = target.SubVec(2, 4);
            Assert.AreEqual(2, target.Count);
            Assert.AreEqual(10, target.Nth(0));
            Assert.AreEqual(-9, target.Nth(1));

            target = target.Without(10);
            Assert.AreEqual(1, target.Count);
            Assert.AreEqual(-9, target.Peek());

            target = target.Empty();

            Assert.AreEqual(0, target.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void TryToGetNonExistentValueForPrimitiveTypeViaValAt()
        {
            IPersistentVector<int> target = new PersistentVector<int>();
            target = target.Cons(1);
            target = target.Cons(2);

            target.ValAt(25);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TryToGetNonExistendValueViaNth()
        {
            IPersistentVector<int> target = new PersistentVector<int>();
            target = target.Cons(1);
            target = target.Cons(2);

            target.Nth(25);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void TryToGetNonExistendValueViaEntryAt()
        {
            IPersistentVector<int> target = new PersistentVector<int>();
            target = target.Cons(1);
            target = target.Cons(2);

            target.EntryAt(25);
        }

        [TestMethod]
        public void StoringNullInAVector()
        {
            IPersistentVector<string> target = new PersistentVector<string>();

            target = target.Cons(null);

            Assert.IsNull(target.ValAt(0));
            Assert.IsNull(target.Nth(0));
        }
    }
}
