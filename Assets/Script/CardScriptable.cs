using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Card", order = 1)]
public class CardScriptable : ScriptableObject
{
    public CardType Type;
    public CardRarity Rarity;
    
    public Sprite Icon;
    public string Header;
    public string Description;
    
    public enum CardType
    {
        Summable,
        Depletable
    }
    
    public enum CardRarity
    {
        Common,
        Uncommon,
        Rare,
        Epic
    }
}
