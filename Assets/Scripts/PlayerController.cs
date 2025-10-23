using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float torqueAmount = 1f;

    InputAction moveAction;
    Rigidbody2D myRigibody2D;
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        myRigibody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
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
}
