using UnityEngine;
using System.Collections;

public class Bullet : Damager
{

    public Vector2 Velocity;

    void Update()
    {
        var rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = Velocity;
        Vector2 roomPos = Game.Instance.CurrentRoomObject.transform.position;
        if (transform.position.x > 3.85f + roomPos.x|| transform.position.x < -3.85f + roomPos.x || transform.position.y > 2.1f + roomPos.y || transform.position.y < -2.1f + roomPos.y)
        {
            Destroy(gameObject);
        }

    }
}
