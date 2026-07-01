using System;
using Avalonia;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Collections;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using System.Diagnostics;
using ApoFisher.ViewModels;

public partial class Map
{
    private readonly int MAP_WIDTH = 500;
    private readonly int MAP_HEIGHT = 500;
    private readonly int BUTTON_SIZE = 50;

    private int LINE_CENTER_OFFSET;
    private int MAP_EDGE_BOUND;

    private Button[] _buttons;
    private Point[] _locations;
    private Canvas _canvas;

    public Map()
    {
        _buttons = new Button[5];
        _locations = new Point[5];
        _canvas = new Canvas();
        Initialize();

        GenerateGridLocations();
        GenerateControlledRoads();
        // GenerateRoadsProximity();
    }

    private void Initialize()
    {
        MAP_EDGE_BOUND = MAP_HEIGHT / 5; // aprox 20%
        LINE_CENTER_OFFSET = BUTTON_SIZE / 2;

        _canvas?.Width = MAP_WIDTH;
        _canvas?.Height = MAP_HEIGHT;
        _canvas?.Background = Brush.Parse("#EBCB88");
    }

    private Button GenerateStandardButton(int locationIndex)
    {
        return new Button
            {
                Content="X",
                Width = BUTTON_SIZE,
                Height = BUTTON_SIZE,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
                FontSize = BUTTON_SIZE/2,
                FontWeight = FontWeight.Bold,
                Foreground = Brush.Parse("#930000"),
                Background = Brush.Parse("#00000000"),
                Command = GoToLocationCommand,
                CommandParameter = locationIndex,
                ZIndex = 1
            };
    }

    [RelayCommand] private void GoToLocation(int id)
    {
        if(id == 1)
        {
            MainViewModel.RouteLocation("Village");
            return;
        }
        MainViewModel.RouteLocation("Lake "+(id-1));
    }

    // This refers to spawning the locations acordingly
    // to the grid. L -> R -> T -> B -> C
    private void GenerateGridLocations()
    {
        Random rand = new Random();

        long x;
        long y;
        Button point;
        var heightMax = MAP_HEIGHT - (4*MAP_EDGE_BOUND);

        int[] ids = {1, 3, 2, 4};

        //generate the same shit but within a single for loop.
        for(int i=0; i<_locations?.Length-1; i++)
        {
            // generate random points within the bounds.
            x = rand.NextInt64(MAP_EDGE_BOUND-BUTTON_SIZE);
            y = rand.NextInt64(heightMax) + MAP_EDGE_BOUND;

            // generate a styled button.
            point = GenerateStandardButton(ids[i]);

            // Switch between different iterations to
            // decide upon the positioning.
            switch (i)
            {
                case 0:
                    point[Canvas.LeftProperty] = x;
                    point[Canvas.TopProperty] = y;
                    break;
                case 1:
                    point[Canvas.LeftProperty] = x + (MAP_WIDTH - MAP_EDGE_BOUND);
                    point[Canvas.TopProperty] = y;
                    break;
                case 2:
                    point[Canvas.LeftProperty] = y;
                    point[Canvas.TopProperty] = x;
                    break;
                case 3:
                    point[Canvas.LeftProperty] = y;
                    point[Canvas.TopProperty] = x + (MAP_WIDTH - MAP_EDGE_BOUND);
                    break;
            }

            _canvas.Children.Add(point);
            _locations?[i] = new Point(Canvas.GetLeft(point), Canvas.GetTop(point));
            _buttons[i] = point;
        }

        // A little inset has been added here so that the points 
        // do not get on top of each other.

        var heightBound = MAP_HEIGHT - (2 * MAP_EDGE_BOUND) - BUTTON_SIZE;
        var widthBound = MAP_WIDTH - (2 * MAP_EDGE_BOUND) - BUTTON_SIZE;

        x = rand.NextInt64(widthBound) + MAP_EDGE_BOUND;
        y = rand.NextInt64(heightBound) + MAP_EDGE_BOUND;

        point = GenerateStandardButton(5);
        point[Canvas.LeftProperty] = x;
        point[Canvas.TopProperty] = y;

        _canvas.Children.Add(point);

        // Here we don't have to get the property of this point
        // since there's no mixing x and y.
        _locations?[4] = new Point(x, y);
    }

    
    // this might simply be the demo version
    // since I don't think that I can manage anything better for now. 
    private void GenerateControlledRoads() 
    {
        int[] pointIndex = { 0, 2, 1, 3, 4 };

        for(int i=0; i<_locations.Length-1; i++)
        {
            Line line = new Line();
            // styling:
            line.Stroke = Brush.Parse("#000");
            line.StrokeThickness = 3;
            line.StrokeLineCap = PenLineCap.Round;
            var dashPattern = new AvaloniaList<double>
            {
                2.5,
            };
            line.StrokeDashArray = dashPattern;
            line.ZIndex = 0;

            line.StartPoint = new Point(_locations[pointIndex[i]].X + LINE_CENTER_OFFSET, _locations[pointIndex[i]].Y + LINE_CENTER_OFFSET);
            line.EndPoint = new Point(_locations[pointIndex[i+1]].X + LINE_CENTER_OFFSET, _locations[pointIndex[i+1]].Y + LINE_CENTER_OFFSET);
            
            _canvas.Children.Add(line);
        }

    }

