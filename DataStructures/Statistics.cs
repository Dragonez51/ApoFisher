namespace ApoFisher.DataStructures;

public class Statistics 
{
    public int _strength;
    public int _dexterity;
    public int _intelligence;
    public int _maxHealth;
    public int _maxStamina;

    public Statistics() 
    {
        _strength = 1;
        _dexterity = 1;
        _intelligence = 1;
        _maxHealth = 100;
        _maxStamina = 100;
    }

    public Statistics(int strength, int dexterity, int intelligence, int maxHealth, int maxStamina) 
    {
        _strength = strength;
        _dexterity = dexterity;
        _intelligence = intelligence;
        _maxHealth = maxHealth;
        _maxStamina = maxStamina;
    }

    public int GetStrength() => _strength;
    public int GetDexterity() => _dexterity;
    public int GetIntelligence() => _intelligence;
    public int GetMaxHP() => _maxHealth;
    public int GetMaxStamina() => _maxStamina;
}