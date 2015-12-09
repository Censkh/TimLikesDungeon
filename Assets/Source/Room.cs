using UnityEngine;
using System.Collections.Generic;

public class Room : MonoBehaviour
{

    public enum Side
    {
        Left, Right, Top, Down
    }

    public const int Width = 10;
    public const int Height = 6;
    public static Vector2[] Sides = { new Vector2(-1, 0), new Vector2(1, 0), new Vector2(0, 1), new Vector2(0, -1) };

    private bool active = true;
    private List<Door> doors = new List<Door>(new Door[4]);

    void Start()
    {
        if (!Game.Instance.CurrentRoom.Equals(this)) Deactivate();
    }

    void Update()
    {
        foreach (var spriteRenderer in gameObject.GetComponentsInChildren<SpriteRenderer>())
        {
            if (spriteRenderer.GetComponent<Player>() == null)
            {
                var alpha = spriteRenderer.color.a;
                alpha = Mathf.Lerp(alpha, active ? 1f : 0f, (active ? 10f : 5f) * Time.deltaTime);
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha);
                if (alpha < 0.01f && !active)
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }

    public void Deactivate()
    {
        gameObject.SetActive(true);
        foreach (var collider in transform.GetComponentsInChildren<Collider2D>())
        {
            if (collider.GetComponent<Player>() == null)
                collider.enabled = false;
        }
        active = false;
    }

    public void Activate()
    {
        gameObject.SetActive(true);
        foreach (var collider in transform.GetComponentsInChildren<Collider2D>())
        {

            collider.enabled = true;
        }
        active = true;
    }

    public Door GetDoor(Side side)
    {
        return doors[(int)side];
    }

    public Door CreateDoor(Side side)
    {
        GameObject doorObject = Instantiate(Resources.Load<GameObject>("Door_P"));
        Door door = doorObject.GetComponent<Door>();
        door.transform.parent = transform;
        doors[(int)side] = door;
        var offset = 0.69f;
        door.transform.localPosition = new Vector2(Sides[(int)side].x * ((Width/2f)- offset), Sides[(int)side].y * ((Height / 2f) - offset));
        if (side==Side.Right||side==Side.Left)
        {
            door.transform.Rotate(new Vector3(0, 0, 90));
            door.Horizontal = true;
        }
        if (side == Side.Right || side == Side.Down) door.transform.localScale = new Vector3(1, -1, 1);
        door.CurrentSide = side;
        return door;
    }

}
