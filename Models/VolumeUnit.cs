using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DPUnity.Unit.Models
{
    public enum VolumeUnit
    {
        CubicMeter,
        CubicDecimeter,
        CubicCentimeter,
        CubicMillimeter,
        CubicKilometer,
    }
    
    public static class VolumeUnits
    {
        public static readonly List<UnitInfo<VolumeUnit>> Units = new List<UnitInfo<VolumeUnit>>
        {
            new UnitInfo<VolumeUnit> { Unit = VolumeUnit.CubicMeter, DisplayName = "Cubic Meter", Symbol = "m3", FactorToBase = 1 },
            new UnitInfo<VolumeUnit> { Unit = VolumeUnit.CubicDecimeter, DisplayName = "Cubic Decimeter", Symbol = "dm3", FactorToBase = 0.001 },
            new UnitInfo<VolumeUnit> { Unit = VolumeUnit.CubicCentimeter, DisplayName = "Cubic Centimeter", Symbol = "cm3", FactorToBase = 0.000001 },
            new UnitInfo<VolumeUnit> { Unit = VolumeUnit.CubicMillimeter, DisplayName = "Cubic Millimeter", Symbol = "mm3", FactorToBase = 0.000000001 },
            new UnitInfo<VolumeUnit> { Unit = VolumeUnit.CubicKilometer, DisplayName = "Cubic Kilometer", Symbol = "km3", FactorToBase = 1000000000 }
        };

        public static readonly VolumeUnit DefaultFromUnit = VolumeUnit.CubicMeter;
        public static readonly VolumeUnit DefaultToUnit = VolumeUnit.CubicKilometer;

        public static UnitInfo<VolumeUnit> GetUnitInfo(VolumeUnit unit)
        {
            return Units.FirstOrDefault(u => u.Unit == unit);
        }
    }
}
