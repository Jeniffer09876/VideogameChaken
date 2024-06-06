using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pick : MonoBehaviour
{
    public bool goodToadArea;
    public bool badToadArea;
    public bool portalArea;
    private AnimationEvents AnimEvents;
    // Start is called before the first frame update
    private void Start()
    {
        AnimEvents = FindObjectOfType<AnimationEvents>();
    }
    private void OnTriggerStay(Collider other)
    {       

        if (other.gameObject.tag == "badToad")
        {
            badToadArea = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "goodToad")
        {
            goodToadArea = true;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "goodToad")
        {
            goodToadArea = false;
        }

    }

    private void Update()
    {

    }
}
