using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] private Animator Animator;
    [SerializeField] private float Speed;
    [SerializeField] private float SpeedRotation;
    [SerializeField] private float SpeedAnimation;
    [SerializeField] private float CurvePointCount;
    [SerializeField] private float CurveStrength;

    public delegate void OnMoveFinish();
    public event OnMoveFinish MoveFinish;

    public IEnumerator Move(List<Transform> PointGroup)
    {
        Animator.CrossFade("Character-Run", SpeedAnimation);

        for (int i = 0; i < PointGroup.Count; i++)
        {
            Vector3 CurrentPointPosition = transform.position;
            Vector3 NextPointPosition = new Vector3(PointGroup[i].position.x, CurrentPointPosition.y, PointGroup[i].position.z);
            Vector3 MiddlePointPosition = new Vector3((CurrentPointPosition.x + NextPointPosition.x) / 2.0f, CurrentPointPosition.y,
                (CurrentPointPosition.z + NextPointPosition.z) / 2.0f);

            Vector3 ReachPointPosition = new Vector3(MiddlePointPosition.x  * (1.0f + CurveStrength), CurrentPointPosition.y,
                MiddlePointPosition.z + (MiddlePointPosition.z - PointGroup[i].parent.position.z) * CurveStrength);

            for (int j = 0; j < CurvePointCount; j++)
            {
                Vector3 CurvePointPosition = CalculatePointOnCurvePosition((j + 1.0f) / CurvePointCount, 
                    CurrentPointPosition, ReachPointPosition, NextPointPosition);
                
                while (transform.position != CurvePointPosition)
                {
                    ChangeRotation(CurvePointPosition);
                    ChangePosition(CurvePointPosition);
                    
                    yield return new WaitForEndOfFrame();
                }
            }
        }
        
        Animator.CrossFade("Character-Wait", SpeedAnimation);
        
        MoveFinish.Invoke();
    }

    private void ChangePosition(Vector3 Position)
    {
        transform.position = Vector3.MoveTowards(transform.position,
            Position, Speed * Time.deltaTime);
    }

    private Vector3 RotationDirection;
    
    private void ChangeRotation(Vector3 Position)
    {
        RotationDirection = Vector3.MoveTowards(RotationDirection, Position - transform.position,
            SpeedRotation * Time.deltaTime);
        
        transform.rotation = Quaternion.LookRotation(RotationDirection);
    }

    private Vector3 CalculatePointOnCurvePosition(float CurrentPointOnCurve, Vector3 Point01Position, Vector3 ReachPosition, Vector3 Point02Position)
    {
        Vector3 Result = Mathf.Pow((1 - CurrentPointOnCurve), 2.0f) * Point01Position;
        Result = Result + 2 * (1 - CurrentPointOnCurve) * CurrentPointOnCurve * ReachPosition;
        Result = Result + Mathf.Pow(CurrentPointOnCurve, 2.0f) * Point02Position;

        return Result;
    }
}
