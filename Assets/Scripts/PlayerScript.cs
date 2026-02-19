using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public enum PlayerActions { None, Dead, Grounded, Walking, Running, InAir, Jumping}
    public List<PlayerActions> currentActions;

    
    public bool tryingJump = false;

    [SerializeField] private float yVelocity = 0.0f;
    [SerializeField] private float gravity = 16.0f;
    [SerializeField] private float jumpHeight = 8.0f;

    public void JumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            tryingJump = true;
        }

        if (context.phase == InputActionPhase.Canceled)
        {
            tryingJump = false;
        }
    }

    private void JumpAction()
    {
        if (currentActions.Contains(PlayerActions.Jumping))
        {
            
        }
    }
    
    private void InAirAction()
    {
        Vector3 pos = transform.position;

        // Replace this with an actual check for being grounded
        if (transform.position.y < 0)
        {
            //currentActions.RemoveAll(PlayerActions.InAir);
            yVelocity = 0.0f;
            transform.position = new Vector3(pos.x, 0, pos.z);
        }

        // should be "if not grounded"
        if (yVelocity != 0)
        {
            yVelocity -= gravity * Time.deltaTime;
            transform.position = new Vector3(pos.x, pos.y + yVelocity * Time.deltaTime, pos.z);
        }
    }

    private void Update()
    {
        Vector3 pos = transform.position;

        // should be "if grounded"
        if (transform.position.y < 0)
        {
            tryingJump = true;
            yVelocity = 0.0f;
            transform.position = new Vector3(pos.x, 0, pos.z);
        }

        // should be "if not grounded"
        if (yVelocity != 0)
        {
            yVelocity -= gravity * Time.deltaTime;
            transform.position = new Vector3(pos.x, pos.y + yVelocity * Time.deltaTime, pos.z);
        }
        
    }

}


