using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    private float health;

    public float GetHealth
    {
        get { return health; }
    }

    private void Awake()
    {
        health = maxHealth;
    }

    public void Subtract(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Destroy(gameObject); 
        }
    }
}
