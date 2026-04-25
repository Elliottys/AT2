using UnityEngine;

public class BoostBoxScript : MonoBehaviour
{
    public Vector3 newMoveDirection;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerScript>().moveDirection = newMoveDirection;
        }
    }
}
