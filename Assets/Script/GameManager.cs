using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PointManager PointManager;
    [SerializeField] private DiceManager DiceManager;
    [SerializeField] private DiceButton DiceButton;
    [SerializeField] private Character Character;
    [SerializeField] private CardManager CardManager;

    private int RollCount;

    private void Awake()
    {
        DiceButton.Roll += OnRoll;
        DiceManager.RollFinish += OnRollFinish;
        Character.MoveFinish += OnMoveFinish;
        CardManager.Pick += OnPick;
    }

    private void OnRoll()
    {
        DiceManager.Roll();

        RollCount = RollCount + 1;
    }

    private void OnRollFinish(int Number)
    {
        StartCoroutine(Character.Move(
            PointManager.GetPointGroupToMove(Number)));
    }

    private void OnMoveFinish(Transform Point)
    {
        if (RollCount != 3)
        {
            DiceButton.OnMoveFinish();
        }
        else
        {
            CardManager.Show();

            RollCount = 0;
        }
    }

    private void OnPick()
    {
        DiceButton.OnMoveFinish();
    }

    private void OnDisable()
    {
        DiceButton.Roll -= OnRoll;
        DiceManager.RollFinish -= OnRollFinish;
        Character.MoveFinish -= OnMoveFinish;
        CardManager.Pick -= OnPick;
    }
}
