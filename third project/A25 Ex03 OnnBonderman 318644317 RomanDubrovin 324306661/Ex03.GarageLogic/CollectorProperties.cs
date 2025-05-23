using System.Collections.Generic;
using Ex03.GarageLogic.Enum;

namespace Ex03.GarageLogic
{
    public class CollectorProperties
    {
        public List<Tire> m_ListOfTires;
        public Vehicle m_vehicle;
        public VehicleStatusEnum.eVehicleStatus m_VehicleStatus;
        public EngineTypeEnum.eEngineType m_EngineType;   
        public CarColorEnum.eCarColor m_CarColor;
        public CarDoorsEnum.eCarDoors m_NumberOfDoors;
        public VehicleTypeEnum.eVehicleType m_VehicleType;
        public MotorcycleLicenseEnum.eLicenseType m_LicenseType;
        public string m_LicenseNumber;
        public string m_ModelVehicleName;
        public string m_TireManufacturerName;
        public string m_OwnerFullName;
        public string m_OwnerPhoneNumber;
        public float m_CurrentTireAirPressure;
        public float m_MaxTireAirPressureFromManufacturer;
        public float m_CurrentEnergyLeft;
        public float m_MaxEnergyAmount;
        public float m_CargoVolume;
        public int m_EngineVolume;
        public bool m_IsRefrigeratedTransferOfMaterials;
    }
}
