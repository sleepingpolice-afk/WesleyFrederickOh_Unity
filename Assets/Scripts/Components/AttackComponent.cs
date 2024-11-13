using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AttackComponent : MonoBehaviour
{
    [SerializeField] private Bullet bullet;
    [SerializeField] private int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        InvincibilityComponent invincibilityComponent = collision.GetComponent<InvincibilityComponent>();
        
        if (collision.CompareTag(gameObject.tag))
        {
            return;
        }

        HitboxComponent hitbox = collision.GetComponent<HitboxComponent>(); 
        if (hitbox != null && invincibilityComponent != null && !invincibilityComponent.isInvincible)
        {
            hitbox.Damage(damage);
            invincibilityComponent.Flash();
        }
    }
}
