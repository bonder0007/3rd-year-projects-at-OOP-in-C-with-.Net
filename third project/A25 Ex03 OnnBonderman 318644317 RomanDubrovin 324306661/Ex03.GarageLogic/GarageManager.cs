using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic.Enum;

namespace Ex03.GarageLogic
{
    public class GarageManager
    {
        private Dictionary<string, VehicleInTheGarage> m_VehicleInTheGarage;

        public GarageManager()
        {
            m_VehicleInTheGarage = new Dictionary<string, VehicleInTheGarage>();
        }

        public Dictionary<string, VehicleInTheGarage> VehicleInGarageDictionary
        {
            set { m_VehicleInTheGarage = value; }
            get { return m_VehicleInTheGarage; }
        }

        public StringBuilder GetInfoOnVehicle(string i_LicenseNumber)
        {
            StringBuilder vehicleInTheGarageStringBuilder = new StringBuilder();
            if (m_VehicleInTheGarage.TryGetValue(i_LicenseNumber, out VehicleInTheGarage vehicleInTheGarageEzer))
            {
                vehicleInTheGarageStringBuilder.Append(vehicleInTheGarageEzer.GetInfoOfVehicleInTheGarage());
                vehicleInTheGarageStringBuilder.Append(vehicleInTheGarageEzer.Vehicle.GetInfoVehicle());
                vehicleInTheGarageStringBuilder.Append(vehicleInTheGarageEzer.Vehicle.Engine.InfoEngine());
            }

            return vehicleInTheGarageStringBuilder;
        }

        public bool IsVehicleExistsInGarage(string i_LicenseNumber)
        {
            bool isVehicleExists = false;
            // $G$ CSS-027 (-1) Missing blank line.
            if (m_VehicleInTheGarage.ContainsKey(i_LicenseNumber))
            {
                isVehicleExists = true;
            }

            return isVehicleExists;
        }

        // $G$ CSS-000 (-5) The variable names are not meaningful and understandable. Why you add the word "Ezer" in Hebrew? the entire project is in english. 
        public void RechargeBattery(string i_VehicleLicense, float i_FuelAmount)
        {
            if (m_VehicleInTheGarage.ContainsKey(i_VehicleLicense))
            {
                Engine engineEzer = m_VehicleInTheGarage[i_VehicleLicense].Vehicle.Engine;
                // $G$ CSS-027 (-1) Missing blank line.
                if (engineEzer is ElectricEngine)
                {
                    engineEzer.AddSourceOfEnergy(i_FuelAmount, null);
                }
            }
        }

        public void RefuelVehicle(string i_VehicleLicense, FuelTypeEnum.eFuelType i_FuelType, float i_FuelAmount)
        {
            if (m_VehicleInTheGarage.ContainsKey(i_VehicleLicense))
            {
                Engine engineEzer = m_VehicleInTheGarage[i_VehicleLicense].Vehicle.Engine;
                if (engineEzer is FuelEngine)
                {
                    engineEzer.AddSourceOfEnergy(i_FuelAmount, i_FuelType);
                }
                else
                {
                    try
                    {
                        engineEzer.AddSourceOfEnergy(i_FuelAmount, null);
                    }
                    catch (ValueOutOfRangeException ex)
                    {

                        Console.WriteLine(ex.Message);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Empty input, enter a number from the menu");
                    }
                }
            }
        }

        public void MatchVehicleTypeToFuelType(string i_VehicleLicense, float i_FuelAmount, FuelTypeEnum.eFuelType i_FuelType)
        {
            Engine engineEzer = m_VehicleInTheGarage[i_VehicleLicense].Vehicle.Engine;
            if (engineEzer is FuelEngine)
            {
                engineEzer.AddSourceOfEnergy(0, i_FuelType);
            }
        }

        public VehicleInTheGarage ReturnVehicleInGarageByLiecense(string i_LicenseNumber)
        {
            m_VehicleInTheGarage.TryGetValue(i_LicenseNumber, out VehicleInTheGarage vehicleInTheGarage);

            return vehicleInTheGarage;
        }

        public bool IsDictionaryEmpty()
        {
            return m_VehicleInTheGarage.Count == 0;
        }

