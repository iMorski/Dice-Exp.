using System.Collections;
using UnityEngine;

public class PlaneManager : MonoBehaviour
{
    [SerializeField] private float Duration;

    public IEnumerator Move(Vector3 Position)
    {
        Vector3 Velocity = new Vector3();
        
        while (transform.position != Position)
        {
            transform.position = Vector3.SmoothDamp(transform.position, Position, ref Velocity, Duration);
            
            yield return new WaitForEndOfFrame();
        }
    }
}
