using TMPro;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    public GameManagerScript gameManager;

    public TextMeshProUGUI textObj;

    private void Update()
    {
        textObj.text = gameManager.modulesPassed + " / " + gameManager.maxModules + " Modules";
    }
}
