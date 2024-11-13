using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int level;  // Level of the enemy
    public Sprite enemySprite; // Sprite of the enemy
    private Rigidbody2D rb;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();  // Initialize Rigidbody2D for physics-based movement
        if (rb != null)
        {
            rb.gravityScale = 0;  // Disable gravity for all enemies
        }

        // Set the sprite if available
        if (enemySprite != null)
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.sprite = enemySprite;
            }
        }
    }

    // This method will be overridden by subclasses to implement specific movement logic
    public virtual void Move() { }
}
