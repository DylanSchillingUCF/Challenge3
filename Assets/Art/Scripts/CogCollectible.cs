using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogCollectible : MonoBehaviour
{
    public AudioClip collectedClip;

    void OnTriggerEnter2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController>();

        if (controller != null)
        {

            controller.ammoCount = controller.ammoCount + 4;
            Destroy(gameObject);

            controller.PlaySound(collectedClip);
            controller.ammoText.text = "Cog Count: " + controller.ammoCount.ToString();
        }
    }

}
