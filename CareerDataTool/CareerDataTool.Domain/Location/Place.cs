namespace CareerDataTool.Domain.Location
{
    public class Place
    {
        public int PlaceID { get; set; }
        public int? CountryID { get; set; }
        public Country ?PlaceCountry { get; set; }
        public int? StateID { get; set; }
        public State? PlaceState { get; set; }
        public int? CityID { get; set; }
        public City? PlaceCity { get; set; }
    }
}
