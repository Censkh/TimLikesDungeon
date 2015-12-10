using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Actor))]
public class Player : NetworkBehaviour
{

    public enum AttackType
    {
        Bullet,Sword
    }

    public AttackType CurrentAttackType = AttackType.Sword;

    void Start()
    {
        if (isLocalPlayer)
        {
            gameObject.AddComponent<PlayerController>();
        }
    }

    void Update()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, Mathf.Lerp(spriteRenderer.color.a, 1f, 6f * Time.deltaTime));
    }

    public void Fade()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0f);
    }

    public void Shoot(Vector2 dir)
    {
        var bulletObject = Instantiate(Resources.Load<GameObject>("Bullet_P"));
        var bullet = bulletObject.GetComponent<Bullet>();
        var rigidbody = GetComponent<Rigidbody2D>();
        bullet.OwningObject = gameObject;
        bullet.Velocity = (rigidbody.velocity.normalized*0.5f) + (dir * 3f);
        bullet.transform.position = transform.position;
    }

    public Sword SwingSword(Side side)
    {
        var swordObject = Instantiate(Resources.Load<GameObject>("Sword_P"));
        var sword = swordObject.GetComponent<Sword>();
        sword.OwningObject = gameObject;
        if (side == Side.Right || side == Side.Left) sword.transform.Rotate(new Vector3(0, 0, 90));
        if (side == Side.Right || side == Side.Down) sword.transform.localScale = new Vector3(1, -1, 1);
        sword.transform.position = transform.position + (sword.transform.TransformVector(Vector3.up) * 0.3f);
        sword.transform.parent = transform;
        return sword;
    }

}
