using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CameraController : MonoBehaviour
{

    public float CameraScale = 2.0f;
    float modificatoreSchermo, lastTargetResolutionHeight, currentPixelsToUnits;

    void Update()
    {
        if (Application.isPlaying)
        {
            var pos = Game.Instance.CurrentRoomObject.transform.position;
            pos.z = transform.position.z;
            transform.position = Vector3.Lerp(transform.position, pos, 5f * Time.deltaTime);

        }

        modificatoreSchermo = 1f;
        lastTargetResolutionHeight = Screen.height;
        currentPixelsToUnits = 48;
        if (lastTargetResolutionHeight < 500)
        {
            modificatoreSchermo = 1.5f;
        }
        else if (lastTargetResolutionHeight >= 500 && lastTargetResolutionHeight <= 650)
        {
            modificatoreSchermo = 2f;
        } else if (lastTargetResolutionHeight>=650 && lastTargetResolutionHeight <= 770)
        {
            modificatoreSchermo = 2.5f;
        }
        else if (lastTargetResolutionHeight >= 770)
        {
            modificatoreSchermo = 3f;
        }
        Camera.main.orthographicSize = (1.0f * lastTargetResolutionHeight) / 2f / currentPixelsToUnits / modificatoreSchermo * CameraScale;

    }
}
