namespace CareerDataTool.Domain.Location
{
    public class City
    {
        public int CityId { get; set; }
        public int StateId { get; set; }
        public State CityState { get; set; }
        public string Name { get; set; }
    }
}
