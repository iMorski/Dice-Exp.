using System.Collections.Generic;
using UnityEngine;

public class PointManager : MonoBehaviour
{
    [SerializeField] private List<Transform> PointGroup;

    private int Index;

    public List<Transform> GetPointGroupToMove(int Number)
    {
        List<Transform> PointGroupToMove = new List<Transform>();

        for (int i = 0; i < Number; i++)
        {
            if (Index + 1 < PointGroup.Count) Index = Index + 1;
            else Index = 0;
            
            PointGroupToMove.Add(PointGroup[Index]);
        }

        return PointGroupToMove;
    }
}
