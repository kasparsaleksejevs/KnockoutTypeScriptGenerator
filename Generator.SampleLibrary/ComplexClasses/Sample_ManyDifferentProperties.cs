using System;
using System.Collections.Generic;

namespace Generator.SampleLibrary.ComplexClasses
{
    public class Sample_ManyDifferentProperties
    {
        public int IntValue { get; set; }

        public int[] IntArr { get; set; }

        public List<int> IntList { get; set; }

        public List<int?> NullableIntList { get; set; }

        public double DoubleValue { get; set; }

        public decimal DecimalValue { get; set; }

        public decimal? NullableDecimalValue { get; set; }

        public string StringValue { get; set; }

        public bool BoolValue { get; set; }

        public DateTime DateValue { get; set; }

        public MyComplexEnum EnumValue { get; set; }
    }
}
