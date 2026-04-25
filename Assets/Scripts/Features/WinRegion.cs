using UnityEngine;

public class WinRegion : MonoBehaviour
{
    public float secondsToWait;

    private bool hit = false;
    private float timer = 0.0f;

    private GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (hit)
        {
            return;
        }
        if (other.CompareTag("Player"))
        {
            hit = true;
            timer = secondsToWait;
            player = other.gameObject;

            player.GetComponent<PlayerScript>().SetWinState(1);
        }
    }

    private void Update()
    {
        if (timer > 0.0f)
        {
            timer = Mathf.Clamp(timer - Time.deltaTime, 0.0f, secondsToWait);

            if (timer == 0.0f)
            {
                player.GetComponent<PlayerScript>().SetWinState(2);
            }
        }
    }
}
