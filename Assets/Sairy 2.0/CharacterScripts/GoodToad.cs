using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodToad : MonoBehaviour
{
    private AnimationEvents animEvents;
    private Pick pick;
    private void Start()
    {
        animEvents = FindObjectOfType<AnimationEvents>();
        pick = FindObjectOfType<Pick>();
    }

    public void ResetGame()
    {
        this.gameObject.SetActive(true);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (animEvents.destroyGoodToad)
            {
                pick.goodToadArea = false;
                //Destroy(this.gameObject);
                this.gameObject.SetActive(false);
                animEvents.destroyGoodToad = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            pick.goodToadArea = false;
        }
    }

}



