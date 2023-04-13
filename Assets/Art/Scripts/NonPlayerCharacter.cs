using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NonPlayerCharacter : MonoBehaviour
{
    public float displayTime = 4.0f;
    public GameObject dialogBox;
    public GameObject levelCompleteDialogBox;
    float timerDisplay;
    bool levelCompleted = false;

    public Scene currentscene;


    void Start()
    {
        dialogBox.SetActive(false);
        levelCompleteDialogBox.SetActive(false);
        timerDisplay = -1.0f;
        currentscene = SceneManager.GetActiveScene();
    }

    void Update()
    {
        if (timerDisplay >= 0)
        {
            timerDisplay -= Time.deltaTime;
            if (timerDisplay < 0)
            {
                dialogBox.SetActive(false);
                levelCompleteDialogBox.SetActive(false);
                if (levelCompleted)
                {
                    SceneManager.LoadScene("main3");
                }
            }
        }
    }

    public void DisplayDialog()
    {
        timerDisplay = displayTime;
        dialogBox.SetActive(true);
    }

    public void DisplayLevelCompleteDialog()
    {
        timerDisplay = displayTime;
        levelCompleteDialogBox.SetActive(true);
        levelCompleted = true;
    }
}