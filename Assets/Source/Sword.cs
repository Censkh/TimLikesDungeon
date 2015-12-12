using UnityEngine;
using System.Collections.Generic;

public class Sword : Damager
{

    public List<Side> CurrentSides = new List<Side>() { Side.Top };
    public float jabTime;

    void Update()
    {
        transform.localEulerAngles = GetCurrentAngle();
        jabTime -= Time.deltaTime;
        transform.localPosition = Vector3.Lerp(transform.localPosition, transform.TransformDirection(new Vector3(0, jabTime < 0 ? -0.25f : -0.55f, 0)), 12f * Time.deltaTime);
    }

    public bool IsSwinging()
    {
        return jabTime > 0;
    }

    public Vector3 GetCurrentAngle()
    {
        bool setAngle = false;
        Vector3 totalAngle = Vector3.zero;
        foreach (var side in CurrentSides)
        {
            Vector3 angle = Vector3.zero;
            switch (side)
            {
                case Side.Down: angle = new Vector3(0, 0, 360); break;
                case Side.Top: angle = new Vector3(0, 0, 180); break;
                case Side.Left: angle = new Vector3(0, 0, 270); break;
                case Side.Right: angle = new Vector3(0, 0, 90); break;
            }
            if (!setAngle) { totalAngle = angle; setAngle = true; }
            else { totalAngle = Vector3.Lerp(totalAngle, angle, 0.5f); }
        }
        return totalAngle;
    }

    public bool IsResting()
    {
        return !IsSwinging() && Vector3.Distance(transform.localPosition, Vector3.zero) < 0.35f;
    }

    public void Jab()
    {
        jabTime = 0.3f;
    }
}
