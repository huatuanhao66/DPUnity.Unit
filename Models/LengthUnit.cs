using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DPUnity.Unit.Models
{
    public enum LengthUnit
    {
        Meter,
        Decimeter,
        Centimeter,
        Millimeter,
        Kilometer,
        Foot,
        Inch
    }

    public static class LengthUnits
    {
        public static readonly List<UnitInfo<LengthUnit>> Units = new List<UnitInfo<LengthUnit>>
        {   
            new UnitInfo<LengthUnit> { Unit = LengthUnit.Meter, DisplayName = "Meter", Symbol = "m", FactorToBase = 1 },
            new UnitInfo<LengthUnit> { Unit = LengthUnit.Decimeter, DisplayName = "Decimeter", Symbol = "dm", FactorToBase = 0.1 },
            new UnitInfo <LengthUnit> { Unit = LengthUnit.Centimeter, DisplayName = "Centimeter", Symbol = "cm", FactorToBase = 0.01 },
            new UnitInfo <LengthUnit> { Unit = LengthUnit.Millimeter, DisplayName = "Milimeter", Symbol = "mm", FactorToBase = 0.001 },
            new UnitInfo <LengthUnit> { Unit = LengthUnit.Kilometer, DisplayName = "Kilometer", Symbol = "km", FactorToBase = 1000 },
            new UnitInfo<LengthUnit> { Unit = LengthUnit.Inch, DisplayName = "Inch", Symbol = "in", FactorToBase = 0.0254 },
            new UnitInfo<LengthUnit> { Unit = LengthUnit.Foot, DisplayName = "Foot", Symbol = "ft", FactorToBase = 0.3048 }
        };

        public static readonly LengthUnit DefaultFromUnit = LengthUnit.Meter;
        public static readonly LengthUnit DefaultToUnit = LengthUnit.Kilometer;

        public static string FormatStation(double value)
        {
            int km = (int)(value / 1000);
            double meters = (value % 1000);
            int mInt = (int)meters; 
            double mFrac = meters - mInt;
            int mDecimal = (int)Math.Round(mFrac * 1000);

            return $"KM {km}+{mInt:D3}.{mDecimal:D3}";
        }
    }
}
