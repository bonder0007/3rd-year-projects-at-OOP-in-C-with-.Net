using System;
using Ex03.GarageLogic.Enum;

namespace Ex03.GarageLogic
{
    public class FactoryVehicle
    {
        public static Vehicle CreateVehicle(VehicleTypeEnum.eVehicleType i_VehicleType)
        {
            Vehicle vehicle;
            switch (i_VehicleType)
            {
                case VehicleTypeEnum.eVehicleType.Car:
                    vehicle = new Car();
                    break;

                case VehicleTypeEnum.eVehicleType.Motorcycle:
                    vehicle = new Motorcycle();
                    break;

                case VehicleTypeEnum.eVehicleType.Truck:
                    vehicle = new Truck();
                    break;

                default:
                    throw new ArgumentException("Invalid vehicle type");
            }

            return vehicle;
        }
    }
}
