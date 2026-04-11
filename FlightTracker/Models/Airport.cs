namespace FlightTracker.Models;

public class Airport
{
    public string IataCode { get; set; }
    public string Name { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    
    public Airport() { }
    public Airport(string iataCode, string name, string city, string country, double latitude, double longitude)
    {
        IataCode = iataCode;
        Name = name;
        City = city;
        Country = country;
        Latitude = latitude;
        Longitude = longitude;
    }

}