        public Vehicle DefineNewVehicleToTheGarage(CollectorProperties i_CollectorPropertiesEzer)
        {
            try
            {
                Vehicle newVehicle = FactoryVehicle.CreateVehicle(i_CollectorPropertiesEzer.m_VehicleType);
                newVehicle.LicenseNumber = i_CollectorPropertiesEzer.m_LicenseNumber;
                newVehicle.ModelName = i_CollectorPropertiesEzer.m_ModelVehicleName;
                newVehicle.Engine = createEngineByUserType(i_CollectorPropertiesEzer.m_VehicleType,
                                                           i_CollectorPropertiesEzer.m_EngineType,
                                                           i_CollectorPropertiesEzer.m_CurrentEnergyLeft);
                newVehicle.InitializeTires();

                return newVehicle;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public void AddToGarageFinalStage(CollectorProperties i_CollectorPropertiesEzer, Vehicle i_VehicleEzer)
        {
            switch (i_CollectorPropertiesEzer.m_VehicleType)
            {
                case VehicleTypeEnum.eVehicleType.Car:
                    Car carEzer = (Car)i_VehicleEzer;
                    carEzer.ColorType = i_CollectorPropertiesEzer.m_CarColor;
                    carEzer.NumberOfDoors = i_CollectorPropertiesEzer.m_NumberOfDoors;
                    i_VehicleEzer = carEzer;
                    break;

                case VehicleTypeEnum.eVehicleType.Motorcycle:
                    Motorcycle motorcycleEzer = (Motorcycle)i_VehicleEzer;
                    motorcycleEzer.LicenseType = i_CollectorPropertiesEzer.m_LicenseType;
                    motorcycleEzer.EngineVolume = i_CollectorPropertiesEzer.m_EngineVolume;
                    i_VehicleEzer = motorcycleEzer;
                    break;

                case VehicleTypeEnum.eVehicleType.Truck:
                    Truck truckEzer = (Truck)i_VehicleEzer;
                    truckEzer.IsRefrigeratedTransferOfMaterials = i_CollectorPropertiesEzer.m_IsRefrigeratedTransferOfMaterials;
                    truckEzer.CargoVolume = i_CollectorPropertiesEzer.m_CargoVolume;
                    i_VehicleEzer = truckEzer;
                    break;
            }

            VehicleInTheGarage vehicleInTheGarage = new VehicleInTheGarage(i_VehicleEzer, i_CollectorPropertiesEzer.m_OwnerFullName,
                                                                           i_CollectorPropertiesEzer.m_OwnerPhoneNumber,
                                                                           i_CollectorPropertiesEzer.m_VehicleStatus, i_CollectorPropertiesEzer.m_VehicleType);
            m_VehicleInTheGarage.Add(vehicleInTheGarage.Vehicle.LicenseNumber, vehicleInTheGarage);
        }

        // $G$ DSN-001 (-5) When adding a new type you'll have to change the code here too!
        private Engine createEngineByUserType(VehicleTypeEnum.eVehicleType m_VehicleType, EngineTypeEnum.eEngineType m_EngineType, float i_CurrentEnergyLeft)
        {
            Engine userEngine = null;
            switch (m_VehicleType)
            {
                case VehicleTypeEnum.eVehicleType.Car:
                    switch (m_EngineType)
                    {
                        case EngineTypeEnum.eEngineType.Fuel:
                            FuelEngine fuelEngineCarEzer = new FuelEngine();
                            fuelEngineCarEzer.MaxAmountEnergySource = fuelEngineCarEzer.MaxAmountEnergySource;
                            fuelEngineCarEzer.CurrentEnergySourceLeft = i_CurrentEnergyLeft;
                            fuelEngineCarEzer.FuelType = FuelTypeEnum.eFuelType.Octan95;
                            userEngine = fuelEngineCarEzer;
                            break;

                        case EngineTypeEnum.eEngineType.Electric:
                            ElectricEngine electricEngineCar = new ElectricEngine();
                            electricEngineCar.MaxAmountEnergySource = 5.4f;
                            electricEngineCar.CurrentEnergySourceLeft = i_CurrentEnergyLeft;
                            userEngine = electricEngineCar;
                            break;                            
                    }

                    break;

                case VehicleTypeEnum.eVehicleType.Motorcycle:
                    switch (m_EngineType)
                    {
                        case EngineTypeEnum.eEngineType.Fuel:
                            FuelEngine fuelEngineMotorcycleEzer = new FuelEngine();
                            fuelEngineMotorcycleEzer.MaxAmountEnergySource = 6.2f;
                            fuelEngineMotorcycleEzer.CurrentEnergySourceLeft = i_CurrentEnergyLeft;
                            fuelEngineMotorcycleEzer.FuelType = FuelTypeEnum.eFuelType.Octan98;
                            userEngine = fuelEngineMotorcycleEzer;
                            break;

                        case EngineTypeEnum.eEngineType.Electric:
                            ElectricEngine electricEngineMotorcycle = new ElectricEngine();
                            electricEngineMotorcycle.MaxAmountEnergySource = 2.9f;
                            electricEngineMotorcycle.CurrentEnergySourceLeft = i_CurrentEnergyLeft;
                            userEngine = electricEngineMotorcycle;
                            break;
                    }

                    break;

                case VehicleTypeEnum.eVehicleType.Truck:
                    FuelEngine fuelEngineTruck = new FuelEngine();
                    fuelEngineTruck.MaxAmountEnergySource = 125f;
                    fuelEngineTruck.CurrentEnergySourceLeft = i_CurrentEnergyLeft;
                    fuelEngineTruck.FuelType = FuelTypeEnum.eFuelType.Soler;
                    userEngine = fuelEngineTruck;
                    break;
            }

            return userEngine;
        }  
        
        public void ChangeStateForVehicle(string i_LicenseEzer, VehicleStatusEnum.eVehicleStatus i_State)
        {
            if (m_VehicleInTheGarage.TryGetValue(i_LicenseEzer, out VehicleInTheGarage vehicleInGarage))
            {
                vehicleInGarage.VehicleStatus = i_State;
            }
        }
    }
}
