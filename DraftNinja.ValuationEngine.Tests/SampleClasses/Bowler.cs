using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftNinja.ValuationEngine.Tests.SampleClasses
{
    public class Bowler
    {
        public string Name { get; set; }
        public int AverageScore { get; set; }

        public Bowler(string name, int avg)
        {
            Name = name;
            AverageScore = avg;
        }
    }
}
