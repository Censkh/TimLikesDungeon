using UnityEngine;

public    class Damager : MonoBehaviour
    {
    public GameObject OwningObject;

    void OnCollisionEnter2D(Collision2D collision)
    {
        Actor otherActor = collision.collider.GetComponent<Actor>();
        if (otherActor != null && (OwningObject == null || !collision.collider.gameObject.Equals(OwningObject)))
        {
            otherActor.Damage(1);
            Destroy(gameObject);
        }
    }

}