using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic.Enum;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private CarDoorsEnum.eCarDoors m_NumberOfDoors;
        private CarColorEnum.eCarColor m_CarColor;

        public Car()  
        {
            
        }

        public CarDoorsEnum.eCarDoors NumberOfDoors
        {
            set { m_NumberOfDoors = value; }
            get { return m_NumberOfDoors; }
        }

        public CarColorEnum.eCarColor ColorType
        {
            set { m_CarColor = value; }
            get { return m_CarColor; }
        }

        public override void InitializeTires()
        {
            base.m_Tires = new List<Tire>();
            for (int index = 0; index < 5; index++)
            {
                base.m_Tires.Add(new Tire());
                m_Tires[index].MaxTireAirPressureFromManufacturer = 32;
            }            
        }

        public override StringBuilder GetInfoVehicle()
        {    
            StringBuilder stringBuilderEzer = new StringBuilder();
            stringBuilderEzer.AppendFormat("The Number of doors is: {0}", NumberOfDoors);
            stringBuilderEzer.Append(Environment.NewLine);
            stringBuilderEzer.AppendFormat("The color of the car is: {0}", m_CarColor);
            stringBuilderEzer.Append(Environment.NewLine);
            stringBuilderEzer.Append(base.GetInfoVehicle());

            return stringBuilderEzer;
        }
    }
}
