using UnityEngine;

public class HurtBoxScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerScript>().SetState(-1);
            other.GetComponent<PlayerScript>().Respawn();
        }
    }
}
