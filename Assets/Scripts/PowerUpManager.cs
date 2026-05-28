using UnityEngine;

// Handles pickup, activation, and timed deactivation of a power-up.
public class PowerUpManager : MonoBehaviour
{
    [SerializeField] PowerUpSO powerUp;

    PlayerController player;
    SpriteRenderer spriteRenderer;
    float timeLeft;

    void Start()
    {
        // Wire up references and initialize the timer from the configured asset.
        player = FindFirstObjectByType<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        timeLeft = powerUp.GetTime();
    }

    void Update()
    {
        CountdownTimer();
    }

    void CountdownTimer()
    {
        // Count down only after the pickup has been collected (sprite hidden).
        if (spriteRenderer.enabled == false)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                if (timeLeft <= 0)
                {
                    player.DeactivatePowerUp(powerUp);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        int layerIndex = LayerMask.NameToLayer("Player");

        if (collision.gameObject.layer == layerIndex && spriteRenderer.enabled == true)
        {
            // Hide the pickup and apply the configured effect.
            spriteRenderer.enabled = false;
            player.ActivatePowerUp(powerUp);
        }
    }
}
