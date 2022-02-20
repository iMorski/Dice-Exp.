using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Animator Animator;
    [SerializeField] private float Speed;
    [SerializeField] private float SpeedRotation;
    [SerializeField] private float SpeedAnimation;

    public delegate void OnMoveFinish(Transform Point);
    public event OnMoveFinish MoveFinish;

    public IEnumerator Move(List<Transform> PointGroup)
    {
        Animator.CrossFade("Character-Run", SpeedAnimation);

        for (int i = 0; i < PointGroup.Count; i++)
        {
            Vector3 Position = new Vector3(PointGroup[i].position.x,
                transform.position.y, PointGroup[i].position.z);
            
            while (transform.position != Position)
            {
                ChangeRotation(Position);
                ChangePosition(Position);
                    
                yield return new WaitForEndOfFrame();
            }

            if (!(i != PointGroup.Count - 1))
            {
                MoveFinish.Invoke(PointGroup[i]);
            }
        }
        
        Animator.CrossFade("Character-Wait", SpeedAnimation);
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
}
