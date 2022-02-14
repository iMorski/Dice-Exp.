using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class DiceManager : MonoBehaviour
{
    [SerializeField] private Transform Dice;
    [SerializeField] private Transform DicePointStart;
    [SerializeField] private Transform DicePointFinish;
    [SerializeField] private float Force;
    [SerializeField] private float Torque;

    public delegate void OnRoll(int Number);
    public event OnRoll OnRollFinish;

    public void Roll()
    {
        Rigidbody Rigidbody = Dice.GetComponent<Rigidbody>();

        Rigidbody.velocity = new Vector3(0.0f, 0.0f, 0.0f);
        Rigidbody.useGravity = true;

        Dice.position = DicePointStart.position;
        
        Rigidbody.AddForce((DicePointFinish.position - DicePointStart.position) * Force, ForceMode.Impulse);
        Rigidbody.AddTorque(new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f),
            Random.Range(-1.0f, 1.0f)) * Torque);

        StartCoroutine(WaitForNumber(Rigidbody));
    }

    private IEnumerator WaitForNumber(Rigidbody Rigidbody)
    {
        yield return new WaitForSeconds(0.5f);
        
        while (Rigidbody.velocity.magnitude > 0.05f)
        {
            yield return new WaitForFixedUpdate();
        }

        Transform[] SideGroup = Dice.GetComponent<Dice>().SideGroup;
        Transform UpperSide = SideGroup[0];

        for (int i = 1; i < SideGroup.Length; i++)
        {
            if (SideGroup[i].position.y > UpperSide.position.y) UpperSide = SideGroup[i];
        }
        
        OnRollFinish?.Invoke(UpperSide.GetComponent<DiceSide>().Number);
        
        Debug.Log(UpperSide.GetComponent<DiceSide>().Number);
    }
}
