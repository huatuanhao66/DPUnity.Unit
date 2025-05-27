using DPUnity.Unit.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DPUnity.Unit.Interfaces
{
    public interface IUnitConverter
    {
        double Convert<TUnit>(TUnit fromUnit, TUnit toUnit, double value) where TUnit : Enum;
        double Convert<TUnit>(double value) where TUnit : Enum;
    }
}
