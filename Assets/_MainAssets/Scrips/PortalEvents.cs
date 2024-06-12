using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalEvents : MonoBehaviour
{

    [SerializeField]
    private ParticleSystem portal;
    [SerializeField]
    private int numberOfCrystals;
    [SerializeField]
    private AnimationEvents animEvents;
    [SerializeField]
    private AudioSource portalSfx;



    // Start is called before the first frame update
    void Start()
    {
        animEvents = FindObjectOfType<AnimationEvents>();
        portal.gameObject.SetActive(false);
        portalSfx = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (animEvents.count >= numberOfCrystals)
            {
                portal.gameObject.SetActive(true);
                portal.Play(true);
                portalSfx.Play();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            portal.Stop(true);
            portal.gameObject.SetActive(false);
            //portalSfx.Stop();
        }
    }

}

