using System;
using System.Text;
using Ex03.GarageLogic.Enum;

namespace Ex03.GarageLogic
{
    public class FuelEngine : Engine
    {
        // $G$ DSN-999 (-3) The "fuel type" field should be readonly member of class FuelEnergyProvider.
        private FuelTypeEnum.eFuelType m_FuelType;

        public FuelEngine()
        {
            this.m_MaxAmountEnergySource = 52f;
        }

        public FuelTypeEnum.eFuelType FuelType
        {
            get { return m_FuelType; }
            set { m_FuelType = value; }
        }

        public override void AddSourceOfEnergy(float i_AddEnergyAmount, FuelTypeEnum.eFuelType? i_FuelType = null)
        {
            if (m_FuelType != i_FuelType)
            {
                throw new ArgumentException(string.Format("Incorrect fuel! Please choose the correct fuel: {0}", i_FuelType));
            }

            if (m_CurrentEnergySourceLeft + i_AddEnergyAmount > m_MaxAmountEnergySource || i_AddEnergyAmount < 0)
            {
                throw new ValueOutOfRangeException(m_MaxAmountEnergySource, 0);
            }
            else
            {
                m_CurrentEnergySourceLeft += i_AddEnergyAmount;
            }
        }     

        public override StringBuilder InfoEngine()
        {
            StringBuilder stringBuilderEzer = new StringBuilder();
            stringBuilderEzer.AppendFormat("Fuel type is {0}", FuelType);
            stringBuilderEzer.Append(Environment.NewLine);
            stringBuilderEzer.AppendFormat("Current Fuel amount left is: {0}", CurrentEnergySourceLeft);
            stringBuilderEzer.Append(Environment.NewLine);

            return stringBuilderEzer;
        }
    }
}
