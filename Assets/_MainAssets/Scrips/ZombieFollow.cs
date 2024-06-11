using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieFollow : MonoBehaviour
{
    public Animator animator;
    public bool idle;

    // Start is called before the first frame update
    void Start()
    {
        idle = true;    
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            idle = false;
            //animator.SetBool("isWalk",true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            idle = true;
            //animator.SetBool("isWalk", false);
        }
    }
}
