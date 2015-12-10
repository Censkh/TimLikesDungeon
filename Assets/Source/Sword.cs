using UnityEngine;

public class Sword : Damager {

    public float LifetimeTimer = 0.15f;
	
	void Update () {
        LifetimeTimer -= Time.deltaTime;
        if (LifetimeTimer <= 0) Destroy(gameObject);
	}
}
