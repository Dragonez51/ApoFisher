using System.Collections.Generic;
using System.Diagnostics;

namespace ApoFisher.DataStructures;

public record ItemShapeData(bool[][] shape)
{
    // public bool[][] shape { get => shape; }
    public List<InventorySlot> ItemSlots { get; private set; } = [];
    public int Width    { get; private set; } = 0;
    public int Height   { get; private set; } = 0;
    public int MinX     { get; private set; } = 0;

    public void InitializeItemSlots()
    {
        int startX = 0;
        int startY = 0;

        int trimTop = TrimshapeTop();
        int trimBottom = TrimshapeBottom();

        int trimLeft = TrimshapeLeft();
        int trimRight = TrimshapeRight();
        // int trimLeft = 0;
        // int trimRight = 0;

        Width = shape.Length - trimLeft - trimRight;
        Height = shape[0].Length - trimTop - trimBottom;

        // Debug.WriteLine("[LoopData]\n[trimTop: "+trimTop+"][trimBottom: "+trimBottom+"]\n[trimLeft: "+trimLeft+"][trimRight: "+trimRight+"]\n [y < "+(shape.Length - trimBottom)+"]");
        for(int y = trimTop; y < shape.Length - trimBottom; y++)
        {
            for(int x = trimLeft; x < shape[y].Length - trimRight; x++)
            {
                if (shape[y][x])
                {
                    if(ItemSlots.Count == 0)
                    {
                        startX = x;
                        startY = y;
                        // Debug.WriteLine("   [Root]["+x+"]["+y+"]");
                        ItemSlots.Add(new InventorySlot(0, 0));
                        continue;
                    }
                    var xcoord = x - startX;
                    if(xcoord < MinX) MinX = xcoord;
                    var ycoord = y - startY;
                    // Debug.WriteLine("   [Slot]["+xcoord+"]["+ycoord+"]");
                    ItemSlots.Add(new InventorySlot(xcoord, ycoord));
                }
            }
        }
    }

    private int TrimshapeTop()
    {
        int slice = 0;

        // slice the shape to fit it's content.
        for(int y = 0; y<this.shape.Length; y++)
        {
            bool isRowEmpty = true;
            for(int x = 0; x<shape[y].Length; x++)
            {
                if (shape[y][x])
                {
                    isRowEmpty = false;
                    break;
                }
            }
            if(isRowEmpty) slice++;
            else break;
        }

        return slice;
    }

    private int TrimshapeBottom()
    {
        int slice = 0;

        for(int y = shape.Length-1; y>=0; y--)
        {
            bool isRowEmpty = true;
            for(int x = 0; x<shape[y].Length; x++)
            {
                if (shape[y][x])
                {
                    isRowEmpty = false;
                    break;
                }
            }
            if(isRowEmpty) slice++;
            else break;
        }

        return slice;
    }

    private int TrimshapeLeft()
    {
        int slice = 0;

        for(int x = 0; x < shape[0].Length; x++)
        {
            bool isRowEmpty = true;
            for(int y = 0; y<shape.Length; y++)
            {
                if (shape[y][x])
                {
                    isRowEmpty = false;
                    break;
                }
            }
            if(isRowEmpty) slice++;
            else break;
        }

        return slice;
    }

    private int TrimshapeRight()
    {
        int slice = 0;

        for(int x = shape[0].Length-1; x>=0; x--)
        {
            bool isRowEmpty = true;
            for(int y = 0; y<shape.Length; y++)
            {
                if (shape[y][x])
                {
                    isRowEmpty = false;
                    break;
                }
            }
            if(isRowEmpty) slice++;
            else break;
        }

        return slice;
    }

    // private int[] TrimAllSide()
    // {
    //     //             L  T  R  B
    //     int[] slice = {0, 0, 0, 0};

    //     bool readyT = false;
    //     bool readyB = false;

    //     for(; !readyT && !readyB;)
    //     {
    //         int 
    //     }

    //     bool readyL = false;
    //     bool readyR = false;

    //     return slice;
    // }
}