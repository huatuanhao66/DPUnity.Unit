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
        private readonly Dictionary<Type, (Enum from, Enum to)> _defaultUnits = new Dictionary<Type, (Enum from, Enum to)>();

        public UnitConverter(IEnumerable<(Enum from, Enum to)> defaultUnits)
        {
            defaultUnits = defaultUnits ?? throw new ArgumentNullException(nameof(defaultUnits));

            foreach (var (from, to) in defaultUnits)
            {
                if (from == null || to == null)
                    continue;

                var fromType = from.GetType();
                var toType = to.GetType();

                if (fromType != toType)
                    throw new InvalidOperationException("Mismatch between 'from' and 'to' enum types.");

                if (_defaultUnits.ContainsKey(fromType))
                    throw new InvalidOperationException($"Duplicate unit type: {fromType.Name}");

                _defaultUnits[fromType] = (from, to);
            }
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

        public TValue Convert<TUnit, TValue>(TUnit fromUnit, TUnit toUnit, TValue value) where TUnit : Enum
        {
            try
            {
                var fromInfo = UnitHelper.GetUnitInfo(fromUnit);
                var toInfo = UnitHelper.GetUnitInfo(toUnit);
                if (fromInfo == null || toInfo == null)
                    throw new ArgumentException("Invalid unit.");

                TValue result = UnitHelper.ConvertValue<TUnit, TValue>(value, fromInfo, toInfo);

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[UnitConverter] Error: {ex.Message}");
                throw new Exception($"Error: {ex.Message}");
            }
        }

        public TValue Convert<TUnit, TValue>(TValue value) where TUnit : Enum
        {
            try
            {
                TUnit defaultFromUnit, defaultToUnit;

                //Check list of default units
                if (_defaultUnits.TryGetValue(typeof(TUnit), out var match))
                {
                    defaultFromUnit = match.from is TUnit from ? from : UnitHelper.GetDefaultFromUnit<TUnit>();
                    defaultToUnit = match.to is TUnit to ? to : UnitHelper.GetDefaultToUnit<TUnit>();
                }
                else //check default from and default to
                {
                    defaultFromUnit = _defaultFromUnit is TUnit from ? from : UnitHelper.GetDefaultFromUnit<TUnit>();
                    defaultToUnit = _defaultToUnit is TUnit to ? to : UnitHelper.GetDefaultToUnit<TUnit>();
                }

                var toInfo = UnitHelper.GetUnitInfo(defaultToUnit, defaultToUnit);
                var fromInfo = UnitHelper.GetUnitInfo(defaultFromUnit, defaultFromUnit);
                if (toInfo == null || fromInfo == null)
                    throw new ArgumentException("Invalid unit.");

                TValue result = UnitHelper.ConvertValue<TUnit, TValue>(value, fromInfo, toInfo);

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[UnitConverter] Error: {ex.Message}");
                throw new Exception($"Error: {ex.Message}");
            }
        }

        public object Convert<TUnit>(object value) where TUnit : Enum
        {
            try
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                else if (value is double doubleValue)
                    return (object)Convert<TUnit>(doubleValue);
                else if (value is decimal decValue)
                    return (object)Convert<TUnit>(decValue);
                else if (value is float floatValue)
                    return (object)Convert<TUnit>(floatValue);
                else if (value is int intValue)
                {
                    double result = Convert<TUnit>((double)intValue);
                    if (result % 1 == 0)
                        return (object)(int)result;
                    else
                        return (object)result;
                }

                return value;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[UnitConverter] Error: {ex.Message}");
                throw new Exception($"Error: {ex.Message}");
            }
        }

        public double Convert<TUnit>(double value) where TUnit : Enum
        {
            try
            {
                return Convert<TUnit, double> (value); ;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[UnitConverter] Error: {ex.Message}");
                throw new Exception($"Error: {ex.Message}");
            }
        }

        public decimal Convert<TUnit>(decimal value) where TUnit : Enum
        {
            try
            {
                return Convert<TUnit, decimal>(value);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[UnitConverter] Error: {ex.Message}");
                throw new Exception($"Error: {ex.Message}");
            }
        }

        public int Convert<TUnit>(int value) where TUnit : Enum
        {
            try
            {
                return Convert<TUnit, int>(value);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[UnitConverter] Error: {ex.Message}");
                throw new Exception($"Error: {ex.Message}");
            }
        }

        public float Convert<TUnit>(float value) where TUnit : Enum
        {
            try
            {
                return Convert<TUnit, float>(value);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[UnitConverter] Error: {ex.Message}");
                throw new Exception($"Error: {ex.Message}");
            }
        }
    }
}
