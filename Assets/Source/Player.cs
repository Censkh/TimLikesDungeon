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

}
