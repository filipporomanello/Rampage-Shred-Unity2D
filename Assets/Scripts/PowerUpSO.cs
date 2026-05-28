using UnityEngine;

// Data container for configuring a power-up's type, strength, and duration.
[CreateAssetMenu(fileName = "PowerUp", menuName = "PowerUpSO")]
public class PowerUpSO : ScriptableObject
{
    [SerializeField] string powerUpType;
    [SerializeField] float valueChange;
    [SerializeField] float time;

    public string GetPowerUpType()
    {
        // Power-up category used by the player to apply effects.
        return powerUpType;
    }

    public float GetValueChange()
    {
        // Amount to add or remove when the power-up is active.
        return valueChange;
    }

    public float GetTime()
    {
        // Duration for the power-up effect.
        return time;
    }
}
