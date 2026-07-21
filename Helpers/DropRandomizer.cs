using System;
using System.Collections.Generic;

namespace ApoFisher.Helpers;

public static class DropRandomizer<T>
{
    public static List<DropElement<T>> DropList { get; set; } = [];

    public static void AddElement(T Element, double chance)
    {
        DropList.Add(new DropElement<T>(Element, chance));
    }

    public static void Reset()
    {
        DropList = new List<DropElement<T>>();
    }

    public static void SetRanges()
    {
        DropList[0].SetRange(0, DropList[0].chance);

        double lastMax = DropList[0].GetMax();
        for(int i=1; i<DropList.Count; i++)
        {
            DropList[i].SetRange(lastMax);
            lastMax = DropList[i].GetMax();
        }
    }

    public static T Draw()
    {
        if(DropList.Count == 0) throw new Exception("[DropRandomizer](Draw) DropList.Count == 0!");

        Random rand = new Random();
        double number = rand.NextDouble() * 100;
        foreach(var drop in DropList)
        {
            if(number >= drop.GetMin() && number < drop.GetMax())
            {
                return drop.Drop;
            }
        }
        throw new NullDrawException("[DropRandomizer](Draw) drew a null item!");
    }
}