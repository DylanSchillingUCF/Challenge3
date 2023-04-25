using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorController : MonoBehaviour
{
    public AudioClip armorCollectionClip;
    public ParticleSystem armorEffect;

    void OnTriggerEnter2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController>();

        if (controller != null)
        {
            if (controller.health < controller.maxHealth)
            {
                controller.ActivateArmor();
                Destroy(gameObject);

                PlaySound
                armorEffect.Play();
            }
        }

    }
}