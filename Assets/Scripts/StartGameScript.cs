using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameScript : MonoBehaviour
{
    public int sceneToChange;
    public TextMeshProUGUI textToChange;
    public void StartGame()
    {
        textToChange.text = "LOADING...";
        SceneManager.LoadScene(sceneToChange);
    }
}
