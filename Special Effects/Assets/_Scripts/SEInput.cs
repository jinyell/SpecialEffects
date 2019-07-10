using UnityEngine;

public class SEInput : MonoBehaviour
{
    private static SEInput instance;
    public static SEInput Instance { get { return instance; } }

    public delegate void InputEvent();
    public event InputEvent upRegistered;
    public event InputEvent downRegistered;
    public event InputEvent leftRegistered;
    public event InputEvent rightRegistered;

    [SerializeField] private float minSwipeDistX = 100f;
    [SerializeField] private float minSwipeDistY = 100f;
    private Vector3 startPos;
    private bool isSwipe = false;
    private bool isTouch = false;
    private float cooldown = 0f;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
#if !UNITY_EDITOR
        if (Input.touchCount > 0) {
            CheckSwipeDirection();
        } else {
            ResetSwipe();
        }
#endif

#if UNITY_EDITOR
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            isSwipe = true;
            OnLeftSwipe();
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            isSwipe = true;
            OnRightSwipe();
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            isSwipe = true;
            OnUpSwipe();
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            isSwipe = true;
            OnDownSwipe();
        }
        else
        {
            ResetSwipe();
        }
#endif
    }

    private void CheckSwipeDirection()
    {
        Touch touch = Input.touches[0];

        switch (touch.phase)
        {
            case TouchPhase.Began:
                startPos = touch.position;
                break;
            case TouchPhase.Moved:
                isSwipe = true;
                CheckHorizontalSwipe(touch.position.x);
                CheckVerticalSwipe(touch.position.y);
                break;
            case TouchPhase.Ended:
                ResetSwipe();
                break;
        }
    }

    private void CheckHorizontalSwipe(float touchPositionX)
    {
        float swipeDistHorizontal = (new Vector3(touchPositionX, 0, 0) - new Vector3(startPos.x, 0, 0)).magnitude;

        if (swipeDistHorizontal > minSwipeDistX)
        {
            float swipeValue = Mathf.Sign(touchPositionX - startPos.x);
            if (swipeValue > 0 && !isTouch)
            {
                isTouch = true;
                OnRightSwipe();
            }
            else if (swipeValue < 0 && !isTouch)
            {
                isTouch = true;
                OnLeftSwipe();
            }
        }
    }

    private void CheckVerticalSwipe(float touchPositionY)
    {
        float swipeDistVertical = (new Vector3(0, touchPositionY, 0) - new Vector3(0, startPos.y, 0)).magnitude;

        if (swipeDistVertical > minSwipeDistY)
        {
            float swipeValue = Mathf.Sign(touchPositionY - startPos.y);
            if (swipeValue > 0 && !isTouch)
            {
                isTouch = true;
                OnUpSwipe();
            }
            else if (swipeValue < 0 && !isTouch)
            {
                isTouch = true;
                OnDownSwipe();
            }
        }
    }

    private void OnLeftSwipe()
    {
        if (leftRegistered != null)
        {
            leftRegistered();
        }
    }

    private void OnRightSwipe()
    {
        if (rightRegistered != null)
        {
            rightRegistered();
        }
    }

    private void OnUpSwipe()
    {
        if (upRegistered != null)
        {
            upRegistered();
        }
    }

    private void OnDownSwipe()
    {
        if (downRegistered != null)
        {
            downRegistered();
        }
    }

    private void ResetSwipe()
    {
        isSwipe = false;
        isTouch = false;
    }
}
