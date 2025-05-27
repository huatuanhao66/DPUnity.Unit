using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DPUnity.Unit.Models
{
    public enum AreaUnit
    {
        SquareMeter,
        SquareDecimeter,
        SquareCentimeter,
        SquareMillimeter,
        SquareKilometer,
    }
    
    public static class AreaUnits
    {
        public static readonly List<UnitInfo<AreaUnit>> Units = new List<UnitInfo<AreaUnit>>
        {
            new UnitInfo<AreaUnit> { Unit = AreaUnit.SquareMeter, DisplayName = "Square Meter", Symbol = "m2", FactorToBase = 1 },
            new UnitInfo<AreaUnit> { Unit = AreaUnit.SquareDecimeter, DisplayName = "Square Decimeter", Symbol = "dm2", FactorToBase = 0.01 },
            new UnitInfo<AreaUnit> { Unit = AreaUnit.SquareCentimeter, DisplayName = "Square Centimeter", Symbol = "cm2", FactorToBase = 0.0001 },
            new UnitInfo<AreaUnit> { Unit = AreaUnit.SquareMillimeter, DisplayName = "Square Millimeter", Symbol = "mm2", FactorToBase = 0.000001 },
            new UnitInfo<AreaUnit> { Unit = AreaUnit.SquareKilometer, DisplayName = "Square Kilometer", Symbol = "km2", FactorToBase = 1000000 }
        };

        public static readonly AreaUnit DefaultFromUnit = AreaUnit.SquareMeter;
        public static readonly AreaUnit DefaultToUnit = AreaUnit.SquareKilometer;

        public static UnitInfo<AreaUnit> GetUnitInfo(AreaUnit unit)
        {
            return Units.FirstOrDefault(u => u.Unit == unit);
        }
    }
}
