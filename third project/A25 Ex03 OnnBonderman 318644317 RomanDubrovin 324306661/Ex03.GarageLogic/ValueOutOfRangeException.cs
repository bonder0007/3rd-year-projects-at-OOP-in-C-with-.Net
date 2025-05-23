using System;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private readonly float r_MaxValue;
        private readonly float r_MinValue;

        public ValueOutOfRangeException(float i_MaxValue, float i_MinValue)
          : base(string.Format("An error occurred. Please use a value between {0} and {1}", i_MinValue, i_MaxValue))
        {
            r_MaxValue = i_MaxValue;
            r_MinValue = i_MinValue;
        }
    }
}
