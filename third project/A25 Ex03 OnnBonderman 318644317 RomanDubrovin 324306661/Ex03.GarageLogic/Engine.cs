using System.Text;
using Ex03.GarageLogic.Enum;

namespace Ex03.GarageLogic
{
    public abstract class Engine
    {
        protected float m_CurrentEnergySourceLeft;
        protected float m_MaxAmountEnergySource;

        public Engine()
        {

        }
    
        public float CurrentEnergySourceLeft
        {
            get { return m_CurrentEnergySourceLeft; }
            set { m_CurrentEnergySourceLeft = value; }
        }

        public float MaxAmountEnergySource
        {
            get { return m_MaxAmountEnergySource; }
            set { m_MaxAmountEnergySource = value; }
        }

        public abstract StringBuilder InfoEngine();

        public abstract void AddSourceOfEnergy(float i_amountToAdd, FuelTypeEnum.eFuelType? i_FuelType);
    }
}
