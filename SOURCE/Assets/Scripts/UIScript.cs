using TMPro;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    public GameManagerScript gameManager;

    public TextMeshProUGUI textObj;

    private void LateUpdate()
    {
        textObj.text = "Courses to Complete: " + (gameManager.maxModules - gameManager.modulesPassed) + "\nAttempts Left: " + gameManager.attemptsLeft;
    }
}
