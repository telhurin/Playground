using UnityEngine;
 
public class DeviceTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject[] targets;
    public bool requireKey;
    public bool isActive;

    void Start()
    {
        GetComponent<Renderer>().enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            foreach (GameObject target in targets)
            {
                target.SendMessage("Activate");
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isActive)
        {
            if (requireKey && Managers.Inventory.equippedItem != "key")
            {
                return;
            }
            foreach (GameObject target in targets)
            {
                target.GetComponent<DoorOpenDevice>().Activate();
            }
            isActive = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (isActive)
        {
            foreach (GameObject target in targets)
            {
                target.SendMessage("Deactivate");
            }
            isActive = false;
        }
    }
}