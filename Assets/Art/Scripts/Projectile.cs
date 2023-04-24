using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rigidbody2d;

    public TextMeshProUGUI countText;
    RubyController rubyScript;


    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        rubyScript = GameObject.Find("Ruby").GetComponent<RubyController>();
    }

    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
    }

    void Update()
    {
        if (transform.position.magnitude > 1000.0f)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        HardEnemyController ehard = other.collider.GetComponent<HardEnemyController>();
        EnemyController e = other.collider.GetComponent<EnemyController>();
        SnugNPCController smug = other.collider.GetComponent<SmugNPCController>();
        if (ehard != null)
        {
            ehard.Fix();
            rubyScript.BotFixIncrement();

        }

        else if (e != null)
        {
            e.Fix();
            rubyScript.BotFixIncrement();
        }

        else if (smug != null)
        {
            smug.Fix();
        }

        Destroy(gameObject);
    }
}
