using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using DraftNinja.ValuationEngine;
using DraftNinja.ValuationEngine.Tests.SampleClasses;

namespace DraftNinja.ValuationEngine.Tests
{
    [TestClass]
    public class ComparativeValuationEngineTests
    {
        public static readonly double MaximumDeltaBetweenEqualDoubles = 0.00001;

        [TestMethod]
        public void TestSimpleLinearValuation()
        {
            var bob = new Bowler("Bob", 90);
            var karen = new Bowler("Karen", 110);
            var bowlers = new List<Bowler> { bob, karen };
            var engine = new ComparativeValuationEngine<Bowler>();
            var valuations = engine.GetValuations(bowlers, ((player, universe) =>
            {
                var avg = (from p in universe select p.AverageScore).Average();
                return player.AverageScore / avg;
            }));

            Assert.AreEqual(valuations[bob], 0.9, MaximumDeltaBetweenEqualDoubles);
            Assert.AreEqual(valuations[karen], 1.1, MaximumDeltaBetweenEqualDoubles);
        }

        [TestMethod]
        public void TestSimpleLinearValuationWithTargetAverage()
        {
            var bob = new Bowler("Bob", 90);
            var karen = new Bowler("Karen", 110);
            var bowlers = new List<Bowler> { bob, karen };
            var precalcs = new Dictionary<string, double> { { "targetAverage", 100.0 } };

            var engine = new ComparativeValuationEngine<Bowler>();
            var valuations = engine.GetValuations(bowlers, precalcs, ((player, universe, precalcuted) =>
            {
                var avg = precalcuted["targetAverage"];
                return player.AverageScore / avg;
            }));

            Assert.AreEqual(valuations[bob], 0.9, MaximumDeltaBetweenEqualDoubles);
            Assert.AreEqual(valuations[karen], 1.1, MaximumDeltaBetweenEqualDoubles);
        }

        [TestMethod]
        public void TestExponentialValuation()
        {
            var bob = new Bowler("Bob", 90);
            var karen = new Bowler("Karen", 110);
            var bowlers = new List<Bowler> { bob, karen };
            var engine = new ComparativeValuationEngine<Bowler>();
            var valuations = engine.GetValuations(bowlers, ((player, universe) =>
            {
                var avg = (from p in universe select p.AverageScore).Average();
                return Math.Pow(player.AverageScore / avg, 2);
            }));

            Assert.AreEqual(valuations[bob], 0.81, MaximumDeltaBetweenEqualDoubles);
            Assert.AreEqual(valuations[karen], 1.21, MaximumDeltaBetweenEqualDoubles);
        }

    }
}
