using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

    public GameObject ConnectedDoorObject;
    public Door ConnectedDoor { get { return ConnectedDoorObject == null ? null : ConnectedDoorObject.GetComponent<Door>(); } }
    public Side CurrentSide;
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

    void OnDrawGizmos()
    {
        if (ConnectedDoorObject != null && GetInstanceID()>ConnectedDoorObject.GetInstanceID())
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, ConnectedDoorObject.transform.position);
            Gizmos.DrawCube(ConnectedDoorObject.transform.position, Vector3.one * 0.1f);
        }
    }

}
