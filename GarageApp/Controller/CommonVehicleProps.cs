namespace GarageApp.Controller
{
    internal record CommonVehicleProps(string RegNo, string Color, int NrOfWheels);


    internal class CM
    {
        public string RegNo { get; }
        public CM(string regNo)
        {
            RegNo = regNo;
        }

    }

}