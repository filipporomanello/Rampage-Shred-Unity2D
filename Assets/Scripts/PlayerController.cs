using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float torqueAmount = 1f;
    [SerializeField] float basespeed = 10f;
    [SerializeField] float boostSpeed = 20f;
    
    InputAction moveAction;
    Rigidbody2D myRigibody2D;
    SurfaceEffector2D surfaceEffector2D;
    bool canControlPlayer = true;
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        myRigibody2D = GetComponent<Rigidbody2D>();
        surfaceEffector2D = FindFirstObjectByType<SurfaceEffector2D>();
    }

    void Update()
    {
        if (canControlPlayer)
        {
            RotatePlayer();
            BoostPlayer();
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
    public void DisableControls()
    {
        canControlPlayer = false;
    }
}
