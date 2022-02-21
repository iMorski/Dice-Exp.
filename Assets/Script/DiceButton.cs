using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DiceButton : MonoBehaviour
{
    [SerializeField] private Image Arrow;
    [SerializeField] private float HoldDuration;
    [SerializeField] private float FillSpeed;
    [SerializeField] private Touch Touch;
    
    public delegate void OnRoll();
    public event OnRoll Roll;
    
    private Animator Animator;

    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    private void Start()
    {
        Touch.TapBegin += OnTapBegin;
        Touch.TapFinish += OnTapFinish;
    }

    private State StateCurrent;
    private enum State
    {
        None,
        Holding,
        Auto
    }

    private bool Active = true;

    public void OnMoveFinish()
    {
        Active = true;

        if (!(StateCurrent != State.Auto))
        {
            Roll?.Invoke();

            Active = false;
        }
    }

    private void OnTapBegin()
    {
        Animator.Play("Dice-Push-In");

        if (!(StateCurrent != State.Auto))
        {
            Animator.Play("Dice-Arrow-Fade-Out", 1);
        
            StateCurrent = State.None;
        }
        else
        {
            StartCoroutine(Hold());
        }
    }

    private void OnTapFinish()
    {
        if (Active)
        {
            Roll?.Invoke();

            Active = false;
        }
        
        Animator.Play("Dice-Push-Out");

        if (!(StateCurrent != State.Holding))
        {
            Animator.Play("Dice-Arrow-Fade-Out", 1);
        
            StateCurrent = State.None;
        }
    
        StopAllCoroutines();
    }

    private IEnumerator Hold()
    {
        yield return new WaitForSeconds(HoldDuration);

        StateCurrent = State.Holding;
    
        Animator.Play("Null", 2);
        Animator.Play("Dice-Arrow-Fade-In", 1);

        float FillAmount = 0.0f;

        while (FillAmount < 1.0f)
        {
            FillAmount = FillAmount + (FillSpeed * Time.deltaTime);
            Arrow.fillAmount = FillAmount;

            yield return new WaitForEndOfFrame();
        }
    
        StateCurrent = State.Auto;
    
        Animator.Play("Dice-Arrow-Spin", 2);
    }

    private void OnDisable()
    {
        Touch.TapBegin -= OnTapBegin;
        Touch.TapFinish -= OnTapFinish;
    }
}
