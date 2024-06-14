using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportScene : MonoBehaviour
{
    public bool tp;
    [SerializeField]
    private CharacterMovement characterMovement;
    private AudioSource winSfx;
    public ParticleSystem particleTeleport;

    public Timer canvas;
    // Start is called before the first frame update
    void Start()
    {
        characterMovement = FindObjectOfType<CharacterMovement>();
        canvas = FindObjectOfType<Timer>();
        winSfx = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        characterMovement.animator.SetTrigger("TakeHealth");
        particleTeleport.Play();
        winSfx.Play();

        if (tp)
        {
            StartCoroutine(WaitForTeleport());
        }
        else
        {
            canvas.winGame();
        }
    }

    IEnumerator WaitForTeleport()
    {
        yield return new WaitForSeconds(3.8f);
        SceneManager.LoadScene("02");
    }
        // Update is called once per frame
        void Update()
    {
        
    }
}
