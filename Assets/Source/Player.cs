using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{

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

}
