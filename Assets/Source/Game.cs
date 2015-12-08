using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

    public static Game Instance { get { return FindObjectOfType<Game>(); } }
    public GameObject CurrentRoomObject;
    public Room CurrentRoom { get { return CurrentRoomObject == null ? null : CurrentRoomObject.GetComponent<Room>(); } }

    public void SwitchRoomFromDoor(Player player,Door door)
    {
        SwitchRoom(player,door.ConnectedDoor.GetRoom());
    }

    public void SwitchRoom(Player player,Room room)
    {
        var rigidbody = player.GetComponent<Rigidbody2D>();
        rigidbody.velocity = Vector2.zero;
        CurrentRoom.Deactivate();
        CurrentRoomObject = room.gameObject;
        room.Activate();
        player.transform.parent = room.gameObject.transform;
    }
}
