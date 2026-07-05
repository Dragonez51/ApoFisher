using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace ApoFisher.DataBases;

public class ItemsDB 
{
    public static List<FishData> Fishes { get; private set; } = [];

    public static void Load()
    {
        Fishes = JsonSerializer.Deserialize<List<FishData>>(
            File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "../../../DataBases/items.json")))!;
        
        foreach(var fish in Fishes)
        {
            fish.SetRarity();
        }
    }
}