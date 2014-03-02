using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftNinja.ValuationEngine.Errors
{
    public class PrecalculationNameConflictError : System.Exception
    {
        public PrecalculationNameConflictError() : base() { }
        public PrecalculationNameConflictError(string msg) : base(msg) { }
    }

    public class UnknownPrecalculatedValueError : System.Exception
    {
        public UnknownPrecalculatedValueError() : base() { }
        public UnknownPrecalculatedValueError(string msg) : base(msg) { }
    }

}
