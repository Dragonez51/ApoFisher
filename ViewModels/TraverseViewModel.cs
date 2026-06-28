using System;
using Avalonia.Controls;
using Avalonia.Media;
using CommunityToolkit.Mvvm.Input;

namespace ApoFisher.ViewModels;

// STATUS:
// =============================================
// Magic numbers(3): 1 (GenerateEdgeLocations)
// 2 final numbers (MAP_WIDTH, MAP_HEIGHT)
// =============================================

public partial class TraverseViewModel : ViewModelBase
{
    private readonly int MAP_WIDTH = 500;
    private readonly int MAP_HEIGHT = 500;
    private int MAP_EDGE_BOUND;
    private Canvas? _map;
    public Canvas? Map { get => _map; set => SetProperty(ref _map, value); }

    public TraverseViewModel()
    {
        MAP_EDGE_BOUND = MAP_WIDTH / 5; // aprox. 20%;
        SetupMap();
        GenerateEdgeLocations();
    }

    private void SetupMap()
    {
        Map = new Canvas();
        
        Map?.Width = MAP_WIDTH;
        Map?.Height = MAP_HEIGHT;

        Map?.Background = Brush.Parse("#EBCB88");
    }

    private void GenerateEdgeLocations()
    {
        // =======================================================
        // NOTE: check if possible to create in a single for loop.
        // Magic numbers: 1 - (MAP_HEIGHT/5): it represents 20% 
        // bound from map edge.
        // =======================================================
        Random rand = new Random();

        // Generate random point on the left side of the map excluding corners.

        // Calculate bounds.        
        var corner = MAP_HEIGHT/5;
        var heightMax = MAP_HEIGHT - (2*corner);
        
        // Create new point with random x and y.
        var x = rand.NextInt64(MAP_WIDTH/5);
        var y = rand.NextInt64(heightMax) + corner;

        Button point = new Button();
        point.Content = "X";
        point[Canvas.LeftProperty] = x;
        point[Canvas.TopProperty] = y;
        
        Map?.Children.Add(point);

        // Let's try mirroring the y point of the method overhead.
        // Note 1. Mirroring doesn't work since it will be literally mirrored. 
        // We have to only reroll the position.
        // Note 2. Reroll Y position too lmao.
        x = rand.NextInt64(MAP_WIDTH/5);
        y = rand.NextInt64(heightMax) + corner;

        point = new Button();
        point.Content = "X";
        point[Canvas.RightProperty] = x;
        point[Canvas.TopProperty] = y;

        Map?.Children.Add(point);

        // Now we have to do the same but with the other walls (Top and bottom)
        // To do so, we have to technically simply swap x and y?

        x = rand.NextInt64(MAP_WIDTH/5);
        y = rand.NextInt64(heightMax) + corner;

        point = new Button();
        point.Content = "X";
        point[Canvas.LeftProperty] = y;
        point[Canvas.TopProperty] = x;

        Map?.Children.Add(point);

        // Okay, so we just now swap TopProperty with Bottom property lmao

        x = rand.NextInt64(MAP_WIDTH/5);
        y = rand.NextInt64(heightMax) + corner;

        point = new Button();
        point.Content = "X";
        point[Canvas.LeftProperty] = y;
        point[Canvas.BottomProperty] = x;

        Map?.Children.Add(point);

        // Works like a charm. Now I can call it the edge generator.
    } 

    private void GenerateCenterLocations()
    {
        // Here we have to get a random position for bounds 
        // that present center piece. That's why the magic number
        // in (GenerateEdgeLocations) is problematic.
    }

    [RelayCommand] public void RouteVillage() => MainViewModel.RouteLocation("Village");
}