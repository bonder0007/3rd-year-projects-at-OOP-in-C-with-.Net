using System;
using System.Text;
using Ex03.GarageLogic.Enum;

namespace Ex03.GarageLogic
{
    public class ElectricEngine : Engine
    {
        public override void AddSourceOfEnergy(float i_AddEnergyAmount, FuelTypeEnum.eFuelType? i_FuelType = null)
        { 
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
            stringBuilderEzer.AppendFormat("Current electricity left is: {0}",CurrentEnergySourceLeft);
            stringBuilderEzer.Append(Environment.NewLine);

            return stringBuilderEzer;
        }
    }
}
