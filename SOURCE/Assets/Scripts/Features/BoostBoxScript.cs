using UnityEngine;

public class BoostBoxScript : MonoBehaviour
{
    public Vector3 newMoveDirection;
    public float duration;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerScript>().ChangeSpeed(newMoveDirection, duration);
        }
    }
}
