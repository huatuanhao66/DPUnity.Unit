using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DPUnity.Unit.Models
{
    public enum AngleUnit
    {
        Degree,
        Radian,
        Minute,
        Second
    }

    public static class AngleUnits
    {
        public static readonly List<UnitInfo<AngleUnit>> Units = new List<UnitInfo<AngleUnit>>
        {   
            new UnitInfo<AngleUnit> { Unit = AngleUnit.Degree, DisplayName = "Degree", Symbol = "", FactorToBase = 1 },
            new UnitInfo<AngleUnit> { Unit = AngleUnit.Radian, DisplayName = "Radian", Symbol = "", FactorToBase = Math.PI / 180 },
            new UnitInfo<AngleUnit> { Unit = AngleUnit.Minute, DisplayName = "Minute", Symbol = "'", FactorToBase = 1.0 / 60 },
            new UnitInfo<AngleUnit> { Unit = AngleUnit.Second, DisplayName = "Second", Symbol = "\"", FactorToBase = 1.0 / 3600 }
        };

        public static readonly AngleUnit DefaultFromUnit = AngleUnit.Degree;
        public static readonly AngleUnit DefaultToUnit = AngleUnit.Radian;

        public static UnitInfo<AngleUnit> GetUnitInfo(AngleUnit unit)
        {
            return Units.FirstOrDefault(u => u.Unit == unit);
        }

        public static string FormatDegree(double degree, int round = 0)
        {
            int deg = (int)degree;
            double fractional = Math.Abs(degree - deg);

            int minutes = (int)(fractional * 60);
            double seconds = (fractional * 60 - minutes) * 60;

            string format = "0";
            if (round > 0)
            {
                format += "." + new string('#', round);
            }

            return $"{deg}° {minutes}' {seconds.ToString(format)}\"";
        }
    }
}
