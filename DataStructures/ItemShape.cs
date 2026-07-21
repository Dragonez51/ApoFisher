using System.Collections.ObjectModel;
using System.Diagnostics;
using ApoFisher.DataBases;

namespace ApoFisher.DataStructures;

public class ItemShape
{
    private ObservableCollection<PlayerInventorySlot> _slots;
    private int _width = 0;
    private int _height = 0;

    public ItemShape(string name)
    {
        _slots = new ();
        GenerateSlots(name);
        GenerateConnections();
    }

    private void GenerateSlots(string name)
    {
        bool[][] shape = ItemsDB.GetItemShape(name).shape;

        int sliceYTop = TrimShapeYTop(shape);        
        int sliceYBottom = TrimShapeYBottom(shape);

        // Debug.WriteLine("[ItemShape] GenerateSlots() => sliceYTop = "+sliceYTop+" || sliceYBottom = "+sliceYBottom);

        for(int y = sliceYTop; y < shape.Length-sliceYBottom; y++)
        {
            _height++;
            for(int x = 0; x < shape[y].Length; x++)
            {
                if (shape[y][x])
                {
                    if(x+1 > _width) _width++;
                    _slots.Add(new PlayerInventorySlot(x, y-sliceYTop));
                }
            }
        }
        // Debug.WriteLine("[ItemShape] GenerateSlots() => width = "+_width+"| height => "+_height);
    }

    private int TrimShapeYTop(bool[][] shape)
    {
        int slice = 0;

        // slice the shape to fit it's content.
        for(int y = 0; y<shape.Length; y++)
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

    private int TrimShapeYBottom(bool[][] shape)
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

    private void GenerateConnections()
    {
        // R - L generation
        for(int i=0; i<_slots.Count-1; i++)
        {
            if(_slots[i].SlotY == _slots[i+1].SlotY && _slots[i+1].SlotX - _slots[i].SlotX == 1)
            {
                Debug.WriteLine("R - L connection.");
                _slots[i].R = true;
                _slots[i+1].L = true;
            }
        }

        // D - T generation
        for(int i=0; i<_slots.Count-_width-1; i++)
        {
            if(_slots[i].SlotX == _slots[i+_width-1].SlotX && _slots[i+_width-1].SlotY - _slots[i].SlotY == 1)
            {
                Debug.WriteLine("D - T connection.");
                _slots[i].D = true;
                _slots[i+_width-1].T = true;
            }
        }
    }
    // to determine the most left top slot, we should do that within the GenerateSlots itself or even in ItemShapeData.
    public ObservableCollection<PlayerInventorySlot> GetSlots() => _slots;
    public int GetWidth() => _width;
    public int GetHeight() => _height;
}