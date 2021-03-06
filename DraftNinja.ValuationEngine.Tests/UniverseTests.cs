﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using DraftNinja.ValuationEngine;
using DraftNinja.ValuationEngine.Tests.SampleClasses;

namespace DraftNinja.ValuationEngine.Tests
{
    [TestClass]
    public class UniverseTests
    {
        public static readonly double MaximumDeltaBetweenEqualDoubles = 0.00001;

        [TestMethod]
        public void TestLazyPrecalculation()
        {
            var bob = new Bowler("Bob", 90);
            var karen = new Bowler("Karen", 110);
            
            var universe = new Universe<Bowler> { bob, karen };
            universe.AddPrecalculation("average-score", u => (from p in u select p.AverageScore).Average());
            Assert.AreEqual(universe["average-score"], 100.0, MaximumDeltaBetweenEqualDoubles);
        }

        [TestMethod]
        public void TestLazyPrecalculationsWithLateAdd()
        {
            var bob = new Bowler("Bob", 90);
            var karen = new Bowler("Karen", 110);

            var universe = new Universe<Bowler> { bob, karen };
            universe.AddPrecalculation("average-score", u => (from p in u select p.AverageScore).Average());
            Assert.AreEqual(universe["average-score"], 100.0, MaximumDeltaBetweenEqualDoubles);

            universe.Add(new Bowler("Chuck", 130));
            Assert.AreEqual(universe["average-score"], 110.0, MaximumDeltaBetweenEqualDoubles);
        }


        [TestMethod]
        public void TestLazyPrecalculationsWithLateRemove()
        {
            var bob = new Bowler("Bob", 90);
            var karen = new Bowler("Karen", 110);
            var chuck = new Bowler("Chuck", 130);

            var universe = new Universe<Bowler> { bob, karen, chuck };
            universe.AddPrecalculation("average-score", u => (from p in u select p.AverageScore).Average());
            Assert.AreEqual(universe["average-score"], 110.0, MaximumDeltaBetweenEqualDoubles);

            universe.Remove(chuck);
            Assert.AreEqual(universe["average-score"], 100.0, MaximumDeltaBetweenEqualDoubles);
        }


    }
}
