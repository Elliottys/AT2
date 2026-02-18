using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public bool jumping = false;
    public float yVelocity = 0.0f;

    private void OnJump()
    {
        if (!jumping)
        {
            jumping = true;
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
        
        if (transform.position.y < 0)
        {
            jumping = false;
            yVelocity = 0.0f;
            transform.position = new Vector3(pos.x, 0, pos.z);
        }
    }

}
