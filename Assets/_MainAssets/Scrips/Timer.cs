using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    private CharacterMovement cm;
    public Image win;
    public Image lose;
    public Text text;
    public float currentTime;
    public float starTime = 300;
    public Text instruccionText;

    // Start is called before the first frame update
    void Start()
    {
        cm = FindObjectOfType<CharacterMovement>();
        currentTime = starTime;
        win.enabled = false;
        lose.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        text.text = currentTime.ToString();
        if (currentTime <= 0)
        {
            loseGame();
            StartCoroutine(cm.ResetGame()) ;
        }

    }

    public void winGame()
    {
        //win.enabled = true;
    }

    public void loseGame()
    {
        lose.enabled = true;
    }

    public void ResetTime()
    {
        currentTime = starTime;
        text.text = currentTime.ToString();
        win.enabled = false;
        lose.enabled = false;
    }

    public void GoPortal()
    {
        instruccionText.text = "Ve la Piedra sagrada Cerca a la Maloka";
    }
}
