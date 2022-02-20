using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> MonsterGroup;
    [SerializeField] private PointManager PointManager;
    [SerializeField] private DiceManager DiceManager;
    [SerializeField] private Character Character;

    private void Awake()
    {
        DiceManager.RollFinish += OnRollFinish;
        Character.MoveFinish += OnMoveFinish;
    }

    private void OnRollFinish(int Number)
    {
        StartCoroutine(Character.Move(
            PointManager.GetPointGroupToMove(Number)));
    }

    private void OnMoveFinish(Transform Point)
    {
        
    }

    private void OnDisable()
    {
        DiceManager.RollFinish -= OnRollFinish;
    }
}
