using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HitboxComponent : MonoBehaviour
{
    [SerializeField] private HealthComponent healthComponent;
    [SerializeField] private InvincibilityComponent invincibilityComponent;

    private void Awake()
    {
        if (healthComponent == null)
        {
            healthComponent = GetComponent<HealthComponent>();
        }

        if (healthComponent == null)
        {
            Debug.LogError("HitboxComponent requires a HealthComponent!");
        }

        if(invincibilityComponent == null)
        {
            invincibilityComponent = GetComponent<InvincibilityComponent>();
            Debug.LogWarning("HitboxComponent requires an InvincibilityComponent!");
        }
    }

    public void Damage(Bullet bullet)
    {
        healthComponent.Subtract(bullet.damage);
    }

    public void Damage(int damageAmount)
    {
        healthComponent.Subtract(damageAmount);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if (bullet != null)
            {
                if(invincibilityComponent != null && !invincibilityComponent.isInvincible)
                {
                    Damage(bullet);
                }
            }
        }
    }
}
