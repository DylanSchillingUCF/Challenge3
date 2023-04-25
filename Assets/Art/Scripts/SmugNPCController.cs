using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmugNPCController : MonoBehaviour
{
    public float displayTime = 4.0f;
    public GameObject dialogBox;
    public GameObject fixedDialogBox;
    public GameObject blockerbox1;
    public GameObject blockerbox2;
    public ParticleSystem smokeEffect;
    float timerDisplay;
    Animator animator;
    public AudioClip computerfixsound;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        dialogBox.SetActive(false);
        fixedDialogBox.SetActive(false);
        timerDisplay = -1.0f;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timerDisplay >= 0)
        {
            timerDisplay -= Time.deltaTime;
            if (timerDisplay < 0)
            {
                dialogBox.SetActive(false);
                fixedDialogBox.SetActive(false);
            }
        }
    }

    public void DisplayDialog()
    {
        timerDisplay = displayTime;
        dialogBox.SetActive(true);
    }
    public void DisplayFixedDialog()
    {
        timerDisplay = displayTime;
        fixedDialogBox.SetActive(true);
    }

    public void Fix()
    {
        //optional if you added the fixed animation
        animator.SetTrigger("Fixed");
        blockerbox1.SetActive(false);
        blockerbox2.SetActive(false);
        smokeEffect.Stop();
        PlaySound(computerfixsound);
    }
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
