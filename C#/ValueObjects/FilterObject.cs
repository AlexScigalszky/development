using Example.ValueObjects.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Example.ValueObjects
{
    public class FilterObject : ValueObject
    {
        public const string EQUALS_OPERATOR = "=";
        public const string LIKE_OPERATOR = "LIKE";

        public string Values { get; set; }
        public string Separator { get; internal set; }
        public string Key { get; internal set; }
        public long ValuesAsLong
        {
            get
            {
                return Convert.ToInt64(Values);
            }
        }
        public long[] ValuesAsArrayLong
        {
            get
            {
                return Values
                    .Split(",")
                    .Select(x => Convert.ToInt64(x))
                    .ToArray();
            }
        }
        public bool IsEqual
        {
            get
            {
                return EQUALS_OPERATOR == Separator;
            }
        }
        public bool IsLike
        {
            get
            {
                return LIKE_OPERATOR == Separator;
            }
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            return new object[]
            {
                Values, Separator, Key
            };
        }
    }
}
