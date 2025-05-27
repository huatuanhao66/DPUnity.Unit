using DPUnity.Unit.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DPUnity.Unit.Services
{
    public class UnitCollection<TUnit> where TUnit : Enum
    {
        public List<UnitInfo<TUnit>> Units { get; set; }
        public TUnit DefaultFromUnit { get; set; }
        public TUnit DefaultToUnit { get; set; }

        public UnitCollection(List<UnitInfo<TUnit>> units, TUnit defaultFrom, TUnit defaultTo)
        {
            Units = units;
            DefaultFromUnit = defaultFrom;
            DefaultToUnit = defaultTo;
        }
    }
}
