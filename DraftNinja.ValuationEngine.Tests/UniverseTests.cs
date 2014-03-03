using System;
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
    }
}
