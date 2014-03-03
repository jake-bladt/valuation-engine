using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DraftNinja.ValuationEngine.Errors;

namespace DraftNinja.ValuationEngine
{
    public class Universe<T> : List<T>
    {
        protected Dictionary<string, Func<Universe<T>, double>> Precalculations;
        protected Dictionary<string, double?> PrecalculatedValues;
        protected bool DirtyCalculationsFlag;

        public Universe()
        {
            Precalculations = new Dictionary<string, Func<Universe<T>, double>>();
            PrecalculatedValues = new Dictionary<string, double?>();
            DirtyCalculationsFlag = false;
        }

        public void AddPrecalculation(string name, Func<Universe<T>, double> calculation)
        {
            if (Precalculations.ContainsKey(name)) throw new PrecalculationNameConflictError(
                 String.Format("There is already a precalculation named {0}.", name));
            Precalculations[name] = calculation;
            PrecalculatedValues[name] = null;
        }

        public void AddPrecalculations(Dictionary<string, Func<Universe<T>, double>> calculations)
        {
            calculations.ToList().ForEach(kvp => AddPrecalculation(kvp.Key, kvp.Value));
        }

        public void ForceInitializePrecalculations()
        {
            Precalculations.Keys.ToList().ForEach(key => Precalculate(key));
        }

        public double this[string index]
        {
            get
            {
                if (Precalculations.ContainsKey(index))
                {
                    if (null == PrecalculatedValues[index]) Precalculate(index);
                    return (double)PrecalculatedValues[index];
                }
                else
                {
                    throw new UnknownPrecalculatedValueError(String.Format(
                        "Unknown value: {0}.", index));
                }
            }
        }

        protected void Precalculate(string index)
        {
            PrecalculatedValues[index] = Precalculations[index](this);
            DirtyCalculationsFlag = true;
        }

        public new void Add(T elem)
        {
            if (DirtyCalculationsFlag) ClearPrecalculations();
            base.Add(elem);
        }

        public new void Remove(T elem)
        {
            if (DirtyCalculationsFlag) ClearPrecalculations();
            base.Remove(elem);
        }

        public new void Clear()
        {
            if (DirtyCalculationsFlag) ClearPrecalculations();
            base.Clear();
        }

        protected void ClearPrecalculations() 
        {
            PrecalculatedValues.Keys.ToList().ForEach(k => PrecalculatedValues[k] = null);
            DirtyCalculationsFlag = false;
        }

    }
}
