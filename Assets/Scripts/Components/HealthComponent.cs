using UnityEngine;
using UnityEngine.Events;

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

    public UnityEvent onDestroyed;

    public void OnDestroy()
    {
        if(onDestroyed != null)
        {
            onDestroyed.Invoke();
            CombatManager combatManager = FindObjectOfType<CombatManager>();
            combatManager.UpdateTotalEnemies();
        }
    }
}
