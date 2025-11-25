namespace CareerDataTool.Domain.Location
{
    public class State
    {
        public int StateId { get; set; }
        public int CountryId { get; set; }
        public Country StateCountry { get; set; }
        public string Name { get; set; }
        public string Acronym { get; set; }           
        public List<City> Cities { get; set; }
    }
}
