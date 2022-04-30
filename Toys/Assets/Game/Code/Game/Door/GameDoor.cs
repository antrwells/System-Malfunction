using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDoor : MonoBehaviour
{
    public enum DoorType
    {
        Sliding,Pivot
    }

    public GameObject door;
    public DoorType type;
    public bool open = false;
    public float OpenAmount = 0;
    private float OpenTarget = 0;
    public float OpenValue;
    private float openA = 0;
    private Vector3 basePosition;
    

    public void Use()
    {
        if (open)
        {
            open = false;
            OpenTarget = 0;
        }
        else
        {
            open = true;
            OpenTarget = OpenValue;
        }
        Debug.Log("Open Door." + open);

    }

    // Start is called before the first frame update
    void Start()
    {
        basePosition = transform.position;
        openA = OpenAmount;   
    }

    // Update is called once per frame
    void Update()
    {
      
        OpenAmount = Mathf.Lerp(OpenAmount, OpenTarget, Time.deltaTime * 5.5f);

        if (type == DoorType.Sliding)
        {
            door.transform.position = basePosition + new Vector3(0, OpenAmount, 0);
        }
        else
        {
            door.transform.localRotation = Quaternion.Euler(0, OpenAmount, 0);
        }

    }
}
