using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    [SerializeField] float torqueAmount = 1f;
    [SerializeField] float basespeed = 10f;
    [SerializeField] float boostSpeed = 20f;
    
    InputAction moveAction;
    Rigidbody2D myRigibody2D;
    SurfaceEffector2D surfaceEffector2D;
    ScoreManager scoreManager;
    bool canControlPlayer = true;
    float previusRotation;
    float totalRotations;
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        myRigibody2D = GetComponent<Rigidbody2D>();
        surfaceEffector2D = FindFirstObjectByType<SurfaceEffector2D>();
        scoreManager = FindFirstObjectByType<ScoreManager>();
    }

    void Update()
    {
        if (canControlPlayer)
        {
            RotatePlayer();
            BoostPlayer();
            CalculteFlips();
        }
    }
    void RotatePlayer()
    {
        Vector2 moveVector;
        moveVector = moveAction.ReadValue<Vector2>();
        if (moveVector.x < 0)
        {
            myRigibody2D.AddTorque(torqueAmount);
        }
        else if (moveVector.x > 0)
        {
            myRigibody2D.AddTorque(-torqueAmount);
        }
    }
    void BoostPlayer()
    {
        if (moveAction.ReadValue<Vector2>().y > 0)
        {
            surfaceEffector2D.speed = boostSpeed;
        }
        else
        {
            surfaceEffector2D.speed = basespeed;
        }
    }
    void CalculteFlips()
    {
        float currentRotation = transform.rotation.eulerAngles.z;

        totalRotations += Mathf.DeltaAngle(previusRotation, currentRotation);
        if (totalRotations > 340 || totalRotations < -340)
        {
            totalRotations = 0;
            scoreManager.AddScore(100);
        }

        previusRotation = currentRotation;
    }
    public void DisableControls()
    {
        canControlPlayer = false;
    }
}
