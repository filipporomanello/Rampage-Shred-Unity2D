using UnityEngine;
using UnityEngine.InputSystem;

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
        Application.targetFrameRate = 60;
    }

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        myRigidbody2D = GetComponent<Rigidbody2D>();
        surfaceEffector2D = FindFirstObjectByType<SurfaceEffector2D>();
    }

    void Update()
    {
        if (canControlPlayer)
        {
            BoostPlayer();
            CalculateFlips();
        }
    }

    void FixedUpdate()
    {
        if (canControlPlayer)
        {
            RotatePlayer();
        }
    }

    void RotatePlayer()
    {
        Vector2 moveVector = moveAction.ReadValue<Vector2>();

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
        Vector2 moveVector = moveAction.ReadValue<Vector2>();
        surfaceEffector2D.speed = (moveVector.y > 0) ? boostSpeed : baseSpeed;
    }

    void CalculateFlips()
    {
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
        canControlPlayer = false;
    }

    public void ActivatePowerUp(PowerUpSO powerUp)
    {
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
