using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

namespace FluentMongo.Session.Tracking
{
    [TestFixture]
    public class StandardChangeTrackerTests
    {
        private class TestClassA
        { }

        [Test]
        public void Should_track_full_objects()
        {
            var a = new TestClassA();
            var tracker = new StandardChangeTracker();
            var trackedObj = tracker.Track(a);

            var trackedObjB = tracker.GetTrackedObject(a);
            Assert.AreSame(trackedObj, trackedObjB);
            Assert.IsTrue(tracker.IsTracked(a));
        }


    }
}