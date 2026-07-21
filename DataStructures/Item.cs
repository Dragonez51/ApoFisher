namespace ApoFisher.DataStructures;

public class Item
{
    private string _name;
    private ItemShape _itemShape;

    public Item(string name)
    {
        this._name = name;
        _itemShape = new ItemShape(name);
    }

    public ItemShape GetItemShape() => _itemShape;
    public string GetName() => _name;
}