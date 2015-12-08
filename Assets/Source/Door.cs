using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

    public GameObject ConnectedDoorObject;
    public Door ConnectedDoor { get { return ConnectedDoorObject == null ? null : ConnectedDoorObject.GetComponent<Door>(); } }
    private Room room;
    public bool Horizontal;

    void Awake()
    {
        room = GetComponentInParent<Room>();
    }

    public Room GetRoom()
    {
        return room;
    }

}
