using DPUnity.Unit.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DPUnity.Unit.Interfaces
{
    public interface IUnitConverter
    {
        TValue Convert<TUnit, TValue>(TUnit fromUnit, TUnit toUnit, TValue value) where TUnit : Enum;
        TValue Convert<TUnit, TValue>(TValue value) where TUnit : Enum;
        object Convert<TUnit>(object value) where TUnit : Enum;
        double Convert<TUnit>(double value) where TUnit : Enum;
        decimal Convert<TUnit>(decimal value) where TUnit : Enum;
        float Convert<TUnit>(float value) where TUnit : Enum;
        int Convert<TUnit>(int value) where TUnit : Enum;
    }
}
