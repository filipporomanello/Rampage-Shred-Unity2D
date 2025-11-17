using UnityEngine;

[CreateAssetMenu(fileName = "PowerUp", menuName = "PowerUpSO")]
public class PowerUpSO : ScriptableObject
{
    [SerializeField] string powerUpType;
    [SerializeField] float valueChange;
    [SerializeField] float time;

    public string GetPowerUpType()
    {
        return powerUpType;
    }

    public float GetValueChange()
    {
        return valueChange;
    }

    public float GetTime()
    {
        return time;
    }
}
