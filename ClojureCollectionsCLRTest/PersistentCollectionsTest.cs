using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using ClojureCollectionsCLR;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClojureCollectionsCLRTest
{
    [TestClass]
    public class PersistentCollectionsTest
    {

        [TestMethod]
        public void PersistentListTests()
        {
            IPersistentList<int> target = new PersistentList<int>();
            target = target.cons(1);
            target = target.cons(5);
            target = target.cons(10);

            Assert.AreEqual(3, target.count());
            Assert.AreEqual(10, target.peek());

            target = target.pop();

            Assert.AreEqual(2, target.count());
            Assert.AreEqual(5, target.peek());

            var init = new List<int> { 5, 1 };

            var target2 = new PersistentList<int>(init);

            Assert.IsTrue(target.equiv(target2));

            target = target.without(1);
            Assert.AreEqual(1, target.count());
            Assert.AreEqual(5, target.peek());

            target = target.empty();

            Assert.AreEqual(0, target.count());
        }

        [TestMethod]
        public void PersistentVectorTests()
        {
            IPersistentVector<int> target = new PersistentVector<int>();

            target = target.cons(1);
            target = target.cons(5);
            target = target.cons(10);
            target = target.cons(20);

            Assert.AreEqual(4, target.count());
            Assert.AreEqual(4, target.length());
            Assert.AreEqual(20, target.peek());
            Assert.AreEqual(5, target.nth(1));
            Assert.AreEqual(5, target.valAt(1));
            Assert.AreEqual(5, target.valAt(1, 666));
            Assert.AreEqual(666, target.valAt(5, 666));
            Assert.AreEqual(5, target.entryAt(1).val());
            Assert.IsTrue(target.containsKey(2));



            target = target.pop();

            Assert.AreEqual(3, target.count());
            Assert.AreEqual(10, target.peek());

            target = target.assocN(1, 9);
            Assert.AreEqual(3, target.count());
            Assert.AreEqual(9, target.nth(1));

            try
            {
                target = target.assocN(15, 8);
                Assert.Fail();
            }
            catch (Exception)
            {
                //Expected exception
            }

            IPersistentVector<int> target2 = new PersistentVector<int>(new[]{3, 56, 55, 8});
            IPersistentVector<int> target3 = new PersistentVector<int>(new[]{3, 56, 55, 8});

            Assert.IsTrue(target2.equiv(target3));

            target = target.cons(-9);
            target = target.subVec(2, 4);
            Assert.AreEqual(2, target.count());
            Assert.AreEqual(10, target.nth(0));
            Assert.AreEqual(-9, target.nth(1));

            target = target.without(10);
            Assert.AreEqual(1, target.count());
            Assert.AreEqual(-9, target.peek());

            target = target.empty();

            Assert.AreEqual(0, target.count());
        }

        [TestMethod]
        public void PersistentMapTests()
        {
            IPersistentMap<String, String> target = new PersistentHashMap<String, String>();

            target = target.assoc("k1", "v1");
            target = target.assoc("k2", "v2");
            target = target.assoc("k3", "v3");
            target = target.assoc("k4", "v4");

            Assert.AreEqual(4, target.count());
            Assert.IsTrue(target.contiansKey("k3"));
            Assert.IsFalse(target.contiansKey("x"));
            Assert.AreEqual("k3", target.entryAt("k3").key());
            Assert.AreEqual("v3", target.entryAt("k3").val());
            Assert.AreEqual("v3", target.valAt("k3"));
            Assert.AreEqual("nf", target.valAt("x", "nf"));

            target = target.assoc("k2", "vx");

            Assert.AreEqual(4, target.count());
            Assert.AreEqual("vx", target.valAt("k2"));

            try
            {
                target = target.assocEx("k2", "vy");
                Assert.Fail();
            }
            catch (Exception)
            {
                //Expected exception
            }

            Assert.AreEqual(4, target.count());
            Assert.AreEqual("vx", target.valAt("k2"));

            target = target.without("k3");

            Assert.AreEqual(3, target.count());
            Assert.AreEqual("v4", target.valAt("k4"));

            target = target.cons(new MapEntry<String, String>("k5", "v5"));

            Assert.AreEqual(4, target.count());
            Assert.AreEqual("v5", target.valAt("k5"));

            Assert.IsNull(target.valAt("x"));
            Assert.IsNull(target.entryAt("x"));

            target = target.assoc(null, "null");
            target = target.assoc("null", null);

            Assert.AreEqual(6, target.count());
            Assert.AreEqual("null", target.valAt(null));
            Assert.IsNull(target.valAt("null"));

            foreach (IMapEntry<String, String> mapEntry in target)
            {
                if (mapEntry.key() != null && mapEntry.key().Equals("null"))
                {
                    Assert.IsNull(mapEntry.val());
                }
            }

            target = target.empty();

            Assert.AreEqual(0, target.count());

            target = target.assoc("x", "y");
            target = target.assoc("z", "a");

            IPersistentMap<String, String> target2 = new PersistentHashMap<String, String>();

            target2 = target2.assoc("x", "y");
            target2 = target2.assoc("z", "a");

            Assert.IsTrue(target.equiv(target2));

            target2 = target2.without("z");
            target2 = target2.without("b");

            Assert.IsFalse(target.equiv(target2));
        }
    }
}
