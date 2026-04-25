using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public PlayerScript playerScript;

    public GameObject[] levelsEasy;
    public GameObject[] levelsMed;
    public GameObject[] levelsHard;

    public AudioClip[] bgmEasy;
    public AudioClip[] bgmMed;
    public AudioClip[] bgmHard;

    public AudioSource bgm;

    public int modulesPassed = 0;

    public int maxModules;
    public int attemptsLeft;

    private GameObject spawn;
    private GameObject goal;

    public float secondsIncreaseDifficulty;
    public float maxDifficulty;
    public float difficultyMultiplierMinimum;

    private GameObject currentModule;
    private float difficulty = 0.0f;

    private void Start()
    {
        GenerateLevelModule();
    }

    private GameObject GetLevelModule(float chance)
    {
        float moduleDifficulty = UnityEngine.Random.Range(modulesPassed / maxModules * difficultyMultiplierMinimum, chance);

        if (moduleDifficulty < 1.0f)
        {
            bgm.clip = bgmEasy[UnityEngine.Random.Range(0, bgmEasy.Length)];
            return levelsEasy[UnityEngine.Random.Range(0, levelsEasy.Length)];
        }
        else if (moduleDifficulty < 2.0f)
        {
            bgm.clip = bgmMed[UnityEngine.Random.Range(0, bgmMed.Length)];
            return levelsMed[UnityEngine.Random.Range(0, levelsMed.Length)];
        }
        bgm.clip = bgmHard[UnityEngine.Random.Range(0, bgmHard.Length)];
        return levelsHard[UnityEngine.Random.Range(0, levelsHard.Length)];
    }
    
    private void GenerateLevelModule()
    {
        GameObject.Destroy(currentModule);

        GameObject moduleToPlace = GetLevelModule(difficulty);

        var module = Instantiate(moduleToPlace);
        module.transform.position = Vector3.zero;

        currentModule = module;
        spawn = moduleToPlace.GetComponent<LevelModuleScript>().start;
        goal = moduleToPlace.GetComponent<LevelModuleScript>().win;

        bgm.Play();
        playerScript.Spawn(spawn.transform.position);
    }

    private void FixedUpdate()
    {
        if (playerScript.GetState() == 2)
        {
            modulesPassed++;

            if (modulesPassed > maxModules)
            {
                SceneManager.LoadScene(2);
            }
            else
            {
                playerScript.SetState(0);
                GenerateLevelModule();
            }
        }

        if (playerScript.GetState() < 0)
        {
            attemptsLeft--;
            if (attemptsLeft < 0)
            {
                SceneManager.LoadScene(3);
            }
            else
            {
                playerScript.SetState(0);
            }
        }
    }
    
    private void Update()
    {
        difficulty = Mathf.Clamp(difficulty + (Time.deltaTime / secondsIncreaseDifficulty), 0, maxDifficulty);
    }

}
