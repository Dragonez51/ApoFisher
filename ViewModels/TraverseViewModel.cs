using System;
using System.Diagnostics;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Layout;
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
    private readonly int BUTTON_SIZE = 50;
    private int LINE_CENTER_OFFSET;
    private int MAP_EDGE_BOUND;

    // until the map has 5 static points, 
    // the location tab is also static.
    private Point[] _locations = new Point[5];

    private Canvas? _map;
    public Canvas? Map { get => _map; set => SetProperty(ref _map, value); }
    
    public TraverseViewModel()
    {
        SetupMap();
        GenerateEdgeLocations();
        GenerateCenterLocations();
        // GenerateRoadsInOrder();
        // GenerateRoadsv2();
        GenerateRoadsProximity();
    }

    private void SetupMap()
    {
        Map = new Canvas();
        // _locations = new Point[5];

        MAP_EDGE_BOUND = MAP_WIDTH / 5; // aprox. 20%;
        LINE_CENTER_OFFSET = BUTTON_SIZE / 2;

        Map?.Width = MAP_WIDTH;
        Map?.Height = MAP_HEIGHT;

        Map?.Background = Brush.Parse("#EBCB88");
    }

    private void GenerateEdgeLocations()
    {
        // =======================================================
        // NOTE: check if possible to create in a single for loop. in progress...
        // =======================================================
        Random rand = new Random();

        long x;
        long y;
        Button point;
        var heightMax = MAP_HEIGHT - (2*MAP_EDGE_BOUND);

        //generate the same shit but within a single for loop.
        for(int i=0; i<4; i++)
        {
            // generate random points within the bounds.
            // buttons on right and bottom edge
            // require an offset to not go out of bounds.
            if(i % 2 == 0)
            {
                x = rand.NextInt64(MAP_EDGE_BOUND);
            }
            else
            {
                x = rand.NextInt64(MAP_EDGE_BOUND-BUTTON_SIZE);
            }
            y = rand.NextInt64(heightMax) + MAP_EDGE_BOUND;

            // generate a styled button.
            point = new Button
            {
                Content="X",
                Width = BUTTON_SIZE,
                Height = BUTTON_SIZE,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
                FontSize = BUTTON_SIZE/2,
                FontWeight = FontWeight.Bold,
                Foreground = Brush.Parse("#930000"),
                ZIndex = 1
            };

            // NOTE 1: Buttons are drawn from their top left corner.
            // This means that both bottom and right edge points
            // can spawn in a way where the button is out of bounds for map.
            // We can just offset them by 
            // their width (for right edge)
            // and by their height (for bottom edge)
            // This bug exists since we don't use the Right property that
            // presumably happens to offset the button from it's right corner
            // making it impossible to go out of bounds.
            // .
            // NOTE 2: I feel like we can make it without the switch loop
            // but I am not going to fix it right now. 
            // This could prepare it tho for a bigger map.

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

            Map?.Children.Add(point);
            _locations?[i] = new Point(Canvas.GetLeft(point), Canvas.GetTop(point));
        }
    } 

    private void GenerateCenterLocations()
    {
        // Here we have to get a random position for bounds 
        // that present center piece. That's why the magic number
        // in (GenerateEdgeLocations) is problematic.

        Random rand = new Random();

        // =================================
        // v1.0 -> let's do a single target.
        // =================================

        // A little inset has been added here so that the points 
        // do not get on top of each other.

        var heightBound = MAP_HEIGHT - (2 * MAP_EDGE_BOUND) - BUTTON_SIZE;
        var widthBound = MAP_WIDTH - (2 * MAP_EDGE_BOUND) - BUTTON_SIZE;

        var x = rand.NextInt64(widthBound) + MAP_EDGE_BOUND;
        var y = rand.NextInt64(heightBound) + MAP_EDGE_BOUND;

        Button point = new Button
        {
            Content="X",
            Width = BUTTON_SIZE,
            Height = BUTTON_SIZE,
            HorizontalContentAlignment = HorizontalAlignment.Center,
            VerticalContentAlignment = VerticalAlignment.Center,
            FontSize = BUTTON_SIZE/2,
            FontWeight = FontWeight.Bold,
            Foreground = Brush.Parse("#930000"),
            ZIndex = 1
        };
        point[Canvas.LeftProperty] = x;
        point[Canvas.TopProperty] = y;

        Map?.Children.Add(point);

        // Here we don't have to get the property of this point
        // since there's no mixing x and y.
        _locations[4] = new Point(x, y);

        //Single target done.
    }

    private void GenerateRoadsInOrder()
    {
        //  To do this generation, we need to change some things
        //  in previous methods.
        //  A tab is required for remembering each location position 
        //  to create a road from. DONE
        //  Thus we also need to have a data structure for those points.
        //  That is Vector2. DONE
        //  PROBLEM 1.: not every point is presented the same way.
        //  Some points are presented with Left property and some with Right.
        //  This creates a problem if we want to have
        //  all the locations in the same format. DONE
        //  We need to recreate the whole process with a simple for loop
        //  and a little bit more calculations. DONE

        // offsets:
        int offsetX = BUTTON_SIZE/2;
        int offsetY = BUTTON_SIZE/2;
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
            // locations:
            // NOTE: We can offset these to 
            // get the line to start from 
            // the center and end in the center.
            line.StartPoint = new Point(_locations[i].X + offsetX, _locations[i].Y + offsetY);
            line.EndPoint = new Point(_locations[i+1].X + offsetX, _locations[i+1].Y + offsetY);

            Map?.Children.Add(line);
        }
        // line.StartPoint = _locations[0];
    }

    private void GenerateRoadsv2()
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
            
            Map?.Children.Add(line);
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

            Debug.WriteLine(i%_locations.Length+"<=>"+minIndex);

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

            line.StartPoint = new Point(_locations[i%_locations.Length].X + LINE_CENTER_OFFSET, _locations[i%_locations.Length].Y + LINE_CENTER_OFFSET);
            line.EndPoint = new Point(_locations[minIndex].X + LINE_CENTER_OFFSET, _locations[minIndex].Y + LINE_CENTER_OFFSET);
            
            Map?.Children.Add(line);
        }
    }

    [RelayCommand] public void RouteVillage() => MainViewModel.RouteLocation("Village");
}