using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    bool doorPresent = false;
    float inputTimer = 0f;
    float bulletTimer = 0f;
    GameObject swordObject;

    void Update()
    {
        inputTimer -= Time.deltaTime;
        bulletTimer -= Time.deltaTime;
        var player = GetComponent<Player>();
        if (player.CurrentAttackType == Player.AttackType.Bullet)
        {
            if (bulletTimer <= 0)
            {
                var shot = false;
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    shot = true;
                    player.Shoot(Vector2.up);
                }
                else if (Input.GetKey(KeyCode.LeftArrow))
                {
                    shot = true;
                    player.Shoot(Vector2.left);
                }
                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    shot = true;
                    player.Shoot(Vector2.right);
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    shot = true;
                    player.Shoot(Vector2.down);
                }
                if (shot)
                {
                    bulletTimer = 0.5f;
                }
            }
        } else
        {
            if (swordObject == null) 
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    swordObject = player.SwingSword(Side.Top).gameObject;
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    swordObject = player.SwingSword(Side.Left).gameObject;
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    swordObject = player.SwingSword(Side.Right).gameObject;
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    swordObject = player.SwingSword(Side.Down).gameObject;
                }
            }
        }
    }

    void FixedUpdate()
    {
        var speed = swordObject==null ? 2.5f : 0.25f;
        var rigidbody = GetComponent<Rigidbody2D>();
        var velocity = inputTimer <= 0 ? new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * speed : Vector2.zero;
        if (rigidbody.velocity.magnitude > speed)
        {
            rigidbody.velocity = rigidbody.velocity.normalized * speed;
        }
        rigidbody.velocity += velocity;
        var pos = rigidbody.position;
        if (!doorPresent)
        {
            var roomPos = Game.Instance.CurrentRoom.transform.position;
            pos.x = Mathf.Lerp(pos.x, Mathf.Max(Mathf.Min(pos.x, 3.75f + roomPos.x), -3.75f + roomPos.x), 0.9999f);
            pos.y = Mathf.Lerp(pos.y, Mathf.Max(Mathf.Min(pos.y, 2.1f + roomPos.y), -1.65f + roomPos.y), 0.9999f);
        }
        rigidbody.position = pos;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity -= collision.relativeVelocity;
        rigidbody.position -= collision.relativeVelocity.normalized * 0.001f;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        doorPresent = true;
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        var ownCollider = GetComponent<BoxCollider2D>();
        var door = collider.gameObject.GetComponent<Door>();
        if (Mathf.Abs(door.Horizontal ? ownCollider.bounds.center.x - collider.bounds.center.x : ownCollider.bounds.center.y - collider.bounds.center.y) < 0.1f)
        {
            Game.Instance.SwitchRoomFromDoor(GetComponent<Player>(),door);
            inputTimer = 0.3f;
            doorPresent = false;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        doorPresent = false;
    }
}
