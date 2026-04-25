using UnityEngine;

public class WinRegion : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("win!");
        }
    }
}
