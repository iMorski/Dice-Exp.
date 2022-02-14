using UnityEngine;

public class Point : MonoBehaviour
{
    [SerializeField] private Animator Animator;

    public void Down()
    {
        Animator.Play("Point-Down");
    }
}
