using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorController : MonoBehaviour
{
    public AudioClip collectedClip;
    public ParticleSystem armorEffect;

    void OnTriggerEnter2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController>();

        if (controller != null)
        {
            controller.ActivateArmor();
            Destroy(gameObject);

            controller.PlaySound(collectedClip);
            armorEffect.Play();
        }
    }

}