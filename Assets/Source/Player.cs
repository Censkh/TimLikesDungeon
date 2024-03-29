﻿using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

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
        var sword = GetComponentInChildren<Sword>();
        sword.OwningObject = gameObject;
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

    public void SwingSword(List<Side> sides)
    {
        if (sides.Count == 0) return;
        var sword = GetComponentInChildren<Sword>();
        sword.CurrentSides = sides;
        if (sword.IsResting()) sword.Jab();
    }

}
