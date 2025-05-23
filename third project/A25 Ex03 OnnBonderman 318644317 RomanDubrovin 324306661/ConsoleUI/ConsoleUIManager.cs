using System;
using System.Collections.Generic;
using System.Linq;
using Ex03.GarageLogic;
using Ex03.GarageLogic.Enum;

namespace ConsoleUI
{
    public class ConsoleUIManager 
    {
        private CollectorProperties m_CollectorPropeties = new CollectorProperties();
        private GarageManager m_GarageManager = new GarageManager();
        private string m_UserChoice;
        private bool m_Exit = false;

        public void RunUI()
        {
            while (!m_Exit)
            {
                welcomeAndPrintMenuOptions();
                getUserChoice();
                runOptionFromUser();
            }
        }

        private void welcomeAndPrintMenuOptions()
        {
            Console.WriteLine();
            Console.WriteLine("Welcome to our Garage, select an option from the menu");
            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine("1. Add Vehicle To The Garage");
            Console.WriteLine("2. See All Licences Numbers In The Garage by filter");
            Console.WriteLine("3. Change Vehicle Status");
            Console.WriteLine("4. Inflate Tires Air Pressure To Max");
            Console.WriteLine("5. Refuel in Vehicle");
            Console.WriteLine("6. Charge ElectricVehicle Battry");
            Console.WriteLine("7. Show All Details Of Vehicle");
            Console.WriteLine("8. Exit");
            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine("Please choose a number from the options:");
            Console.Write("Your Choice:");
        }

