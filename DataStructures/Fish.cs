namespace ApoFisher.DataStructures;

public class Fish : Item
{
    private double _size;
    public Fish(string name, double size) : base(name)
    {
        this._size = size;
    }
}