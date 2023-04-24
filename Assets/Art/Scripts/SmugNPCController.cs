using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmugNPCController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         
    }
    
    public void Fix()
    {
        broken = false;
        rigidbody2D.simulated = false;
        //optional if you added the fixed animation
        animator.SetTrigger("Fixed");

        smokeEffect.Stop();
    }
}
