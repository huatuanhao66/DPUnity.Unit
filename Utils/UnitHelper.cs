using DPUnity.Unit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DPUnity.Unit.Services;
using System.Collections.ObjectModel;

namespace DPUnity.Unit.Utils
{
    public static class UnitHelper
    {
        private static readonly Dictionary<Type, object> UnitCollections = new Dictionary<Type, object>
        {
            { typeof(LengthUnit), new UnitCollection<LengthUnit>(LengthUnits.Units, LengthUnits.DefaultFromUnit, LengthUnits.DefaultToUnit) },
            { typeof(AreaUnit), new UnitCollection<AreaUnit>(AreaUnits.Units, AreaUnits.DefaultFromUnit, AreaUnits.DefaultToUnit) },
            { typeof(AngleUnit), new UnitCollection<AngleUnit>(AngleUnits.Units, AngleUnits.DefaultFromUnit, AngleUnits.DefaultToUnit) },
            { typeof(VolumeUnit), new UnitCollection<VolumeUnit>(VolumeUnits.Units, VolumeUnits.DefaultFromUnit, VolumeUnits.DefaultToUnit) }
        };

        public static UnitInfo<TUnit> GetUnitInfo<TUnit>(TUnit unit) where TUnit : Enum
        {
            Type unitType = typeof(TUnit);

            if (!UnitCollections.ContainsKey(unitType))
                throw new ArgumentException($"No unit collection found for type {unitType.Name}");

            var collecton = UnitCollections[unitType] as UnitCollection<TUnit>;
            if (!UnitCollections.ContainsKey(unitType))
                throw new ArgumentException($"No unit collection found for type {unitType.Name}");

            var units = collecton.Units;
            if (units == null)
                throw new InvalidOperationException($"Invalid collection info for type {unitType.Name}");

            return units.FirstOrDefault(u => u.Unit.Equals(unit));
        }

        public static UnitInfo<TUnit> GetUnitInfo<TUnit>(TUnit unit, TUnit defaultUnit) where TUnit : Enum
        {
            Type unitType = typeof(TUnit);
            if (!UnitCollections.ContainsKey(unitType))
                throw new ArgumentException($"No unit collection found for type {unitType.Name}");

            var collecton = UnitCollections[unitType] as UnitCollection<TUnit>;
            if (!UnitCollections.ContainsKey(unitType))
                throw new ArgumentException($"No unit collection found for type {unitType.Name}");

            var units = collecton.Units;
            if (units == null)
                throw new InvalidOperationException($"Invalid collection info for type {unitType.Name}");

            var defaultUnitInfo = units.FirstOrDefault(u => u.Unit.Equals(defaultUnit));
            if (defaultUnitInfo == null)
                throw new InvalidOperationException("Default unit not found in unit list.");

            double baseFactor = defaultUnitInfo.FactorToBase;

            foreach (var unitInfo in units)
            {
                unitInfo.FactorToBase = unitInfo.FactorToBase / baseFactor;
            }

            return units.FirstOrDefault(u => u.Unit.Equals(unit));
        }

        public static TUnit GetDefaultFromUnit<TUnit> () where TUnit : Enum
        {
            Type unitType = typeof(TUnit);

            if (!UnitCollections.ContainsKey(unitType))
                throw new ArgumentException($"No unit collection found for type {unitType.Name}");

            var collection = UnitCollections[unitType] as UnitCollection<TUnit>;
            if (collection == null)
                throw new InvalidOperationException($"Invalid collection info for type {unitType.Name}");

            return collection.DefaultFromUnit;
        }

        public static TUnit GetDefaultToUnit<TUnit>() where TUnit : Enum
        {
            Type unitType = typeof(TUnit);

            if (!UnitCollections.ContainsKey(unitType))
                throw new ArgumentException($"No unit collection found for type {unitType.Name}");

            var collection = UnitCollections[unitType] as UnitCollection<TUnit>;
            if (collection == null)
                throw new InvalidOperationException($"Invalid collection info for type {unitType.Name}");

            return collection.DefaultToUnit;
        }

        public static TValue ConvertValue<TUnit, TValue>(object value, UnitInfo<TUnit> fromInfo, UnitInfo<TUnit> toInfo)
        {
            var type = typeof(TValue);
            TValue result;

            if (type == typeof(decimal))
            {
                decimal decValue = (decimal)(object)value;
                decimal valueInBase = decValue * (decimal)fromInfo.FactorToBase;
                result = (TValue)(object)(valueInBase / (decimal)toInfo.FactorToBase);
            }
            else if (type == typeof(int))
            {
                int intValue = (int)(object)value;
                int valueInBase = intValue * (int)fromInfo.FactorToBase;
                result = (TValue)(object)(valueInBase / (int)toInfo.FactorToBase);
            }
            else if (type == typeof(float))
            {
                float floatValue = (float)(object)value;
                float valueInBase = floatValue * (float)fromInfo.FactorToBase;
                result = (TValue)(object)(valueInBase / (float)toInfo.FactorToBase);
            }
            else
            {
                double doubleValue = System.Convert.ToDouble(System.Convert.ChangeType(value, type));
                double valueInBase = doubleValue * fromInfo.FactorToBase;
                result = (TValue)(object)(valueInBase / toInfo.FactorToBase);
            }

            return result;
        }
    }
}
