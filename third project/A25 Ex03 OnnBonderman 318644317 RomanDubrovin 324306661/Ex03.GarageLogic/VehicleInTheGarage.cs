using System;
using System.Text;
using Ex03.GarageLogic.Enum;

namespace Ex03.GarageLogic
{
    public class VehicleInTheGarage
    {
        private string m_OwnerName;
        private string m_OwnerPhoneNumber;
        private Vehicle m_Vehicle;
        private VehicleStatusEnum.eVehicleStatus m_VehicleStatus;
        private VehicleTypeEnum.eVehicleType m_VehicleType;

        public VehicleInTheGarage()
        {
            
        }

        public VehicleInTheGarage(Vehicle i_vehicle, string i_OwnerName, 
                                 string i_OwnerPhoneNumber, VehicleStatusEnum.eVehicleStatus i_VehicleStatus, 
                                 VehicleTypeEnum.eVehicleType i_VehicleType)
        {
            this.m_Vehicle = i_vehicle;
            this.m_OwnerName = i_OwnerName;
            this.m_OwnerPhoneNumber = i_OwnerPhoneNumber;
            this.m_VehicleStatus = i_VehicleStatus;
            this.m_VehicleType = i_VehicleType;
        }

        public string OwnerName
        {
            get { return m_OwnerName; }
            set { m_OwnerName = value; }
        }

        public string OwnerPhoneNumber
        {
            get { return m_OwnerPhoneNumber; }
            set { m_OwnerPhoneNumber = value; }
        }

        public Vehicle Vehicle
        {
            get { return m_Vehicle; }
            set { m_Vehicle = value; }
        }

        public VehicleStatusEnum.eVehicleStatus VehicleStatus
        {
            get { return m_VehicleStatus; }
            set { m_VehicleStatus = value; }
        }

        public VehicleTypeEnum.eVehicleType VehicleType
        {
            get { return m_VehicleType; }
            set { m_VehicleType = value; }
        }

        public bool IsFuelEngine()
        {
            return m_Vehicle.Engine is FuelEngine;
        }

        // $G$ DSN-006 (-5) Bad Encapsulation. This method and others in your project should have an internal access modifier.
        public StringBuilder GetInfoOfVehicleInTheGarage()
        {
            StringBuilder stringBuilderEzer = new StringBuilder();
            stringBuilderEzer.AppendFormat("The name of the owner is: {0}", OwnerName);
            stringBuilderEzer.Append(Environment.NewLine);
            stringBuilderEzer.AppendFormat("The Phone number of the owner is: {0}", OwnerPhoneNumber);
            stringBuilderEzer.Append(Environment.NewLine);
            stringBuilderEzer.AppendFormat("The Vehicle status is: {0}", VehicleStatus);
            stringBuilderEzer.Append(Environment.NewLine);
            stringBuilderEzer.AppendFormat("The Vehicle type is: {0}", VehicleType);
            stringBuilderEzer.Append(Environment.NewLine);

            return stringBuilderEzer;
        }
    }
}

