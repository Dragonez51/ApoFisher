using ApoFisher.DataBases;

namespace ApoFisher.DataStructures;

public class Item
{
    private string _name;
    private ItemShapeData _itemShape;

    public Item(string name)
    {
        _name = name;
        _itemShape = ItemsDB.GetItemShape(name);
    }

    public ItemShapeData GetItemShape() => _itemShape;
    public string GetName() => _name;
    public override string ToString()
    {
        return "[Item[Name = "+_name+"]]";
    }
}