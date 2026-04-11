using System;

namespace FlightTracker.Models;

public class Flight
{
    public string FlightNumber { get; set; }
    public string AirlineName { get; set; }
    public string AirlineCode { get; set; }
    public string DepartureAirport { get; set; }
    public string ArrivalAirport { get; set; }
    public DateTime ScheduledDeparture { get; set; }
    public DateTime ScheduledArrival { get; set; }
    public string AircraftType { get; set; }
    public string status { get; set; }

    
    public Flight() { }
    public Flight(string flightNumber, string airlineName, string airlineCode, string departureAirport, string arrivalAirport, DateTime scheduledDeparture, DateTime scheduledArrival, string aircraftType, string status)
    {
        FlightNumber = flightNumber;
        AirlineName = airlineName;
        AirlineCode = airlineCode;
        DepartureAirport = departureAirport;
        ArrivalAirport = arrivalAirport;
        ScheduledDeparture = scheduledDeparture;
        ScheduledArrival = scheduledArrival;
        AircraftType = aircraftType;
        this.status = status;
    }
    
}


