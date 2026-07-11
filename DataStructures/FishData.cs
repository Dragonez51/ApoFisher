public record FishData(string id, string location, double minSize, double maxSize, double chance, bool[] slots)
{
    private string rarity { get; set; } = "unknown";

    public void SetRarity()
    {
        if(chance > 50) rarity = "common";
        else if(chance > 30) rarity = "uncommon";
        else if(chance > 15) rarity = "rare";
        else if(chance > 5) rarity = "epic";
        else if(chance > 0.5) rarity = "legendary";
        else if(chance > 0) rarity = "mythical";
    }

    public override string ToString()
    {
        return "[Fish](id='"+id+"')(location='"+location+"')(minSize='"+minSize+"')(maxSize='"+maxSize+"')(chance='"+chance+"')(rarity='"+rarity+"')\n";
    }
}