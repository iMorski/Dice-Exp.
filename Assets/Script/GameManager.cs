using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<Transform> PointGroup = new List<Transform>();
    [SerializeField] private PointManager PointManager;
    [SerializeField] private DiceManager DiceManager;
    [SerializeField] private CharacterManager CharacterManager;
    [SerializeField] private PlaneManager PlaneManager;

    private Coroutine CharacterCoroutine;
    private Coroutine PlaneCoroutine;

    private int PointCurrentIndex;

    private void Awake()
    {
        DiceManager.OnRollFinish += OnRollFinish;
    }

    private void OnRollFinish(int Number)
    {
        List<Transform> PointGroupToMove = new List<Transform>();

        for (int i = 0; i < Number; i++)
        {
            PointGroupToMove.Add(PointGroup[PointCurrentIndex + (i + 1)]);
        }

        if (CharacterCoroutine != null) StopCoroutine(CharacterCoroutine);
        CharacterCoroutine = StartCoroutine(CharacterManager.Move(PointGroupToMove));
        
        PointCurrentIndex = PointCurrentIndex + Number;

        if (PointGroup.Count - PointCurrentIndex < (12 + 1))
        {
            for (int i = 0; i < PointCurrentIndex; i++)
            {
                PointGroup.Remove(PointGroup[0]);
            }

            Transform[] NewPointGroup = PointManager.GetNewPointGroup();

            for (int i = 0; i < NewPointGroup.Length; i++)
            {
                PointGroup.Add(NewPointGroup[i]);
            }

            if (PlaneCoroutine != null) StopCoroutine(PlaneCoroutine);
            PlaneCoroutine = StartCoroutine(PlaneManager.Move(PointGroup.First().parent.position));

            PointCurrentIndex = 0;
        }
    }

    private void OnDisable()
    {
        DiceManager.OnRollFinish -= OnRollFinish;
    }
}
