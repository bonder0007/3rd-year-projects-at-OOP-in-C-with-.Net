namespace Ex03.GarageLogic
{
   public class Tire
    {
        // $G$ DSN-999 (-3) The "maximum air pressure" field should be readonly member of class wheel.
        private string m_TireManufacturerName;
        private float m_CurrentTireAirPressure;
        private float m_MaxTireAirPressureFromManufacturer;

        public Tire()
        {
            
        }

        public string ManufacturerName
        {
            get { return m_TireManufacturerName; }
            set { m_TireManufacturerName = value; }
        }

        public float CurrentTireAirPressure
        {
            get { return m_CurrentTireAirPressure; }
            set { m_CurrentTireAirPressure = value; }
        }

        public float MaxTireAirPressureFromManufacturer
        {
            get { return m_MaxTireAirPressureFromManufacturer; }
            set { m_MaxTireAirPressureFromManufacturer = value; }
        }
    }
}
