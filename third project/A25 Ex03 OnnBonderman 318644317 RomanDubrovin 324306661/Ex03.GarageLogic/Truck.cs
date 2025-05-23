using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private bool m_IsRefrigeratedTransferOfMaterials;
        private float m_CargoVolume;

        public Truck()
        {

        }

        public bool IsRefrigeratedTransferOfMaterials
        {
            get { return m_IsRefrigeratedTransferOfMaterials; }
            set { m_IsRefrigeratedTransferOfMaterials = value; }
        }

        public float CargoVolume
        {
            get { return m_CargoVolume; }
            set { m_CargoVolume = value; }
        }

        public override void InitializeTires()
        {
            base.m_Tires = new List<Tire>();
            for (int index = 0; index < 14; index++)
            {
                base.m_Tires.Add(new Tire());
                m_Tires[index].MaxTireAirPressureFromManufacturer = 29;
            }
        }

        public override StringBuilder GetInfoVehicle()
        {
            StringBuilder stringBuilderEzer = new StringBuilder();
            stringBuilderEzer.AppendFormat("Is the truck transfer items in cooling is : {0}", IsRefrigeratedTransferOfMaterials);
            stringBuilderEzer.Append(Environment.NewLine);
            stringBuilderEzer.AppendFormat("The cargo pf the truck is: {0}", m_CargoVolume);
            stringBuilderEzer.Append(Environment.NewLine);
            stringBuilderEzer.Append(base.GetInfoVehicle());

            return stringBuilderEzer;
        }
    }
}
