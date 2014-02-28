using System;
using System.Collections.Generic;
using System.Linq;
using ClojureCollectionsCLR;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClojureCollectionsCLRTest
{
    [TestClass]
    public class PersistentCollectionsTest
    {
        #region PersistentList
        [TestMethod]
        public void PersistentListTests()
        {
            IPersistentList<int> target = new PersistentList<int>();
            target = target.Cons(1);
            target = target.Cons(5);
            target = target.Cons(10);

            Assert.AreEqual(3, target.Count());
            Assert.AreEqual(10, target.Peek());

            target = target.Pop();

            Assert.AreEqual(2, target.Count());
            Assert.AreEqual(5, target.Peek());

            var init = new List<int> { 5, 1 };

            var target2 = new PersistentList<int>(init);

            Assert.IsTrue(target.Equiv(target2));
            Assert.AreEqual(target, target2);
            Assert.AreEqual(target.GetHashCode(), target2.GetHashCode());
            Assert.AreNotSame(target, target2);

            target = target.Without(1);
            Assert.AreEqual(1, target.Count());
            Assert.AreEqual(5, target.Peek());

            target = target.Empty();

            Assert.AreEqual(0, target.Count());
        }
        #endregion

        #region PersistentVector
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

            IPersistentVector<int> target2 = new PersistentVector<int>(new[]{3, 56, 55, 8});
            IPersistentVector<int> target3 = new PersistentVector<int>(new[]{3, 56, 55, 8});

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
        #endregion

        #region PersistentMap
        [TestMethod]
        public void PersistentMapTests()
        {
            IPersistentMap<String, String> target = new PersistentHashMap<String, String>();

            target = target.Assoc("k1", "v1");
            target = target.Assoc("k2", "v2");
            target = target.Assoc("k3", "v3");
            target = target.Assoc("k4", "v4");

            Assert.AreEqual(4, target.Count);
            Assert.IsTrue(target.ContainsKey("k3"));
            Assert.IsFalse(target.ContainsKey("x"));
            Assert.AreEqual("k3", target.EntryAt("k3").Key);
            Assert.AreEqual("v3", target.EntryAt("k3").Value);
            Assert.AreEqual("v3", target.ValAt("k3"));
            Assert.AreEqual("nf", target.ValAt("x", "nf"));

            target = target.Assoc("k2", "vx");

            Assert.AreEqual(4, target.Count);
            Assert.AreEqual("vx", target.ValAt("k2"));

            try
            {
                target = target.AssocEx("k2", "vy");
                Assert.Fail();
            }
            catch (Exception)
            {
                //Expected exception
            }

            Assert.AreEqual(4, target.Count);
            Assert.AreEqual("vx", target.ValAt("k2"));

            target = target.Without("k3");

            Assert.AreEqual(3, target.Count);
            Assert.AreEqual("v4", target.ValAt("k4"));

            target = target.Cons(new KeyValuePair<string, string>("k5", "v5"));

            Assert.AreEqual(4, target.Count);
            Assert.AreEqual("v5", target.ValAt("k5"));

            target = target.Assoc(null, "null");
            target = target.Assoc("null", null);

            Assert.AreEqual(6, target.Count);
            Assert.AreEqual("null", target.ValAt(null));
            Assert.IsNull(target.ValAt("null"));

            foreach (KeyValuePair<string, string> mapEntry in target)
            {
                if (mapEntry.Key != null && mapEntry.Key.Equals("null"))
                {
                    Assert.IsNull(mapEntry.Value);
                }
            }

            target = target.Empty();

            Assert.AreEqual(0, target.Count);

            target = target.Assoc("x", "y");
            target = target.Assoc("z", "a");

            IPersistentMap<String, String> target2 = new PersistentHashMap<String, String>();

            target2 = target2.Assoc("x", "y");
            target2 = target2.Assoc("z", "a");

            Assert.IsTrue(target.Equiv(target2));
            Assert.AreEqual(target, target2);
            Assert.AreEqual(target.GetHashCode(), target2.GetHashCode());

            target2 = target2.Without("z");
            target2 = target2.Without("b");

            Assert.IsFalse(target.Equiv(target2));
            Assert.AreNotEqual(target, target2);
        }

        [TestMethod]
        public void CreatePersistentHashMapFromDictonary()
        {
            var dict = new Dictionary<Int32, Int32>();
            dict[1] = 1;
            dict[3] = 3;

            var target = new PersistentHashMap<Int32, Int32>(dict);

            Assert.AreEqual(1, target.ValAt(1));
            Assert.AreEqual(3, target.ValAt(3));
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void RetriveValueWithUnknownKeyViaEntryAt()
        {
            IPersistentMap<string, string> target = new PersistentHashMap<string, string>();
            target = target.Assoc("a", "b");

            target.EntryAt("x");
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void RetriveValueWithUnknownKeyViaValAt()
        {
            IPersistentMap<string, string> target = new PersistentHashMap<string, string>();
            target = target.Assoc("a", "b");

            target.ValAt("x");
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void RetriveValueWithUnknownKeyForPrimitiveTypesViaEntryAt()
        {
            IPersistentMap<int, int> target = new PersistentHashMap<int, int>();
            target = target.Assoc(1, 2);

            target.EntryAt(3);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void RetriveValueWithUnknownKeyForPrimitiveTypesViaValAt()
        {
            IPersistentMap<int, int> target = new PersistentHashMap<int, int>();
            target = target.Assoc(1, 2);

            target.ValAt(3);
        }
        #endregion
    }
}
