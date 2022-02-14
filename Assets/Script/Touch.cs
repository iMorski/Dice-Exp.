using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Touch : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private float TapDistance = 15.0f;

    public delegate void OnTapBegin();
    public event OnTapBegin TapBegin;

    public delegate void OnTap();
    public event OnTap Tap;

    public delegate void OnTapFinish();
    public event OnTapFinish TapFinish;

    public delegate void OnSwipeByDistance(Vector2 Direction);
    public event OnSwipeByDistance SwipeByDistance;

    public delegate void OnSwipeByRelease(Vector2 Direction);
    public event OnSwipeByRelease SwipeByRelease;

    private Vector2 BeginMousePosition;

    private bool TapCheck;

    public void OnPointerDown(PointerEventData Data)
    {
        if (!OnScreen(Data)) return;

        TapCheck = !TapCheck;
    
        TapBegin?.Invoke();

        BeginMousePosition = MousePosition(Data);
    }

    private bool OnSwipeCheck;

    public void OnDrag(PointerEventData Data)
    {
        if (!OnSwipeCheck && !DragInTapDistance(Data))
        {
            OnSwipeCheck = !OnSwipeCheck;
        
            SwipeByDistance?.Invoke(GetDirection(Data));
        }
    }

    public void OnPointerUp(PointerEventData Data)
    {
        if (TapCheck)
        {
            TapCheck = !TapCheck;
        
            TapFinish?.Invoke();

            if (DragInTapDistance(Data))
            {
                Tap?.Invoke();
            }
        }
    
        if (!DragInTapDistance(Data)) SwipeByRelease?.Invoke(GetDirection(Data));
        if (OnSwipeCheck) OnSwipeCheck = !OnSwipeCheck;
    }

    private Vector2 GetDirection(PointerEventData Data)
    {
        var Direction = MousePosition(Data) - BeginMousePosition;
        
        var MaxValue = Math.Max(
            Math.Abs(Direction.x), Math.Abs(Direction.y));
        
        var DirectionInCircle = Vector2.ClampMagnitude(Direction, MaxValue);
        var DirectionNormalized = new Vector2(DirectionInCircle.x / MaxValue,
            DirectionInCircle.y / MaxValue);

        return DirectionNormalized;
    }

    private bool DragInTapDistance(PointerEventData Data)
    {
        return Vector2.Distance(MousePosition(Data),
            BeginMousePosition) < TapDistance;
    }

    private bool OnScreen(PointerEventData Data)
    {
        return Data.hovered.Contains(gameObject);
    }

    private Vector2 MousePosition(PointerEventData Data)
    {
        return Data.position;
    }
}
