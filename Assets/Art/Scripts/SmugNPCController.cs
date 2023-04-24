using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEnginge.SceneManagement;

public class SmugNPCController : MonoBehaviour
{
    public float displayTime = 4.0f;
    public GameObject dialogBox;
    public GameObject levelCompleteDialogBox;
    public GameObject blockerbox1;
    public GameObject blockerbox2;
    public ParticleSystem smokeEffect;
    float timerDisplay;
    bool levelCompleted = false;
    bool broken = true;
    Animator animator;

    public Scene currentscene;

    // Start is called before the first frame update
    void Start()
    {
        dialogBox.SetActive(false);
        levelCompleteDialogBox.SetActive(false);
        timerDisplay = -1.0f;
        currentscene = SceneManager.GetActiveScene();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
         
    }

    public void Fix()
    {
        broken = false;
        //optional if you added the fixed animation
        animator.SetTrigger("Fixed");
        blockerbox1.SetActive(false);
        blockerbox2.SetActive(false);
        smokeEffect.Stop();
    }
}
