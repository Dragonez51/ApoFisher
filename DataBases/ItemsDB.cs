using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using ApoFisher.DataStructures;

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

    public static FishData GetFishData(string name)
    {
        foreach(var fish in Fishes)
        {
            if(name.Equals(fish.id)) return fish;
        }
        throw new Exception("[ItemsDB](GetFishData)=>(name = "+name+") could not find this fish!");
    }

    public static ItemShapeData GetItemShape(string name) => GetFishData(name).itemShapeData;
}