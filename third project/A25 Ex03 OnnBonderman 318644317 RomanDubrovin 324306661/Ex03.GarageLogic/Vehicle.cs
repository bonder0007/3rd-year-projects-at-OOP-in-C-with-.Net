using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private Engine m_Engine;
        protected List<Tire> m_Tires;
        protected string m_ModelName;
        protected string m_LicenseNumber;
        
        public Vehicle()
        {

        }

        public Vehicle(string i_ModelName, string i_LicenseNumber, List<Tire> i_Tires)
        {
            m_ModelName = i_ModelName;
            m_LicenseNumber = i_LicenseNumber;
            m_Tires = i_Tires;
        }

        public string ModelName
        {
            get { return m_ModelName; }
            set { m_ModelName = value; }
        }

        public string LicenseNumber
        {
            get { return m_LicenseNumber; }
            set { m_LicenseNumber = value; }
        }

        public List<Tire> Tires
        {
            get { return m_Tires; }
            set { m_Tires = value; }
        }

        public Engine Engine
        {
            get { return m_Engine; }
            set { m_Engine = value; }
        }

        public abstract void InitializeTires();

        public virtual StringBuilder GetInfoVehicle()
        {
            StringBuilder stringBuilderEzer = new StringBuilder();
            stringBuilderEzer.Append("The model of the vehicle is: ").Append(ModelName.ToString());
            stringBuilderEzer.Append(Environment.NewLine);
            stringBuilderEzer.Append("The license number of the vehicle is: ").Append(LicenseNumber.ToString());
            stringBuilderEzer.Append(Environment.NewLine);
            for (int index = 0; index < m_Tires.Count; index++)
            {
                string ezerManufactur = m_Tires[index].ManufacturerName;
                string ezerAirPressure = m_Tires[index].CurrentTireAirPressure.ToString();
                stringBuilderEzer.AppendFormat("The {0} tire fill with air pressure {1} and manufactur {2}", index + 1, ezerAirPressure, ezerManufactur);
                stringBuilderEzer.Append(Environment.NewLine);
            }

            return stringBuilderEzer;
        }

        public void SetAllTiresAirPressure(float i_AirPressure)
        {
            for (int index = 0; index < m_Tires.Count; index++)
            {
                m_Tires[index].CurrentTireAirPressure = i_AirPressure;
                if (m_Tires[index].CurrentTireAirPressure > (m_Tires[index].MaxTireAirPressureFromManufacturer))
                {
                    throw new ValueOutOfRangeException(m_Tires[index].MaxTireAirPressureFromManufacturer, 0);
                }
            }
        }

        public void SetTireAirPressure(int i_TireIndex, float i_AirPressure)
        {
            m_Tires[i_TireIndex].CurrentTireAirPressure = i_AirPressure;
            if (m_Tires[i_TireIndex].CurrentTireAirPressure > m_Tires[i_TireIndex].MaxTireAirPressureFromManufacturer)
            {
                throw new ValueOutOfRangeException(m_Tires[i_TireIndex].MaxTireAirPressureFromManufacturer, 0);
            }
        }

        public void SetAllTireManufacturer(string i_ManufacturerName)
        {
            for (int index = 0; index < m_Tires.Count; index++)
            {
                m_Tires[index].ManufacturerName = i_ManufacturerName;
            }
        }

        public void SetTireManufacturer(int i_TireIndex, string i_ManufacturerName)
        {
            m_Tires[i_TireIndex].ManufacturerName = i_ManufacturerName;
        }

        public void UpdateTiresToMax()
        {
            for (int index = 0; index < m_Tires.Count; index++)
            {
                m_Tires[index].CurrentTireAirPressure = m_Tires[index].MaxTireAirPressureFromManufacturer;
            }
        }  
    }
}
