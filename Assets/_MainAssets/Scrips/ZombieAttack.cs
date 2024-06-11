using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    public bool isAttacking;
    public Animator animator;
    public float cooldownAttack;
    public bool cooldownBool;

    // Start is called before the first frame update
    void Start()
    {
        isAttacking = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isAttacking = true;
            animator.SetTrigger("Attack");
        }
    }



    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isAttacking = false;
        }
    }


}
