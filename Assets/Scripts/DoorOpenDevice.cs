using UnityEngine;
using System.Collections;
 
public class DoorOpenDevice : MonoBehaviour
{
    [SerializeField]
    private Vector3 dPos;
    [SerializeField]
    private DoorsType doorsType = DoorsType.manual;
    [SerializeField]
    private AudioClip doorSound;

    private Vector3 startPosition;
    private AudioSource audioSource;

    private bool canOpen;
    private bool isLerping;
    private float lerpingPercent;


    void Start()
    {
        startPosition = transform.position;
        lerpingPercent = 0f;
        canOpen = true;
        isLerping = false;

        audioSource = GetComponent<AudioSource>();
    }

    public void Operate()
    {
        if (doorsType == DoorsType.manual)
        {
            isLerping = true;
            canOpen = !canOpen;
            audioSource.PlayOneShot(doorSound);
        }
    }

    public void Activate()
    {
        isLerping = true;
        canOpen = false;
        audioSource.PlayOneShot(doorSound);
    }

    public void Deactivate()
    {
        isLerping = true;
        canOpen = true;
        audioSource.PlayOneShot(doorSound);
    }

    void FixedUpdate()
    {

        if (isLerping && !canOpen)
        {
            lerpingPercent += 0.025f;
            transform.position = Vector3.Lerp(startPosition, startPosition + dPos, lerpingPercent);
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