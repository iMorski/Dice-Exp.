using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private Image Icon;
    [SerializeField] private Image Rarity;
    [SerializeField] private Text Count;
    [SerializeField] private Text Header;
    [SerializeField] private Text Description;
    [SerializeField] private Touch Touch;
    
    private Animator Animator;

    private void Awake()
    {
        Animator = GetComponent<Animator>();
        
        Touch.TapBegin += OnTapBegin;
        Touch.TapFinish += OnTapFinish;
    }

    private void OnTapBegin()
    {
        Animator.Play("Card Push In");
    }

    private int ScriptableIndex;
    
    private void OnTapFinish()
    {
        Animator.Play("Card Push Out");

        StartCoroutine(GetComponentInParent<CardManager>().Hide(ScriptableIndex));
    }

    public void Set(int Index, Sprite Sprite, Color Color, string Count, string Header, string Description)
    {
        ScriptableIndex = Index;
        
        Icon.sprite = Sprite;
        Rarity.color = Color;

        this.Count.text = Count;
        this.Header.text = Header;
        this.Description.text = Description;
    }

    public void Show() { Animator.Play("Card Show"); }
    public void Hide() { Animator.Play("Card Hide"); }
    public void EnableCount() { Count.enabled = true; }
    public void DisableCount() { Count.enabled = false; }
    public void EnableTouch() { Touch.enabled = true; }
    public void DisableTouch() { Touch.enabled = false; }
}
