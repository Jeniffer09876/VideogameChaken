using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationEvents : MonoBehaviour
{
    private Timer canvas;
    private Pick pick;
    public Text counter;
    public ParticleSystem playerHappyParticles;
    public ParticleSystem playerSickParticles;
    public ParticleSystem playerChargeParticles;
    public bool destroyGoodToad;

    public int count;
    // Start is called before the first frame update
    void Start()
    {
        canvas = FindObjectOfType<Timer>();
        pick = FindObjectOfType<Pick>();
        count = 0;
        counter.text = count.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (pick.badToadArea)
        {
            playerSickParticles.Play(true);
        }
        */
        if (count == 10)
        {
            canvas.winGame();
        }
    }
    public void PickItem()
    {
        if (pick.goodToadArea)
        {
            destroyGoodToad = true;
            playerHappyParticles.Play(true);
            count++;
            counter.text = count.ToString();
            pick.goodToadArea = false;
        }
    }

    public void PlaySick()
    {
        playerSickParticles.Play(true);
    }

    public void PlayChargeStick()
    {
        playerChargeParticles.Play(true);
    }

    public void StopChargeStick()
    {
        playerChargeParticles.Stop(true);
    }

    public void ResetGame()
    {
        count = 0;
        counter.text = count.ToString();
    }
}
