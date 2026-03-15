using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform player;

    private float speed;

    private void Start()
    {
        var start_position = new Vector3(player.position.x, player.position.y, transform.position.z);
        transform.position = start_position;
    }

    private void Update()
    {
        speed = Vector2.Distance(player.position, transform.position);

        transform.position = Vector3.Lerp(transform.position, player.position + new Vector3(0, 0, -8), speed * Time.deltaTime);

    }
}
