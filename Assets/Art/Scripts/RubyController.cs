using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class RubyController : MonoBehaviour
{
    public float speed = 3.0f;

    public int maxHealth = 5;

    public int ammoCount;

    public GameObject projectilePrefab;

    public ParticleSystem hurtEffect;

    public AudioClip throwSound;
    public AudioClip hitSound;

    public int health { get { return currentHealth; } }
    int currentHealth;

    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;

    public bool armorActive = false;
    public float armorTime = 6.0f;
    float armorTimeRemaining;

    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;

    int botTotal;
    public GameObject[] bottotals;

    public int botsFixed;
    bool levelComplete;
    bool gameComplete;
    bool failed;
    bool computerFixed = false;

    public TMP_Text countText;
    public TMP_Text ammoText;
    public GameObject victoryText;
    public GameObject failureText;
    public GameObject levelCompleteText;
    public GameObject armorUIIndicator;

    public GameObject BGM;
    public GameObject VictoryTheme;
    public GameObject FailureTheme;

    public Scene currentscene;

    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        
        levelComplete = false;
        gameComplete = false;
        failed = false;

        armorTimeRemaining = -1.0;

        levelCompleteText.SetActive(false);
        victoryText.SetActive(false);
        failureText.SetActive(false);
        armorUIIndicator.SetActive(false);

        currentscene = SceneManager.GetActiveScene();

        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentHealth = maxHealth;

        audioSource = GetComponent<AudioSource>();

        bottotals = GameObject.FindGameObjectsWithTag("Enemy");
        botTotal = bottotals.Length;
        ammoCount = botTotal;

        countText.text = "Robots Fixed: " + botsFixed.ToString() + "/" + botTotal.ToString();
        ammoText.text = "Cog Count: " + ammoCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        if ((!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f)))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        if (!failed)
        {
            animator.SetFloat("Look X", lookDirection.x);
            animator.SetFloat("Look Y", lookDirection.y);
            animator.SetFloat("Speed", move.magnitude);
        }

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }

        if (Input.GetKeyDown(KeyCode.C) && !failed)
        {
            Launch();
        }

        if (Input.GetKeyDown(KeyCode.X) && !failed)
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                SmugNPCController smug = hit.collider.GetComponent<SmugNPCController>();
                if ((character != null) && !levelComplete)
                {
                    character.DisplayDialog();
                }
                if ((character != null) && levelComplete)
                {
                    character.DisplayLevelCompleteDialog();
                }
                if ((smug != null) && !computerFixed)
                {
                    smug.DisplayDialog();
                }
                if ((smug != null) && computerFixed)
                {
                    smug.DisplayFixedDialog();
                }
            }
        }
        if (levelComplete || failed || gameComplete)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(currentscene.name);
            }
        }
        if (gameComplete && !failed)
        {
            victoryText.SetActive(true);
        }

        //Code that tracks remaining time the armor powerup has. When it's over, sets it to inactive and kills the UI element.
        if (armorTimeRemaining >= 0)
        {
            armorTimeRemaining -= time.deltaTime;
            if (armorTimeRemaining < 0)
            {
                armorUIIndicator.SetActive(false);
                armorActive = false;
            }
        }
    }

    void FixedUpdate()
    {
        if (!failed)
        {
            Vector2 position = rigidbody2d.position;
            position.x = position.x + speed * horizontal * Time.deltaTime;
            position.y = position.y + speed * vertical * Time.deltaTime;

            rigidbody2d.MovePosition(position);
        }
    }

    public void ChangeHealth(int amount)
    {
        if ((amount < 0) && !failed)
        {
            if (isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = timeInvincible;

            animator.SetTrigger("Hit");
            PlaySound(hitSound);
            hurtEffect.Play();
        }
        if (armorActive){
        currentHealth = Mathf.Clamp(((currentHealth + amount) - 1), 0, maxHealth);
        }
        else currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);

        if (currentHealth <= 0)
        {
            failed = true;
            victoryText.SetActive(false);
            levelCompleteText.SetActive(false);
            failureText.SetActive(true);
            animator.SetFloat("Look X", 0);
            animator.SetFloat("Look Y", 0);
            animator.SetFloat("Speed", 0);
            BGM.SetActive(false);
            VictoryTheme.SetActive(false);
            FailureTheme.SetActive(true);
        }
    }

    public void ActivateArmor()
    {

    }

    void Launch()
    {
        if (failed)
        {
            return;
        }
        if (ammoCount == 0)
        {
            return;
        }
        if (ammoCount > 0)
        {
            GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

            Projectile projectile = projectileObject.GetComponent<Projectile>();
            projectile.Launch(lookDirection, 300);

            animator.SetTrigger("Launch");

            PlaySound(throwSound);
            ammoCount--;
            ammoText.text = "Cog Count: " + ammoCount.ToString();
        }
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public void BotFixIncrement()
    {
        botsFixed++;
        countText.text = "Robots Fixed: " + botsFixed.ToString() + "/" + botTotal.ToString();
        if (botsFixed == botTotal)
        {
            if (currentscene.name == "main2")
            {
                levelComplete = true;
                levelCompleteText.SetActive(true);
            }
            else if (currentscene.name == "main3")
            {
                gameComplete = true;
                victoryText.SetActive(true);
                BGM.SetActive(false);
                VictoryTheme.SetActive(true);
            }
        }



    }

    public void ComputerFixed()
    {
        computerFixed = true;
    }
}