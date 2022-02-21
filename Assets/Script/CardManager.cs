using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardManager : MonoBehaviour
{
    [SerializeField] private GameObject Panel;
    [SerializeField] private List<Card> CardGroup;
    [SerializeField] private List<CardScriptable> CardScriptableGroup;
    [SerializeField] private Color ColorCommon;
    [SerializeField] private Color ColorUncommon;
    [SerializeField] private Color ColorRare;
    [SerializeField] private Color ColorEpic;

    private List<int> CardCountGroup = new List<int>();

    private void Awake()
    {
        for (int i = 0; i < CardScriptableGroup.Count; i++)
        {
            CardCountGroup.Add(0);
        }
    }

    public void Show()
    {
        Panel.SetActive(true);
        
        for (int i = 0; i < CardGroup.Count; i++)
        {
            CardGroup[i].EnableTouch();
        }

        List<int> CardScriptableIndexGroup = new List<int>();

        for (int i = 0; i < CardGroup.Count; i++)
        {
            int CardScriptableIndex = Random.Range(0, CardScriptableGroup.Count);

            if (CardGroup.Count <= CardScriptableGroup.Count)
            {
                while (CardScriptableIndexGroup.Contains(CardScriptableIndex))
                {
                    CardScriptableIndex = Random.Range(0, CardScriptableGroup.Count);
                }
            }
            else
            {
                Debug.Log("Not enough scriptable!");
            }
            
            CardScriptableIndexGroup.Add(CardScriptableIndex);
            
            Set(CardGroup[i], CardScriptableIndex);
        }

        for (int i = 0; i < CardGroup.Count; i++)
        {
            CardGroup[i].Show();
        }
    }

    public IEnumerator Hide(int Index)
    {
        for (int i = 0; i < CardGroup.Count; i++)
        {
            CardGroup[i].DisableTouch();
        }
        
        switch (CardScriptableGroup[Index].Type)
        {
            case CardScriptable.CardType.Summable: CardCountGroup[Index] = CardCountGroup[Index] + 1; break;
            case CardScriptable.CardType.Depletable:
                
                CardScriptableGroup.Remove(CardScriptableGroup[Index]);
                CardCountGroup.Remove(CardCountGroup[Index]);

                break;
        }
        
        yield return new WaitForSeconds(0.25f);
        
        for (int i = 0; i < CardGroup.Count; i++)
        {
            CardGroup[i].Hide();
        }
        
        Panel.SetActive(false);
    }

    private void Set(Card Card, int Index)
    {
        CardScriptable Scriptable = CardScriptableGroup[Index];
        
        Sprite Icon = Scriptable.Icon;
        
        Color Color = new Color();

        switch (Scriptable.Rarity)
        {
            case CardScriptable.CardRarity.Common: Color = ColorCommon; break;
            case CardScriptable.CardRarity.Uncommon: Color = ColorUncommon; break;
            case CardScriptable.CardRarity.Rare: Color = ColorRare; break;
            case CardScriptable.CardRarity.Epic: Color = ColorEpic; break;
        }
        
        switch (CardScriptableGroup[Index].Type)
        {
            case CardScriptable.CardType.Summable: Card.EnableCount(); break;
            case CardScriptable.CardType.Depletable: Card.DisableCount(); break;
        }
        
        string Count = "x" + CardCountGroup[Index];
        string Header = Scriptable.Header;
        string Description = Scriptable.Description;
        
        Card.Set(Index, Icon, Color, Count, Header, Description);
    }
}
