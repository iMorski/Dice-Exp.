using System.Collections;
using UnityEngine;

public class DiceBox : MonoBehaviour
{
    [SerializeField] private Collider Collider;

    public void Set()
    {
        Collider.isTrigger = true;
    }
    
    private void OnTriggerEnter(Collider Other)
    {
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.25f);
        
        Collider.isTrigger = false;
    }
}
