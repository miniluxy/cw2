
using System;
using System.Collections.Generic;
using System.Text;

namespace cw2
{
    class MojComparator : IEqualityComparer<Student>
    {
        public bool Equals(Student x, Student y)
        {

            return StringComparer.InvariantCultureIgnoreCase.Equals(
                $"{x.dadName} {x.lastName} {x.indexNumber}", $"{y.dadName} {y.lastName} {y.indexNumber}");
        }

        public int GetHashCode(Student obj)
        {
            return StringComparer.InvariantCultureIgnoreCase.GetHashCode(
                $"{obj.dadName} {obj.lastName} {obj.indexNumber}");
        }
    }
}