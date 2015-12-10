using UnityEngine;
using System.Collections.Generic;

public class Floor : MonoBehaviour {

    public void GenerateFloor(int roomCount)
    {
        ClearFloor();
        int size = 24;
        bool[,] roomPresentMap = new bool[size, size];
        Room[,] roomMap = new Room[size, size];
        int offset = size / 2;
        int deadZones = Random.Range(roomCount / 4, roomCount / 2);
        for (int c = 0; c < deadZones;c++)
        {
            roomPresentMap[Random.Range(0, size), Random.Range(0, size)] = true;
        }
        int currentX = 0, currentY = 0;
        var rooms = new List<Room>();
        rooms.Add(roomMap[0+offset,0+offset] = CreateRoom(0, 0));
        roomPresentMap[0 + offset, 0 + offset] = true;
        var edges = Room.Sides;
        for (int i = 0; i < roomCount;i++)
        {
            Vector2 nextPos = Vector2.zero;
            int newCount = new int[] { 1,1,2,3}[Random.Range(0,4)];
            for (int n = 0; n < newCount; n++)
            {
                bool validRoom = false;
                int attempts = 0;
                Vector2 newPos = Vector2.zero;
                while (!validRoom)
                {
                    if (attempts > 6) break;
                    newPos = new Vector2(currentX, currentY) + edges[Random.Range(0, 4)];
                    var isFree = !roomPresentMap[(int)newPos.x + offset,(int)newPos.y + offset];
                    if (isFree)
                    {
                        validRoom = true;
                    }
                    attempts++;
                }
                if (validRoom)
                {
                    if (n != 0) i++;
                    nextPos = newPos;
                    roomPresentMap[(int)newPos.x + offset, (int)newPos.y + offset] = true;
                    rooms.Add(roomMap[(int)newPos.x + offset, (int)newPos.y + offset] = CreateRoom((int)newPos.x, (int)newPos.y));
                }
            }
            currentX = (int)nextPos.x;
            currentY = (int)nextPos.y;
        }
        for(int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                var room = roomMap[x, y];
                if (room != null)
                {
                    for (int n = 0; n < 4; n++)
                    {
                        var edge = edges[n];
                        int checkX = x + (int) edge.x;
                        int checkY = y + (int) edge.y;
                        if (checkX >= 0 && checkY >= 0 && checkX < size && checkY < size)
                        {
                            var connectedRoom = roomMap[checkX, checkY];
                            if (connectedRoom!=null)
                            {
                                Room.Side side = (Room.Side)n;
                                Room.Side oppositeSide = Room.Side.Down;
                                switch (side)
                                {
                                    case Room.Side.Left: oppositeSide = Room.Side.Right; break;
                                    case Room.Side.Right: oppositeSide = Room.Side.Left; break;
                                    case Room.Side.Top: oppositeSide = Room.Side.Down; break;
                                    case Room.Side.Down: oppositeSide = Room.Side.Top; break;
                                }
                                Door ownDoor = room.GetDoor(side);
                                if (ownDoor == null) ownDoor = room.CreateDoor(side);
                                Door otherDoor = connectedRoom.GetDoor(oppositeSide);
                                if (otherDoor == null) otherDoor = connectedRoom.CreateDoor(oppositeSide);
                                ownDoor.ConnectedDoorObject = otherDoor.gameObject;
                                otherDoor.ConnectedDoorObject = ownDoor.gameObject;
                            }
                        }
                    }
                }
            }
        }
        Game.Instance.CurrentRoomObject = rooms[0].gameObject;
    }

    public Room CreateRoom(int x, int y)
    {
        var roomObject = Instantiate<GameObject>(Resources.Load<GameObject>("Room_P"));
        var room = roomObject.GetComponent<Room>();
        room.transform.position = new Vector3(x*Room.Width,y*Room.Height);
        room.transform.parent = transform;
        room.X = x;
        room.Y = y;
        return room;
    }

    public void ClearFloor()
    {
        int count = transform.childCount;
        for (int i = 0; i < count;i++) {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }
}
