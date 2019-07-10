using UnityEngine;

public class SmokeController : MonoBehaviour
{
    [SerializeField] private GameObject[] smokes;

    private GameObject current;
    private int index = 0;
    
    private void SetSmoke(bool left)
    {
        current.SetActive(false);

        if(left == true) {
            index = (index - 1 < 0) ? smokes.Length - 1 : --index;
        } else {
            index = (index + 1 >= smokes.Length) ? 0 : ++index;
        }
        
        current = smokes[index];
        current.SetActive(true);
    }

    private void Start()
    {
        current = smokes[index];
        SEInput.Instance.leftRegistered += Instance_leftRegistered;
        SEInput.Instance.rightRegistered += Instance_rightRegistered;
    }
    private void Instance_rightRegistered()
    {
        SetSmoke(false);
    }

    private void Instance_leftRegistered()
    {
        SetSmoke(true);
    }

    private void OnDestroy()
    {
        SEInput.Instance.leftRegistered -= Instance_leftRegistered;
        SEInput.Instance.rightRegistered -= Instance_rightRegistered;
    }
}
