using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftNinja.ValuationEngine
{
    public class ComparativeValuationEngine<T>
    {
        public Dictionary<T, double> GetValuations(IEnumerable<T> elements, Func<T, IEnumerable<T>, double> strategy)
        {
            // Apply the valuation strategy to each element and return a dictionary of valuations.
            var ret = new Dictionary<T, double>();
            elements.ToList().ForEach(elem => ret[elem] = strategy(elem, elements));
            return ret;
        }

        public Dictionary<T, double> GetValuations(
            IEnumerable<T> elements, 
            Dictionary<string, double> precalculatedValues, 
            Func<T, IEnumerable<T>, Dictionary<string, double>, double> strategy)
        {
            // Apply the valuation strategy to each element and return a dictionary of valuations.
            var ret = new Dictionary<T, double>();
            elements.ToList().ForEach(elem => ret[elem] = strategy(elem, elements, precalculatedValues));
            return ret;
        }

















    
    }
}