    private void GenerateRoadsProximity()
    {
        // This generator is meant to connect
        // only the closest points.

        AvaloniaList<string> connections = new AvaloniaList<string>();

        for(int i=0; i<4; i++)
        {
            double minDistance = MAP_WIDTH;
            int minIndex = -1;

            double Ax = _locations[i%_locations.Length].X;
            double Ay = _locations[i%_locations.Length].Y;

            for(int j=0; j<_locations.Length; j++)
            {
                // Do not check yourself.
                if(i%_locations.Length == j) continue;
                
                // Do not check if there is already a connection between these two.
                bool skip = false;
                foreach(string connection in connections)
                {
                    if(connection.Equals(i%_locations.Length+""+j) || connection.Equals(j + "" + i%_locations.Length))
                    {
                        skip = true;
                        break;
                    }
                }
                if(skip) continue;

                // All good, calculate distance.
                double Bx = _locations[j].X;
                double By = _locations[j].Y;

                double differenceX = 0;
                if(Ax > Bx) differenceX = Ax - Bx;
                if(Ax < Bx) differenceX = Bx - Ax;
                double differenceY = 0;
                if(Ay > By) differenceY = Ay - By;
                if(Ay < By) differenceY = By - Ay;

                double distance = Math.Sqrt((differenceX*differenceX) + (differenceY*differenceY));

                // If it is smaller than the smallest, 
                // save which one it is and its distance.
                if(distance < minDistance)
                {
                    minDistance = distance;
                    minIndex = j; 
                } 
            }

            // if the smallest distance has not been found, throw an exception.
            if(minIndex == -1) throw new Exception("[TraverseViewModel](GenerateRoadProximity) minIndex == -1 => couldn't find a smaller distance than MAP_WIDTH ("+MAP_WIDTH+")");
            
            // if the smallest distance has not been found, skip this location?
            // if(minIndex == -1) continue;

            connections.Add(i%_locations.Length+""+minIndex);

            Line line = new Line();
            // styling:
            line.Stroke = Brush.Parse("#1a000000");
            line.StrokeThickness = 3;
            line.StrokeLineCap = PenLineCap.Round;
            var dashPattern = new AvaloniaList<double>
            {
                2.5,
            };
            line.StrokeDashArray = dashPattern;
            line.ZIndex = 0;

            line.StartPoint = new Point(_locations[i%_locations.Length].X + LINE_CENTER_OFFSET, _locations[i%_locations.Length].Y + LINE_CENTER_OFFSET);
            line.EndPoint = new Point(_locations[minIndex].X + LINE_CENTER_OFFSET, _locations[minIndex].Y + LINE_CENTER_OFFSET);
            
            _canvas.Children.Add(line);
        }
    }

    public Canvas GetCanvas()
    {
        return this._canvas;
    }
}