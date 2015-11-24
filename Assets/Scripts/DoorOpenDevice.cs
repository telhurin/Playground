using UnityEngine;
using System.Collections;
 
public class DoorOpenDevice : MonoBehaviour
{
    [SerializeField]
    private Vector3 dPos;
    [SerializeField]
    private DoorsType doorsType = DoorsType.manual;
    private Vector3 startPosition;

    private bool canOpen;
    private bool isLerping;
    private float lerpingPercent;


    void Start()
    {
        startPosition = transform.position;
        lerpingPercent = 0f;
        canOpen = true;
        isLerping = false;
    }

    public void Operate()
    {
        if (doorsType == DoorsType.manual)
        {
            isLerping = true;
            canOpen = !canOpen;
        }
    }

    public void Activate()
    {
        isLerping = true;
        canOpen = false;
        Debug.Log(isLerping);
        Debug.Log(canOpen);
    }

    public void Deactivate()
    {
        isLerping = true;
        canOpen = true;
    }

    void FixedUpdate()
    {

        if (isLerping && !canOpen)
        {
            lerpingPercent += 0.025f;
            transform.position = Vector3.Lerp(startPosition, startPosition + dPos, lerpingPercent);
            Debug.Log(lerpingPercent);
        }

        if (isLerping && canOpen)
        {
            lerpingPercent -= 0.025f;
            transform.position = Vector3.Lerp(startPosition + dPos, startPosition, 1 - lerpingPercent);
        }

        if (lerpingPercent >= 1 || lerpingPercent < 0)
        {
            isLerping = false;
        }
    }

    public enum DoorsType { automatic, manual };
}