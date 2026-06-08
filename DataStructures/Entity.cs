namespace ApoFisher.DataStructures;

public class Entity 
{
    private Statistics _statistics;
    private int _attackPower;
    
    public Entity() 
    {
        _statistics = new Statistics();
        _attackPower = 3;
    }

    public Entity(int strength, int dexterity, int intelligence, int maxHealth, int maxStamina)
    {
        _statistics = new Statistics(strength, dexterity, intelligence, maxHealth, maxStamina);
        _attackPower = 3;
    }

    public Statistics GetStatistics() => _statistics;
    public int GetAttackPower() => _attackPower;
}