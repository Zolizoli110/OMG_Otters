
using System.Linq;
using FlightTracker.Models;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;
using System;
namespace FlightTracker.ViewModels;
public partial class ListOfAirportsViewModel : ViewModelBase
{
    public List<Flight> Flights { get; set; }
    public List<Airport> Airports { get; set; }

    private Airport? _selectedAirport;
    public Airport? SelectedAirport
    {
        get => _selectedAirport;
        set
        {
            SetProperty(ref _selectedAirport, value);
            UpdateFilteredFlights(); // call this after setting
        }
    }

    private List<Flight> _filteredFlights = new();
    public List<Flight> FilteredFlights
    {
        get => _filteredFlights;
        set => SetProperty(ref _filteredFlights, value);
    }

    private bool _showLanded;
    public bool ShowLanded
    {
        get => _showLanded;
        set
        {
            SetProperty(ref _showLanded, value);
            UpdateFilteredFlights();
        }
    }

    private bool _showScheduled;
    public bool ShowScheduled
    {
        get => _showScheduled;
        set
        {
            SetProperty(ref _showScheduled, value);
            UpdateFilteredFlights();
        }
    }

    public ListOfAirportsViewModel()
    {
        GetJsonData();
    }

    private void UpdateFilteredFlights()
    {
        if (SelectedAirport == null)
        {
            FilteredFlights = new List<Flight>();
            return;
        }

        var flights = Flights
            .Where(f => f.DepartureAirport == SelectedAirport.IataCode
                    || f.ArrivalAirport == SelectedAirport.IataCode);

        if (ShowLanded)
            flights = flights.Where(f => f.status == "Landed");
        else if (ShowScheduled)
            flights = flights.Where(f => f.status == "Scheduled");

        FilteredFlights = flights.ToList();
    }



    private void GetJsonData()
    {
        string json = File.ReadAllText("Data/flights.json");

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var data = JsonSerializer.Deserialize<FlightData>(json, options);


        Flights = data?.Flights ?? new List<Flight>();
        Airports = data?.Airports ?? new List<Airport>();
        Console.WriteLine($"Loaded {Flights.Count} flights and {Airports.Count} airports from JSON data.");
    }
}