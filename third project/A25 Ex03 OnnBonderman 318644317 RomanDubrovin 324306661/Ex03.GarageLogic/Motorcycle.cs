using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic.Enum;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        private int m_EngineVolume;
        private MotorcycleLicenseEnum.eLicenseType m_LicenseType;

        public Motorcycle()
        {

        }

        public int EngineVolume
        {
            get { return m_EngineVolume; }
            set { m_EngineVolume = value; }
        }

        public MotorcycleLicenseEnum.eLicenseType LicenseType
        {
            get { return m_LicenseType; }
            set { m_LicenseType = value; }
        }

        public override void InitializeTires()
        {
            base.m_Tires = new List<Tire>();
            for (int index = 0; index < 2; index++)
            {
                base.m_Tires.Add(new Tire());
                m_Tires[index].MaxTireAirPressureFromManufacturer = 34;
            }
        }

        public override StringBuilder GetInfoVehicle()
        {
            StringBuilder stringBuilderEzer = new StringBuilder();
            stringBuilderEzer.AppendFormat("The license type of the motorcycle is: {0}", LicenseType);
            stringBuilderEzer.Append(Environment.NewLine);
            stringBuilderEzer.AppendFormat("The volume of the motorcycle in CC is: {0}", EngineVolume);
            stringBuilderEzer.Append(Environment.NewLine);
            stringBuilderEzer.Append(base.GetInfoVehicle());

            return stringBuilderEzer;    
        }
    }
}
