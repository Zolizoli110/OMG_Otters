using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using FlightTracker.Models;
using Mapsui;
using Mapsui.Extensions;
using Mapsui.Layers;
using Mapsui.Projections;
using Mapsui.Providers;
using Mapsui.Styles;
using Mapsui.Tiling;

namespace FlightTracker.Views;

public partial class FlightMap : UserControl
{
    public FlightMap()
    {
        InitializeComponent();
        MyMapControl.Map?.Layers.Add(OpenStreetMap.CreateTileLayer());
        MyMapControl.Map?.Layers.Add(CreatePoint(new List<Airport>
        {
            new Airport("CPH", "Copenhagen Airport", "Copenhagen", "Denmark", 55.6179, 12.6560),
            new Airport("SGD", "Sønderborg Airport", "Sønderborg", "Denmark", 54.9644, 9.7918),
        }));
    }

    private IEnumerable<IFeature> LoadAirports(List<Airport> airports)
    {
        List<IFeature> features = new List<IFeature>();
        foreach (Airport airport in airports)
        {
            IFeature feature = new PointFeature(SphericalMercator.FromLonLat(airport.Longitude, airport.Latitude).ToMPoint());
            features.Add(feature);
        }
        return features;
    }

    private MemoryLayer CreatePoint(List<Airport> airports)
    {
        return new MemoryLayer
        {
            Name = "Airports",
            Features = new MemoryProvider(LoadAirports(airports)).Features,
            Style = new SymbolStyle
            {
                SymbolType = SymbolType.Ellipse,
                Fill = new Brush(Color.FromArgb(255, 255, 0, 0)),
                SymbolScale = 0.5
            }
        };
    }
}