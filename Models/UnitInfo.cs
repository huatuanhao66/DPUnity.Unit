using System;
using System.Collections.Generic;
using System.Text;

namespace DPUnity.Unit.Models
{
    public class UnitInfo<TUnit>
    {
        public TUnit Unit { get; set; }
        public string DisplayName { get; set; }
        public string Symbol { get; set; }
        public double FactorToBase { get; set; }
    }
}
