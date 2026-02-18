using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public bool canJump = true;
    public float yVelocity = 0.0f;

    private void OnJump()
    {
        if (canJump)
        {
            canJump = false;
            yVelocity = 8.0f;
        }
    }

    private void Update()
    {
        Vector3 pos = transform.position;

        if (yVelocity != 0)
        {
            yVelocity -= 16.0f * Time.deltaTime;
            transform.position = new Vector3(pos.x, pos.y + yVelocity * Time.deltaTime, pos.z);
        }
        
		// should be "if on ground"
        if (transform.position.y < 0)
        {
            canJump = true;
            yVelocity = 0.0f;
            transform.position = new Vector3(pos.x, 0, pos.z);
        }
    }

}
