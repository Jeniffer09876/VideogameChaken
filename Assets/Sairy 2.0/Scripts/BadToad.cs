using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadToad : MonoBehaviour
{
    public ParticleSystem smoke;
    private CharacterMovement cH;
    private AnimationEvents anim;

    private void Start()
    {
        anim = FindObjectOfType<AnimationEvents>();
        cH = FindObjectOfType<CharacterMovement>();
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            smoke.Play(true);
            anim.PlaySick();
        }
    }
}
