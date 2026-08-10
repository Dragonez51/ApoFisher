namespace ApoFisher.DataStructures;

public class Player
{
    private Inventory Inventory;

    private Statistics _statistics = new Statistics();

    public Player() { Inventory = new Inventory(); initialize(); }
    public Player(int inventoryCapacity) { Inventory = new Inventory(inventoryCapacity); initialize(); }
    public Player(int inventoryWidth, int inventoryHeight) { Inventory = new Inventory(inventoryWidth, inventoryHeight); initialize(); }
    
    private void initialize()
    {
        // statistics = new Statistics();
    }

    public Inventory GetInventory() => Inventory;
    public Statistics GetStatistics() => _statistics;
}