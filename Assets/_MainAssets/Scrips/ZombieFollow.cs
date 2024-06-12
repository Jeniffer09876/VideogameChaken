using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ZombieFollow : MonoBehaviour
{
    public Animator animator;
    public bool idle;
    private AudioSource sfxZombieWalk;

    // Start is called before the first frame update
    void Start()
    {
        sfxZombieWalk = GetComponent<AudioSource>();
        idle = true;    
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            idle = false;
            sfxZombieWalk.Play();
            //animator.SetBool("isWalk",true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            idle = true;
            sfxZombieWalk.Stop();
            //animator.SetBool("isWalk", false);
        }
    }
}
