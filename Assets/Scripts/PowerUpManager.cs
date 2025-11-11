using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    [SerializeField] PowerUpSO powerUp;
    PlayerController player;
    SpriteRenderer spriteRenderer;
    float timeLeft;
    void Start()
    {
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
            spriteRenderer.enabled = false;
            player.ActivatePowerUp(powerUp);
        }
    }
}