        private void getUserChoice()
        {
            bool isUserChoiceValid = false;
            while (!isUserChoiceValid)
            {
                try
                {
                    m_UserChoice = Console.ReadLine();
                    if (int.TryParse(m_UserChoice, out int choice) && choice >= 1 && choice <= 8)
                    {
                        isUserChoiceValid = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice, please enter a number between 1 and 8.");
                    }
                }
                catch (Exception ex)
                {
                    if (string.IsNullOrEmpty(m_UserChoice))
                    {
                        Console.WriteLine("Empty input, enter a number from the menu");
                    }
                    else
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            }

            Console.WriteLine();
        }

        private void checkIfEmptyUserInput(string i_UserInput)
        {
            if (string.IsNullOrWhiteSpace(i_UserInput))
            {
                throw new Exception("You have enterd empty string! please try again");
            }
        }

        private void runOptionFromUser()
        {
            int convertUserChoice = int.Parse(m_UserChoice);
            switch (convertUserChoice)
            {
                case 1:
                    addNewVehicleToTheGarage();
                    break;

                case 2:
                    displayVehicleList();
                    break;

                case 3:
                    changeVehicleStatus();
                    break;

                case 4:
                    inflateAllTiersToMaximumAirPressure();
                    break;

                case 5:
                    fillFuelInVehicle();
                    break;

                case 6:
                    fillElectricVehicle();
                    break;

                case 7:
                    showInfoOfVehicleByLicense();
                    break;

                case 8:
                    m_Exit = true;
                    break;
            }
        }

        private void showInfoOfVehicleByLicense()
        {
            Console.WriteLine("Enter the license number of the vehicle you want to check its info");
            string userLicenseNUmber = Console.ReadLine();
            if (m_GarageManager.IsDictionaryEmpty() || !(m_GarageManager.IsVehicleExistsInGarage(userLicenseNUmber)))
            {
                Console.WriteLine("There is no such license number in the Garage or Engine type doesnt fit");
            }
            else
            {
                Console.WriteLine("Here are the details of the car with the license number {0}", userLicenseNUmber);
                Console.WriteLine("------------------------------------------------------------");
                Console.WriteLine(m_GarageManager.GetInfoOnVehicle(userLicenseNUmber));
            }
        }

        // $G$ CSS-006 (-3) Missing blank line, after "if / else" blocks.
        private void fillElectricVehicle()
        {
            VehicleInTheGarage vehicleInTheGarageEzer = null;
            Console.WriteLine("Enter your license number");
            string userLicenseNumberToChange = Console.ReadLine();
            vehicleInTheGarageEzer = m_GarageManager.ReturnVehicleInGarageByLiecense(userLicenseNumberToChange);
            if (m_GarageManager.IsDictionaryEmpty() || !(m_GarageManager.IsVehicleExistsInGarage(userLicenseNumberToChange)) || vehicleInTheGarageEzer.IsFuelEngine())
            {
                Console.WriteLine("There is no such license number in the Garage or Engine type doesnt fit");
            }
            else
            {
                bool isvalidValueElectricity = false;
                float differenceElectricityToAdd =
                      (vehicleInTheGarageEzer.Vehicle.Engine.MaxAmountEnergySource - vehicleInTheGarageEzer.Vehicle.Engine.CurrentEnergySourceLeft);
                while (!isvalidValueElectricity)
                {
                    try
                    {
                        Console.WriteLine("Enter the amount of elecrticity to add, choose between 0 to {0} in minutes", differenceElectricityToAdd * 60);
                        string differenceElectricityFromUser = Console.ReadLine();
                        float convertDifferenceElectricityFromUser = float.Parse(differenceElectricityFromUser);
                        convertDifferenceElectricityFromUser /= 60;
                        if (convertDifferenceElectricityFromUser > 0)
                        {
                            m_GarageManager.RechargeBattery(userLicenseNumberToChange, convertDifferenceElectricityFromUser);
                        }
                        isvalidValueElectricity = true;
                    }
                    catch (ValueOutOfRangeException ex)
                    {
                        Console.WriteLine("Error ! Enter amount of minutes between 0 to {0}", differenceElectricityToAdd * 60);
                    }
                }
            }
        }

        // $G$ DSN-007 (-5) This method is too long, should be divided into methods.
        private void fillFuelInVehicle()
        {
            VehicleInTheGarage vehicleInTheGarageEzer = null;
            Console.WriteLine("Enter your license number");
            string userLicenseNumberToChange = Console.ReadLine();
            vehicleInTheGarageEzer = m_GarageManager.ReturnVehicleInGarageByLiecense(userLicenseNumberToChange);
            if (m_GarageManager.IsDictionaryEmpty() || !(m_GarageManager.IsVehicleExistsInGarage(userLicenseNumberToChange)) || !(vehicleInTheGarageEzer.IsFuelEngine()))
            {
                Console.WriteLine("There is no such license number in the Garage or Engine type doesnt fit");
            }
            else
            {
                string userInputFuelType = null;
                bool userDidListenFuelType = false;
                while (!userDidListenFuelType)
                {
                    try
                    {
                        Console.WriteLine("Choose which type of fuel to fill (choose a number)");
                        foreach (FuelTypeEnum.eFuelType eFuelType in Enum.GetValues(typeof(FuelTypeEnum.eFuelType)))
                        {
                            Console.WriteLine($"{(int)eFuelType}. {eFuelType}");
                        }

                        userInputFuelType = Console.ReadLine();
                        if (Enum.TryParse(userInputFuelType, out FuelTypeEnum.eFuelType ezerFuelType))
                        {
                            m_GarageManager.MatchVehicleTypeToFuelType(userLicenseNumberToChange, 0, ezerFuelType);
                        }

                        userDidListenFuelType = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Fuel type wrong, try again");
                    }
                }        
                
                if (Enum.TryParse(userInputFuelType, out FuelTypeEnum.eFuelType ezerFuel))
                {
                    float convertDifferenceFuelFromUser = 0;
                    string differenceFuelFromUser = null;
                    bool didUserListen = false;
                    float differenceFuelToAdd =
                          (vehicleInTheGarageEzer.Vehicle.Engine.MaxAmountEnergySource - vehicleInTheGarageEzer.Vehicle.Engine.CurrentEnergySourceLeft);
                    while (!didUserListen)
                    {
                        Console.WriteLine("Enter the amount of fuel to add, choose between 0 to {0}", differenceFuelToAdd);
                        try
                        {
                            differenceFuelFromUser = Console.ReadLine();
                            convertDifferenceFuelFromUser = float.Parse(differenceFuelFromUser);
                            if (convertDifferenceFuelFromUser > 0 && convertDifferenceFuelFromUser <= differenceFuelToAdd)
                            {
                                didUserListen = true;
                            }
                            m_GarageManager.RefuelVehicle(userLicenseNumberToChange, ezerFuel, convertDifferenceFuelFromUser);
                            Console.WriteLine("You have refuel the vehicle");
                        }
                        catch(ValueOutOfRangeException ex)
                        {
                            Console.WriteLine("Error ! Enter a value between 0 to {0}", differenceFuelToAdd);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Couldnt find  fuel type as entered");
                }
            }
        }

        private void inflateAllTiersToMaximumAirPressure()
        {
            Console.WriteLine("Enter your license number");
            string userLicenseNumberToChange = Console.ReadLine();
            if (m_GarageManager.IsDictionaryEmpty() || !(m_GarageManager.IsVehicleExistsInGarage(userLicenseNumberToChange)))
            {
                Console.WriteLine("There is no such license number in the Garage");
            }
            else
            {
                VehicleInTheGarage vehicleInTheGarageEzer = null;
                vehicleInTheGarageEzer = m_GarageManager.ReturnVehicleInGarageByLiecense(userLicenseNumberToChange);
                vehicleInTheGarageEzer.Vehicle.UpdateTiresToMax();
            }
        }

        private void changeVehicleStatus()
        {
            Console.WriteLine("Enter the license number of the vehicle you want to change state");
            string userLicenseNumberToChange = Console.ReadLine();
            if(m_GarageManager.IsDictionaryEmpty() || !(m_GarageManager.IsVehicleExistsInGarage(userLicenseNumberToChange)))
            {
                Console.WriteLine("There is no such license number in the Garage");
            }
            else
            {
                Dictionary<string, VehicleInTheGarage> ezerDictionary = m_GarageManager.VehicleInGarageDictionary;
                VehicleInTheGarage vehicleInTheGarageEzer = null;
                Console.WriteLine("Enter new state for your vehicle");
                foreach (VehicleStatusEnum.eVehicleStatus statusEzer in Enum.GetValues(typeof(VehicleStatusEnum.eVehicleStatus)))
                {
                    Console.WriteLine($"{(int)statusEzer}. {statusEzer}");
                }

                bool isValidInput = false;
                VehicleStatusEnum.eVehicleStatus ezerStatus = default;
                while (!isValidInput)
                {
                    string userNewState = Console.ReadLine();
                    if (int.TryParse(userNewState, out int numericState) &&
                        Enum.IsDefined(typeof(VehicleStatusEnum.eVehicleStatus), numericState))
                    {
                        ezerStatus = (VehicleStatusEnum.eVehicleStatus)numericState; // יצירת הערך מהאינומרציה
                        vehicleInTheGarageEzer = m_GarageManager.ReturnVehicleInGarageByLiecense(userLicenseNumberToChange);
                        vehicleInTheGarageEzer.VehicleStatus = ezerStatus;
                        isValidInput = true;

                        Console.WriteLine($"The status of vehicle with license {userLicenseNumberToChange} has been updated to {ezerStatus}.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid status entered. Please enter a valid number from the list:");
                    }
                }
            }
        }

        private void displayVehicleList()
        {
            if (m_GarageManager.IsDictionaryEmpty())
            {
                Console.WriteLine("The garage is empty! there is no vehicle to show ");
            }
            else
            {
                string forPrintingMethod;
                VehicleStatusEnum.eVehicleStatus vehicleStatusFromUser = VehicleStatusEnum.eVehicleStatus.InRepair;
                Console.WriteLine("Choose the type of status to filter (choose a number)");
                foreach (VehicleStatusEnum.eVehicleStatus eVehicleStatusEzer in Enum.GetValues(typeof(VehicleStatusEnum.eVehicleStatus)))
                {
                    Console.WriteLine($"{(int)eVehicleStatusEzer}. {eVehicleStatusEzer}");
                }

                Console.WriteLine("4. All");
                Console.Write("Your choice is:");
                string userStatusInput = Console.ReadLine();
                if (int.TryParse(userStatusInput, out int choice) &&
                    Enum.IsDefined(typeof(VehicleStatusEnum.eVehicleStatus), choice))
                {
                    vehicleStatusFromUser = (VehicleStatusEnum.eVehicleStatus)choice;
                    Console.WriteLine($"User Selected Vehicle type: {vehicleStatusFromUser}");
                    switch (vehicleStatusFromUser)
                    {
                        case VehicleStatusEnum.eVehicleStatus.InRepair:
                            vehicleStatusFromUser = VehicleStatusEnum.eVehicleStatus.InRepair;
                            break;

                        case VehicleStatusEnum.eVehicleStatus.Repaired:
                            vehicleStatusFromUser = VehicleStatusEnum.eVehicleStatus.Repaired;
                            break;

                        case VehicleStatusEnum.eVehicleStatus.Paid:
                            vehicleStatusFromUser = VehicleStatusEnum.eVehicleStatus.Paid;
                            break;
                    }

                    forPrintingMethod = vehicleStatusFromUser.ToString();
                }
                else
                {
                    forPrintingMethod = "All";
                    printDictionary(forPrintingMethod);
                }

                if(forPrintingMethod != "All")
                {
                    printDictionary(forPrintingMethod);
                }
            }
        }

        private void printDictionary(string i_VehicleStatus)
        {
            Console.WriteLine();
            Dictionary<string, VehicleInTheGarage> ezerDictionary = m_GarageManager.VehicleInGarageDictionary;
            foreach (KeyValuePair<string, VehicleInTheGarage> vehicleEach in ezerDictionary)
            {
                if(i_VehicleStatus == "All")
                {
                    Console.WriteLine($"License: {vehicleEach.Key}, Status: {vehicleEach.Value.VehicleStatus}");
                }
                else
                {
                    if(i_VehicleStatus == vehicleEach.Value.VehicleStatus.ToString())
                    {
                        Console.WriteLine($"License: {vehicleEach.Key}, Status: {vehicleEach.Value.VehicleStatus}");
                    }
                }
            }
        }

        private void addNewVehicleToTheGarage()
        {
            bool isCarExistsInTheGarage = false;
            Vehicle vehicleEzer = null;
            // $G$ CSS-027 (-1) Missing blank line.
            while (isCarExistsInTheGarage != true)
            {
                getLicenseNumberFromUser();
                Console.WriteLine();
                try
                {
                    if (m_GarageManager.IsVehicleExistsInGarage(m_CollectorPropeties.m_LicenseNumber) != true)
                    {
                        getVehicleTypeFromUser();
                        Console.WriteLine();
                        getEngineTypeFromUser();
                        Console.WriteLine();
                        getPersonalInfoFromUser();
                        getVehicleInfoFromUser();
                        vehicleEzer = m_GarageManager.DefineNewVehicleToTheGarage(m_CollectorPropeties);
                        setTiresForVehicle(vehicleEzer);
                        tireManufacturerName(vehicleEzer);
                        m_GarageManager.AddToGarageFinalStage(m_CollectorPropeties, vehicleEzer);
                        isCarExistsInTheGarage = true;
                        Console.WriteLine("Vehicle defined.");
                        Console.WriteLine("");
                    }
                    else
                    {
                        Console.WriteLine($"Vehicle number {m_CollectorPropeties.m_LicenseNumber} is already in the garage");
                        Console.WriteLine($"Switching the state of the vehicle to 'InRepair'");
                        m_GarageManager.ChangeStateForVehicle(m_CollectorPropeties.m_LicenseNumber, VehicleStatusEnum.eVehicleStatus.InRepair);
                        isCarExistsInTheGarage = true;
                    }
                }
                catch (ValueOutOfRangeException exception)
                {
                    Console.WriteLine($"{exception.Message}");
                    break;
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"{exception.Message}");
                }
            }
        }

        private void getLicenseNumberFromUser()
        {
            bool isNumberLicenseValid = false;
            while (isNumberLicenseValid != true)
            {
                try
                {
                    Console.WriteLine("Please enter license number:");
                    m_CollectorPropeties.m_LicenseNumber = Console.ReadLine();
                    isLicenseValid();
                    checkIfEmptyUserInput(m_CollectorPropeties.m_LicenseNumber);   //חדש
                    isNumberLicenseValid = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void isLicenseValid()
        {
            int maxLicenseLength = 8;
            if (m_CollectorPropeties.m_LicenseNumber.Length < 0 || m_CollectorPropeties.m_LicenseNumber.Length > 8)
            {
                throw new ArgumentException($"License number should be {maxLicenseLength} or {maxLicenseLength - 1} digits only");
            }

            bool isLetterOrDigit = m_CollectorPropeties.m_LicenseNumber.All(Char.IsLetterOrDigit);
            if (isLetterOrDigit == false)
            {
                throw new ArgumentException("License number should be only digits or Letters");
            }
        }

        private void getVehicleTypeFromUser()
        {
            VehicleTypeEnum.eVehicleType vehicleTypeFinal = VehicleTypeEnum.eVehicleType.Car;
            bool isValidChoice = false;
            // $G$ CSS-027 (-1) Missing blank line.
            do
            {
                Console.WriteLine("Choose the type of your Vehicle (choose a number):");
                foreach (VehicleTypeEnum.eVehicleType eVehicleTypeEzer in Enum.GetValues(typeof(VehicleTypeEnum.eVehicleType)))
                {
                    Console.WriteLine($"{(int)eVehicleTypeEzer}. {eVehicleTypeEzer}");
                }

                Console.Write("Your choice is: ");
                string userChoiceForVehicleType = Console.ReadLine();
                if (int.TryParse(userChoiceForVehicleType, out int choice) &&
                    Enum.IsDefined(typeof(VehicleTypeEnum.eVehicleType), choice))
                {
                    vehicleTypeFinal = (VehicleTypeEnum.eVehicleType)choice;
                    isValidChoice = true;
                    Console.WriteLine($"User Selected Vehicle type: {vehicleTypeFinal}");
                }
                else
                {
                    Console.WriteLine("Invalid vehicle type. Please try again.");
                }
            } 
            while (!isValidChoice);

            m_CollectorPropeties.m_VehicleType = vehicleTypeFinal;
        }

        private void getEngineTypeFromUser()
        {
            EngineTypeEnum.eEngineType engineTypeFinal = EngineTypeEnum.eEngineType.Fuel;
            if (m_CollectorPropeties.m_VehicleType != VehicleTypeEnum.eVehicleType.Truck)
            {
                bool isValidChoice = false;
                // $G$ CSS-027 (-1) Missing blank line.
                do
                {
                    Console.WriteLine("Choose the type of your Engine (choose a number):");
                    foreach (EngineTypeEnum.eEngineType eEngineTypeEzer in Enum.GetValues(typeof(EngineTypeEnum.eEngineType)))
                    {
                        Console.WriteLine($"{(int)eEngineTypeEzer}. {eEngineTypeEzer}");
                    }

                    Console.Write("Your choice is: ");
                    string userChoiceForEngineType = Console.ReadLine();
                    if (int.TryParse(userChoiceForEngineType, out int choice) &&
                        Enum.IsDefined(typeof(EngineTypeEnum.eEngineType), choice))
                    {
                        EngineTypeEnum.eEngineType engineType = (EngineTypeEnum.eEngineType)choice;
                        Console.WriteLine($"Selected Engine type: {engineType}");
                        engineTypeFinal = engineType;
                        isValidChoice = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid engine type. Please try again.");
                    }
                } 
                while (!isValidChoice);
            }
            else
            {
                Console.WriteLine("Engine type for Truck is Fuel Only!");
                engineTypeFinal = EngineTypeEnum.eEngineType.Fuel;
            }

            m_CollectorPropeties.m_EngineType = engineTypeFinal;
        }

        private void getPersonalInfoFromUser()
        {
            Console.WriteLine("Please enter your full name");
            m_CollectorPropeties.m_OwnerFullName = Console.ReadLine();
            while (m_CollectorPropeties.m_OwnerFullName.Length > 50 || (!(isValidName(m_CollectorPropeties.m_OwnerFullName)) 
                   || string.IsNullOrWhiteSpace(m_CollectorPropeties.m_OwnerFullName)))           
            {
                Console.WriteLine("Full name must be only letters and spaces, try again");
                m_CollectorPropeties.m_OwnerFullName = Console.ReadLine();
            }

            Console.WriteLine("Please enter  your phone number (05xxxxxxxx):");
            m_CollectorPropeties.m_OwnerPhoneNumber = Console.ReadLine();
            while ((m_CollectorPropeties.m_OwnerPhoneNumber.Length != 10) || (!isValidPhoneNumber(m_CollectorPropeties.m_OwnerPhoneNumber)))
            {
                Console.WriteLine("Phone number is isnvalid. Try again:");
                m_CollectorPropeties.m_OwnerPhoneNumber = Console.ReadLine();
            }
        }

        private bool isValidName(string i_FullName)
        {
            bool isValid = true;
            foreach (char currentChar in i_FullName)
            {
                if (!char.IsLetter(currentChar) && currentChar != ' ')
                {
                    isValid = false;
                    break;
                }
            }

            return isValid;
        }

        private bool isValidPhoneNumber(string i_PhoneNumber)
        {
            bool isValidPhoneNumber = true;
            while(i_PhoneNumber.Length != 9 && i_PhoneNumber.Length != 10)
            {
                Console.WriteLine("Length of phone number must be 9 or 10");
                isValidPhoneNumber =  false;
                break;
            }

            while(i_PhoneNumber[0] != '0' || i_PhoneNumber[1] != '5')
            {
                Console.WriteLine("Phone number must begin with 05X");
                isValidPhoneNumber = false;
                break;
            }

            foreach (char currentChar in i_PhoneNumber)
            {
                if (!char.IsDigit(currentChar))
                {
                    Console.WriteLine("Phone number must be only digits");
                    isValidPhoneNumber = false;
                    break;
                }
            }

            return isValidPhoneNumber;
        }

        // $G$ DSN-011 (-7) Adding a new type will require changes here, violating the guideline to modify only the factory class.
        private void getVehicleInfoFromUser()
        {
            m_CollectorPropeties.m_VehicleStatus = VehicleStatusEnum.eVehicleStatus.InRepair;
            m_CollectorPropeties.m_ModelVehicleName = getModelNameFromUser();
            Console.WriteLine();
            switch (m_CollectorPropeties.m_VehicleType)
            {
                case VehicleTypeEnum.eVehicleType.Car:
                    getCarColorFromUser();
                    getNumberOfDoorsFromUser();
                    m_CollectorPropeties.m_MaxTireAirPressureFromManufacturer = 34f;
                    break;

                case VehicleTypeEnum.eVehicleType.Motorcycle:
                    getEngineVolumeFromUser();
                    getLicenseTypeFromUser();
                    m_CollectorPropeties.m_MaxTireAirPressureFromManufacturer = 32f;
                    break;

                case VehicleTypeEnum.eVehicleType.Truck:
                    getCargoVolumeFromUser();
                    isTransferItemInCooling();
                    m_CollectorPropeties.m_MaxTireAirPressureFromManufacturer = 29f;
                    break;
            }

            if (m_CollectorPropeties.m_VehicleType != VehicleTypeEnum.eVehicleType.Truck)
            {
                switch (m_CollectorPropeties.m_EngineType)
                {
                    case EngineTypeEnum.eEngineType.Fuel:
                        if (m_CollectorPropeties.m_VehicleType == VehicleTypeEnum.eVehicleType.Car)
                        {
                            m_CollectorPropeties.m_MaxEnergyAmount = 52f;
                        }
                        else
                        {
                            m_CollectorPropeties.m_MaxEnergyAmount = 6.2f;
                        }       
                        
                        getCurrentEnergyAmountFromUser(m_CollectorPropeties.m_MaxEnergyAmount);
                        break;

                    case EngineTypeEnum.eEngineType.Electric:
                        if(m_CollectorPropeties.m_VehicleType == VehicleTypeEnum.eVehicleType.Car)
                        {
                            m_CollectorPropeties.m_MaxEnergyAmount = 5.4f;
                        }
                        else
                        {
                            m_CollectorPropeties.m_MaxEnergyAmount = 2.9f;
                        }

                        getCurrentEnergyAmountFromUser(m_CollectorPropeties.m_MaxEnergyAmount);
                        break;
                }
            }
            else
            {
                m_CollectorPropeties.m_MaxEnergyAmount = 125f;
                getCurrentEnergyAmountFromUser(m_CollectorPropeties.m_MaxEnergyAmount);
            }
        }

        private void tireManufacturerName(Vehicle i_Vehicle)
        {
            bool checkIfValid = false;
            string manufacturerName;
            Console.WriteLine("Choose each tire manufacturer: All at once or individually (choose a number)");
            Console.WriteLine("1. All at once");
            Console.WriteLine("2. Individually");
            string userChoice = Console.ReadLine();
            while (!checkIfValid)
            {
                try
                {
                    switch (userChoice)
                    {
                        case "1":
                            Console.Write("Enter manufacturer name for all tires: ");
                            manufacturerName = Console.ReadLine();
                            i_Vehicle.SetAllTireManufacturer(manufacturerName);
                            Console.WriteLine("all tire manufacturer name set successfully for all tires.");
                            checkIfValid = true;
                            break;

                        case "2":
                            for (int i = 0; i < i_Vehicle.Tires.Count; i++)
                            {
                                Console.Write($"Enter manufacturer name for tire {i + 1}: ");
                                manufacturerName = Console.ReadLine();
                                i_Vehicle.SetTireManufacturer(i, manufacturerName);
                            }

                            Console.WriteLine("Manufacturer name is set for all tires individually.");
                            checkIfValid = true;
                            break;
                    }
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }               
            }
        }

        private void setTiresForVehicle(Vehicle i_Vehicle)
        {
            bool checkIfValid = false;
            bool isValidEachSuccessful = false;
            Console.WriteLine("Choose how to fill air in tires: All at once or individually (choose a number)");
            Console.WriteLine("1. All at once");
            Console.WriteLine("2. Individually");
            string userChoice = Console.ReadLine();
            while (!checkIfValid)
            {
                try
                {
                    switch (userChoice)
                    {
                        case "1":
                            Console.Write("Enter air pressure for all tires: ");
                            float airPressureForAll = float.Parse(Console.ReadLine());
                            i_Vehicle.SetAllTiresAirPressure(airPressureForAll);
                            Console.WriteLine("Air pressure set successfully for all tires.");
                            checkIfValid = true;
                            break;

                        case "2":
                            while (!isValidEachSuccessful)
                            {
                                try
                                {
                                    for (int index = 0; index < i_Vehicle.Tires.Count; index++)
                                    { 
                                        Console.Write($"Enter air pressure for tire {index + 1}: ");
                                        float airPressureForTire = float.Parse(Console.ReadLine());
                                        i_Vehicle.SetTireAirPressure(index, airPressureForTire);
                                    }

                                    isValidEachSuccessful = true;
                                }
                                catch (ValueOutOfRangeException ex)
                                {
                                    
                                    Console.WriteLine($"Error: {ex.Message}");
                                }
                            }

                            Console.WriteLine("Air pressure set successfully for all tires individually.");
                            checkIfValid = true;
                            break;
                    }
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        private void getCurrentEnergyAmountFromUser(float i_MaxEnergy)
        {
            Console.WriteLine("Enter the current amount of EnergySource in your vehicle , maximum is {0}", i_MaxEnergy);
            string userAnswer = Console.ReadLine();
            float convertUserAnswer = float.Parse(userAnswer);
            while(convertUserAnswer < 0 || convertUserAnswer > i_MaxEnergy)
            {
                Console.WriteLine("Wrong input try again, maximum is {0}", i_MaxEnergy);
                userAnswer = Console.ReadLine();
                convertUserAnswer = float.Parse(userAnswer);
            }

            m_CollectorPropeties.m_CurrentEnergyLeft = convertUserAnswer;
        }

        private void isTransferItemInCooling()
        {
            Console.WriteLine("Is the truck transfer items in cooling? (yes/no)");
            string userAnswer = Console.ReadLine().ToLower(); 
            while (userAnswer != "yes" && userAnswer != "no") 
            {
                Console.WriteLine("Write only 'yes' or 'no'. Try again");
                userAnswer = Console.ReadLine().ToLower(); 
            }

            m_CollectorPropeties.m_IsRefrigeratedTransferOfMaterials = userAnswer == "yes";
        }

        private void getCargoVolumeFromUser()
        {
            Console.WriteLine("Enter the volume of your Cargo");
            string volumeCargoFromUser = Console.ReadLine();
            float convertVolumeFromUser = float.Parse(volumeCargoFromUser);
            while (convertVolumeFromUser < 0 || convertVolumeFromUser > 10000)
            {
                Console.WriteLine("The minimum is 100  and Maximum 8000 , try again");
                volumeCargoFromUser = Console.ReadLine();
                convertVolumeFromUser = int.Parse(volumeCargoFromUser);
            }

            m_CollectorPropeties.m_CargoVolume = convertVolumeFromUser;
        }

        private void getLicenseTypeFromUser()
        {
            Console.WriteLine();
            MotorcycleLicenseEnum.eLicenseType eLicenseType = MotorcycleLicenseEnum.eLicenseType.A1;
            bool isValidChoice = false;
            do
            {
                Console.WriteLine("Choose the license type of your Motorcycle (choose a number):");
                foreach (MotorcycleLicenseEnum.eLicenseType eLicenseTypeEzer in Enum.GetValues(typeof(MotorcycleLicenseEnum.eLicenseType)))
                {
                    Console.WriteLine($"{(int)eLicenseTypeEzer}. {eLicenseTypeEzer}");
                }

                Console.Write("Your choice is: ");
                string userChoiceForLicenseType = Console.ReadLine();
                if (int.TryParse(userChoiceForLicenseType, out int choice) &&
                    Enum.IsDefined(typeof(MotorcycleLicenseEnum.eLicenseType), choice))
                {
                    eLicenseType = (MotorcycleLicenseEnum.eLicenseType)choice;
                    Console.WriteLine($"License type selected: {eLicenseType}");
                    isValidChoice = true;
                }
                else
                {
                    Console.WriteLine("Invalid license type. Please try again.");
                }
            } 
            while (!isValidChoice);

            m_CollectorPropeties.m_LicenseType = eLicenseType;
        }

        private void getEngineVolumeFromUser()
        {
            Console.WriteLine();
            Console.WriteLine("Enter the volume of your vehicle in CC");
            bool isValidString = true;
            while (isValidString)
            {
                string volumeFromUser = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(volumeFromUser))
                {
                    Console.WriteLine("Input cannot be empty, Please enter a number");
                    continue;
                }

                int convertVolumeFromUser = int.Parse(volumeFromUser);
                while (convertVolumeFromUser < 100 || convertVolumeFromUser > 8000 || !(isNumeric(volumeFromUser)))
                {
                    Console.WriteLine("The minimum is 100 CC and Maximum 8000 CC, try again");
                    volumeFromUser = Console.ReadLine();
                    convertVolumeFromUser = int.Parse(volumeFromUser);
                }

                m_CollectorPropeties.m_EngineVolume = convertVolumeFromUser;
                isValidString = false;
            }
        }

        private bool isNumeric(string i_Input)
        {
            bool isDigitOnly = true;
            foreach (char currentChar in i_Input)
            {
                if (!char.IsDigit(currentChar))
                {
                    isDigitOnly = false;
                    break;
                }
            }

            return isDigitOnly;
        }

        private string getModelNameFromUser()
        {
            Console.WriteLine();
            Console.WriteLine("Please enter model name:");
            string modelName = Console.ReadLine();
            while(modelName.Length < 5 || modelName.Length > 30)
            {
                Console.WriteLine("Enter minimum 5 chars and maximum 30 chars");
                modelName = Console.ReadLine();
            }

            foreach (char CurrentChar in modelName)
            {
                while(!char.IsLetterOrDigit(CurrentChar) && CurrentChar != ' ')
                {
                    Console.WriteLine("Enter only letters, digits and spaces");
                    modelName = Console.ReadLine();
                }
            }

            return modelName; 
        }

        private void getCarColorFromUser()
        {
            CarColorEnum.eCarColor eCarColorEzer = CarColorEnum.eCarColor.Black;
            bool isValidChoice = false;
            do
            {
                Console.WriteLine("Choose the color of your Vehicle (choose a number):");
                foreach (CarColorEnum.eCarColor eCarColorTypeEzer in Enum.GetValues(typeof(CarColorEnum.eCarColor)))
                {
                    Console.WriteLine($"{(int)eCarColorTypeEzer}. {eCarColorTypeEzer}");
                }

                Console.Write("Your choice is: ");
                string userChoiceForCarColorType = Console.ReadLine();
                if (int.TryParse(userChoiceForCarColorType, out int choice) &&
                    Enum.IsDefined(typeof(CarColorEnum.eCarColor), choice))
                {
                    eCarColorEzer = (CarColorEnum.eCarColor)choice;
                    Console.WriteLine($"Car Color selected: {eCarColorEzer}");
                    isValidChoice = true;
                }
                else
                {
                    Console.WriteLine("Invalid color choice. Please try again.");
                }
            }
            while (!isValidChoice);

            m_CollectorPropeties.m_CarColor = eCarColorEzer;
        }
        
        private void getNumberOfDoorsFromUser()
        {
            Console.WriteLine();
            CarDoorsEnum.eCarDoors eCarDoorsNumberEzer = CarDoorsEnum.eCarDoors.Five;
            bool isValidChoice = false;
            do
            {
                Console.WriteLine("Enter the number of doors of your Vehicle (choose a number):");
                foreach (CarDoorsEnum.eCarDoors eDoorsNumberTypeEzer in Enum.GetValues(typeof(CarDoorsEnum.eCarDoors)))
                {
                    Console.WriteLine($"{(int)eDoorsNumberTypeEzer}. {eDoorsNumberTypeEzer}");
                }

                Console.Write("Your choice is: ");
                string userChoiceForDoorsNumberType = Console.ReadLine();
                if (int.TryParse(userChoiceForDoorsNumberType, out int choice) &&
                    Enum.IsDefined(typeof(CarDoorsEnum.eCarDoors), choice))
                {
                    eCarDoorsNumberEzer = (CarDoorsEnum.eCarDoors)choice;
                    Console.WriteLine($"Number Of Doors Selected: {eCarDoorsNumberEzer}");
                    isValidChoice = true;
                }
                else
                {
                    Console.WriteLine("Invalid number of doors. Please try again.");
                }
            } 
            while (!isValidChoice);

            m_CollectorPropeties.m_NumberOfDoors = eCarDoorsNumberEzer;
        }
    }
}
