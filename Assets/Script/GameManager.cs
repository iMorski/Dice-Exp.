using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PointManager PointManager;
    [SerializeField] private DiceManager DiceManager;
    [SerializeField] private DiceButton DiceButton;
    [SerializeField] private Character Character;

    private void Awake()
    {
        DiceButton.Roll += OnRoll;
        DiceManager.RollFinish += OnRollFinish;
        Character.MoveFinish += OnMoveFinish;
    }

    private void OnRoll()
    {
        DiceManager.Roll();
    }

    private void OnRollFinish(int Number)
    {
        StartCoroutine(Character.Move(
            PointManager.GetPointGroupToMove(Number)));
    }

    private void OnMoveFinish(Transform Point)
    {
        DiceButton.OnMoveFinish();
    }

    private void OnDisable()
    {
        DiceButton.Roll -= OnRoll;
        DiceManager.RollFinish -= OnRollFinish;
        Character.MoveFinish -= OnMoveFinish;
    }
}
