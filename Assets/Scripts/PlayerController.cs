using UnityEngine;
using UnityEngine.InputSystem;

// Controls player movement, boosts, flips scoring, and power-up effects.
public class PlayerController : MonoBehaviour
{
    [SerializeField] float torqueAmount = 170f;
    [SerializeField] float baseSpeed = 10f;
    [SerializeField] float boostSpeed = 18f;
    [SerializeField] ParticleSystem powerupParticles;
    [SerializeField] ScoreManager scoreManager;

    const float FlipThreshold = 340f;
    const int FlipScore = 100;

    InputAction moveAction;
    Rigidbody2D myRigidbody2D;
    SurfaceEffector2D surfaceEffector2D;
    bool canControlPlayer = true;
    float previousRotation;
    float totalRotations;
    int activePowerupCount;

    void Awake()
    {
        // Keep gameplay feel consistent across machines.
        Application.targetFrameRate = 60;
    }

    void Start()
    {
        // Resolve input and physics dependencies once.
        moveAction = InputSystem.actions.FindAction("Move");
        myRigidbody2D = GetComponent<Rigidbody2D>();
        surfaceEffector2D = FindFirstObjectByType<SurfaceEffector2D>();
    }

    void Update()
    {
        if (canControlPlayer)
        {
            // Apply speed changes and check for flip scoring.
            BoostPlayer();
            CalculateFlips();
        }
    }

    void FixedUpdate()
    {
        if (canControlPlayer)
        {
            // Apply rotation in the physics step.
            RotatePlayer();
        }
    }

    void RotatePlayer()
    {
        Vector2 moveVector = moveAction.ReadValue<Vector2>();

        // Apply torque based on horizontal input.
        if (moveVector.x < -0.1f)
        {
            myRigidbody2D.AddTorque(torqueAmount * Time.fixedDeltaTime, ForceMode2D.Force);
        }
        else if (moveVector.x > 0.1f)
        {
            myRigidbody2D.AddTorque(-torqueAmount * Time.fixedDeltaTime, ForceMode2D.Force);
        }
    }


    void BoostPlayer()
    {
        // Increase speed while the player holds the boost direction.
        Vector2 moveVector = moveAction.ReadValue<Vector2>();
        surfaceEffector2D.speed = (moveVector.y > 0) ? boostSpeed : baseSpeed;
    }

    void CalculateFlips()
    {
        // Accumulate rotation and award points for full flips.
        float currentRotation = transform.rotation.eulerAngles.z;
        totalRotations += Mathf.DeltaAngle(previousRotation, currentRotation);

        if (totalRotations > FlipThreshold || totalRotations < -FlipThreshold)
        {
            totalRotations = 0f;
            scoreManager.AddScore(FlipScore);
        }

        previousRotation = currentRotation;
    }

    public void DisableControls()
    {
        // Used by crash detection to stop player input.
        canControlPlayer = false;
    }

    public void ActivatePowerUp(PowerUpSO powerUp)
    {
        // Support stacking power-ups before turning off the effect.
        powerupParticles.Play();
        activePowerupCount++;

        if (powerUp.GetPowerUpType() == "speed")
        {
            baseSpeed += powerUp.GetValueChange();
            boostSpeed += powerUp.GetValueChange();
        }
        else if (powerUp.GetPowerUpType() == "torque")
        {
            torqueAmount += powerUp.GetValueChange();
        }
    }

    public void DeactivatePowerUp(PowerUpSO powerUp)
    {
        activePowerupCount--;
        if (activePowerupCount <= 0)
            powerupParticles.Stop();

        // Reverse the same adjustment applied when activating.
        if (powerUp.GetPowerUpType() == "speed")
        {
            baseSpeed -= powerUp.GetValueChange();
            boostSpeed -= powerUp.GetValueChange();
        }
        else if (powerUp.GetPowerUpType() == "torque")
        {
            torqueAmount -= powerUp.GetValueChange();
        }
    }
}
