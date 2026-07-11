namespace ApoFisher.DataStructures;

public class Player
{
    private PlayerInventory Inventory;
    private int _hp = 0;
    private int _stamina = 0;

    private Statistics statistics;

    public Player() { Inventory = new PlayerInventory(20); initialize(); }
    public Player(int inventoryCapacity) { Inventory = new PlayerInventory(inventoryCapacity); initialize(); }
    public Player(int strength, int dexterity, int intelligence, int maxHealth, int maxStamina){ Inventory = new PlayerInventory(); initialize(); }
    public Player(int strength, int dexterity, int intelligence, int maxHealth, int maxStamina, int inventoryCapacity) { Inventory = new PlayerInventory(inventoryCapacity); initialize(); }

    private void initialize()
    {
        statistics = new Statistics();
        _hp = 50;
        _stamina = statistics.GetMaxStamina();
    }

    public PlayerInventory GetInventory() => Inventory;
    public int GetHP() => _hp;
    public int GetMaxHP() => statistics.GetMaxHP();
    public int GetStamina() => _stamina;
    public int GetMaxStamina() => statistics.GetMaxStamina();
}