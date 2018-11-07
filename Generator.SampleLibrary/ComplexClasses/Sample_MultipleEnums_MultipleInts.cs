using System.Collections.Generic;

namespace Generator.SampleLibrary.ComplexClasses
{
    public class Sample_MultipleEnums_MultipleInts
    {
        public MyComplexEnum MyEnumProperty { get; set; }

        public int MyIntProperty { get; set; }

        public SomeOtherEnum? OtherEnumProperty { get; set; }

        public List<int?> MyIntListProperty { get; set; }
    }
}
