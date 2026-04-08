using System;

namespace FlightTracker.Models;

public class Flight
{
    public int FlightNumber { get; set; }
    public string AirlineName { get; set; }
    public string AirlineCode { get; set; }
    public Airport DepartureAirport { get; set; }
    public Airport ArrivalAirport { get; set; }
    public DateTime ScheduledDepartureTime { get; set; }
    public DateTime ScheduledArrivalTime { get; set; }
    public string AircraftType { get; set; }
    public string status { get; set; }
    
    public Flight(int flightNumber, string airlineName, string airlineCode, Airport departureAirport, Airport arrivalAirport, DateTime scheduledDepartureTime, DateTime scheduledArrivalTime, string aircraftType, string status)
    {
        FlightNumber = flightNumber;
        AirlineName = airlineName;
        AirlineCode = airlineCode;
        DepartureAirport = departureAirport;
        ArrivalAirport = arrivalAirport;
        ScheduledDepartureTime = scheduledDepartureTime;
        ScheduledArrivalTime = scheduledArrivalTime;
        AircraftType = aircraftType;
        this.status = status;
    }
}