using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    bool doorPresent = false;
    float inputTimer = 0f;

    void Update()
    {
        inputTimer -= Time.deltaTime;
    }

    void FixedUpdate()
    {
        var speed = 3f;
        var rigidbody = GetComponent<Rigidbody2D>();
        var velocity = inputTimer <= 0 ? new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * speed : Vector2.zero;
        rigidbody.velocity = Vector2.Lerp(rigidbody.velocity, velocity, 20f * Time.deltaTime);
        rigidbody.velocity *= 0.9f;
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
