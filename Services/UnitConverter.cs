using DPUnity.Unit.Interfaces;
using DPUnity.Unit.Models;
using DPUnity.Unit.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace DPUnity.Unit.Services
{
    public class UnitConverter : IUnitConverter
    {
        private readonly object _defaultFromUnit = null;
        private readonly object _defaultToUnit = null;
        private readonly List<(Enum from, Enum to)> _defaultUnits = new List<(Enum from, Enum to)>();

        public UnitConverter(List<(Enum from, Enum to)> defaultUnits)
        {
            defaultUnits = defaultUnits ?? throw new ArgumentNullException(nameof(defaultUnits));

            var enumTypeCounts = new Dictionary<Type, int>();

            foreach (var defaultUnit in defaultUnits)
            {
                if (defaultUnit.from != null && defaultUnit.to != null)
                {
                    if (defaultUnit.from.GetType() != defaultUnit.from.GetType())
                        throw new InvalidOperationException("Unit types mismatch.");

                    var enumType = defaultUnit.from.GetType();
                    if (!enumTypeCounts.ContainsKey(enumType))
                        enumTypeCounts[enumType] = 1;
                    else
                        enumTypeCounts[enumType]++;

                }
            }

            var duplicates = enumTypeCounts.Where(kvp => kvp.Value > 1).ToList();
            if (duplicates.Any())
            {
                throw new InvalidOperationException($"Duplicate unit types found in list");
            }

            _defaultUnits = defaultUnits.ToList();
        }

        public UnitConverter((Enum from, Enum to) defaultUnit)
        {
            _defaultFromUnit = defaultUnit.from;
            _defaultToUnit = defaultUnit.to;

            if (_defaultFromUnit != null && _defaultToUnit != null)
            {
                if (_defaultToUnit.GetType() != _defaultFromUnit.GetType())
                    throw new InvalidOperationException("Unit types mismatch.");
            }
        }

        public UnitConverter()
        {
        }

        public double Convert<TUnit>(TUnit fromUnit, TUnit toUnit, double value) where TUnit : Enum
        {
            try
            {
                var fromInfo = UnitHelper.GetUnitInfo(fromUnit);
                var toInfo = UnitHelper.GetUnitInfo(toUnit);
                if (fromInfo == null || toInfo == null)
                    throw new ArgumentException("Invalid unit.");

                double valueInBase = value * fromInfo.FactorToBase;
                return valueInBase / toInfo.FactorToBase;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[UnitConverter] Error: {ex.Message}");
                throw;
            }
        }

        public double Convert<TUnit>(double value) where TUnit : Enum
        {
            try
            {
                TUnit defaultFromUnit, defaultToUnit;

                if (_defaultUnits.Any()) //Check list Unit Default
                {
                    var match = _defaultUnits.FirstOrDefault(p => p.from.GetType() == typeof(TUnit));
                    if (match.from == null)
                        defaultFromUnit = UnitHelper.GetDefaultFromUnit<TUnit>();
                    else
                        defaultFromUnit = (TUnit)match.from;

                    match = _defaultUnits.FirstOrDefault(p => p.to.GetType() == typeof(TUnit));
                    if (match.to == null)
                        defaultToUnit = UnitHelper.GetDefaultToUnit<TUnit>();
                    else
                        defaultToUnit = (TUnit)match.to;
                }
                else //check default from and default to
                {
                    if (_defaultFromUnit == null || !(_defaultFromUnit is TUnit))
                        defaultFromUnit = UnitHelper.GetDefaultFromUnit<TUnit>();
                    else
                        defaultFromUnit = (TUnit)_defaultFromUnit;

                    if (_defaultToUnit == null || !(_defaultFromUnit is TUnit))
                        defaultToUnit = UnitHelper.GetDefaultToUnit<TUnit>();
                    else
                        defaultToUnit = (TUnit)_defaultToUnit;
                }

                var toInfo = UnitHelper.GetUnitInfo(defaultToUnit, defaultToUnit);
                var defaultInfo = UnitHelper.GetUnitInfo(defaultFromUnit, defaultFromUnit);
                if (toInfo == null)
                    throw new ArgumentException("Invalid unit.");

                double valueInBase = value * defaultInfo.FactorToBase;

                return valueInBase / toInfo.FactorToBase;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[UnitConverter] Error: {ex.Message}");
                throw;
            }
        }
    }
}
