using UnityEngine;

public class Room : MonoBehaviour
{

    private bool active = true;

    void Start()
    {
        if (!Game.Instance.CurrentRoom.Equals(this)) Deactivate();
    }

    void Update()
    {
        foreach (var spriteRenderer in gameObject.GetComponentsInChildren<SpriteRenderer>())
        {
            var alpha = spriteRenderer.color.a;
            alpha = Mathf.Lerp(alpha, active ? 1f : 0f, 5f * Time.deltaTime);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha);
            if (alpha < 0.01f && !active)
            {
                gameObject.SetActive(false);
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
        foreach(var collider in transform.GetComponentsInChildren<Collider2D>())
        {
            
            collider.enabled = true;
        }
        active = true;
    }

}
