using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadToad : MonoBehaviour
{
    public ParticleSystem smoke;
    private CharacterMovement cH;
    private AnimationEvents anim;
    private AudioSource sfxDamage;

    private void Start()
    {
        anim = FindObjectOfType<AnimationEvents>();
        cH = FindObjectOfType<CharacterMovement>();
        sfxDamage = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            sfxDamage.Play();
            smoke.Play(true);
            anim.PlaySick();
        }
    }
}
