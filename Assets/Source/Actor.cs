using UnityEngine;
using System.Collections;

public class Actor : MonoBehaviour {

    [SerializeField]
    private HealthContainer healthContainer = new HealthContainer();

    public void Damage(int amount)
    {
        healthContainer.Damage(amount);
    }

    public void Heal(int amount)
    {
        healthContainer.Heal(amount);
    }

    public int GetCurrentHealth()
    {
        return healthContainer.Health;
    }

    public int GetMaxHealth()
    {
        return healthContainer.MaxHealth;
    }
}
