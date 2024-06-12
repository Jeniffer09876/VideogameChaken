using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportScene : MonoBehaviour
{

    [SerializeField]
    private CharacterMovement characterMovement;
    // Start is called before the first frame update
    void Start()
    {
        characterMovement = FindObjectOfType<CharacterMovement>();

    }

    private void OnTriggerEnter(Collider other)
    {
        characterMovement.animator.SetTrigger("TakeHealth");
        StartCoroutine(WaitForTeleport());
    }

    IEnumerator WaitForTeleport()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("login");
    }
        // Update is called once per frame
        void Update()
    {
        
    }
}
