using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using ClojureCollectionsCLR;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClojureCollectionsCLRTest
{
    [TestClass]
    public class PersistentMapTest
    {
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

            Assert.AreEqual(target, target2);
            Assert.AreEqual(target, target2);
            Assert.AreEqual(target.GetHashCode(), target2.GetHashCode());

            target2 = target2.Without("z");
            target2 = target2.Without("b");

            Assert.AreNotEqual(target, target2);
            Assert.IsFalse(target == target2);
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
    }
}
