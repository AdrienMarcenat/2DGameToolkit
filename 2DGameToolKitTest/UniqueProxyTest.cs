using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace _2DGameToolKitTest
{
    [TestClass]
    public class UniqueProxyTest
    {
        class DummyClass
        { }

        class DummyClassProxy : UniqueProxy<DummyClass>
        { }

        [TestCleanup]
        public void TestCleanup ()
        {
            if (DummyClassProxy.IsValid ())
            {
                DummyClassProxy.Close ();
            }
        }

        [TestMethod]
        public void TestOpen ()
        {
            DummyClass dummyClass = new DummyClass ();
            DummyClassProxy.Open (dummyClass);
            Assert.IsTrue (DummyClassProxy.IsValid ());
            Assert.AreEqual (dummyClass, DummyClassProxy.Get ());
        }

        [TestMethod]
        public void TestCloseAfterOpen ()
        {
            DummyClass dummyClass = new DummyClass ();
            DummyClassProxy.Open (dummyClass);

            DummyClassProxy.Close ();
            Assert.ThrowsException<NUnit.Framework.AssertionException> (delegate { DummyClassProxy.Get (); });
        }

        [TestMethod]
        public void TestCloseBeforeOpen ()
        {
            Assert.ThrowsException<NUnit.Framework.AssertionException> (delegate { DummyClassProxy.Close (); });
        }

        [TestMethod]
        public void TestDoubleOpen ()
        {
            DummyClass dummyClass = new DummyClass ();
            DummyClassProxy.Open (dummyClass);
            Assert.AreEqual (dummyClass, DummyClassProxy.Get ());
            Assert.ThrowsException<NUnit.Framework.AssertionException> (delegate { DummyClassProxy.Open (dummyClass); });
        }
    }
}
