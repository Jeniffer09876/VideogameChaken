using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.SceneManagement;


public class CharacterMovement : MonoBehaviour
{
    public GameObject dad;
    public Transform resetTrasform;
    public Animator animator;
    public CharacterController characterController;
    public Transform cam;
    public Health healthBar;

    public float runSpeed = 5f;
    public float AimRunSpeed = 2f;
    private float zombieSpeed = 1.5f;
    private float crounchSpeed = 2f;
    public float turnSmoothTime = 0.1f;
    public int maxHealth = 100;
    public int currentHeatlh;
    public int damage = 1;
    public int zombieDamage = 10;
    public float gravityMultiplayer;

    private float gravity = 9.81f;
    private float _velocity;  

    float turnVelocity;
    int hashVelocity;
    float playerVelicity;

    public float acceleration = 0.1f;
    public float deaceleration = 0.5f;

    private float horizontal;
    private float vertical;

    float xInput;
    float yInput;

    bool isHuman;
    bool isCrounch;
    bool isDead;

    public Projectile projectileScript;
    [SerializeField]
    private float cooldown;
    private bool magicThrowed;
    private QuadraticCurve curve;
    [SerializeField]
    private Vector3 curveStartPosition;

    AnimationEvents animEvents;
    GoodToad goodToad;
    Pick pick;
    Timer canvas;
    ZombieAttack ZombieAttack;
    ZombieWalk ZombieWalk;



    // Start is called before the first frame update

    // Update is called once per frame

    private void Awake()
    {   
        //animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        healthBar.SetMaxHelth(maxHealth);
        pick = FindObjectOfType<Pick>();
        canvas = FindObjectOfType<Timer>();
        goodToad = FindObjectOfType<GoodToad>();
        animEvents = FindObjectOfType<AnimationEvents>();
        ZombieAttack = FindObjectOfType<ZombieAttack>();
        ZombieWalk = FindObjectOfType<ZombieWalk>();
        isHuman = true;
        hashVelocity = Animator.StringToHash("RunVelocity");
        playerVelicity = 0.0f;
        currentHeatlh = maxHealth;
        cooldown = 0f;
        curve = projectileScript.curve;
        curveStartPosition = curve.transform.position;
        isDead = false;
        Time.timeScale = 1f;
    }

    void Update()
    {
        

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        
            //Inputs aim
            if (cooldown <=0)
        {
            if (Input.GetKeyDown("space"))
            {
                animator.SetBool("Aiming", true);
                //animEvents.playerChargeParticles.Play(true);
                animEvents.PlayChargeStick();
            }

            if (Input.GetKeyUp("space"))
            {
                animator.SetBool("Aiming", false);
                //animEvents.playerChargeParticles.Stop(true);
                animEvents.StopChargeStick();
                StartCoroutine(WaitForAimAnimation());
            }

            if (Input.GetKey("space"))
            {
                AimingMove();
            }
        }


        if (!Input.GetKey("space"))
        {
            Move();
        }



        // pickup item input
        if (Input.GetKeyDown("e")) animator.SetTrigger("PickUp");

        //if (Input.GetKeyDown("q")) animator.SetTrigger("TakeHealth");

         
        if (pick.badToadArea)
        {
            animator.SetTrigger("TakeDamage");
            currentHeatlh -= damage;
            healthBar.SetHelth(currentHeatlh);
            Vector3 PushVector = transform.forward * 2f;
            transform.DOMove(transform.position - PushVector, 0.5f).SetEase(Ease.Linear);
            pick.badToadArea = false;
        }

        //dead condition
        if (currentHeatlh <= 0)
        {
            canvas.loseGame();
            //Time.timeScale = 0f;
            //isDead=true;

            StartCoroutine(ResetGame());
                
            if (Input.GetKeyDown("r"))
            {
                //ResetGame();
            }
        }

        //cooldown condition
        if (cooldown > 0 && magicThrowed)
        {
            cooldown -= Time.deltaTime;
            if (cooldown <= 0)
            {
                magicThrowed = false;
                curve.transform.SetParent(transform);
                curve.transform.localPosition = new Vector3(-0.27f, 0.9934968f,2.21f);
                curve.transform.rotation = transform.rotation;
            }

        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Zombie")
        {   
            StartCoroutine(WaitForAttack());
        }
    }

    IEnumerator WaitForAttack()
    {
        yield return new WaitForSeconds(0.3f);
        animEvents.TakeDamage();
        animator.SetTrigger("TakeDamage");
        currentHeatlh -= zombieDamage;
        healthBar.SetHelth(currentHeatlh);
        Vector3 PushVector = transform.forward * 2f;
        transform.DOMove(transform.position - PushVector, 0.5f).SetEase(Ease.Linear);
    }

    IEnumerator WaitForAimAnimation() 
    {
        yield return new WaitForSeconds(0.6f);
        //projectileScript.gameObject.SetActive(true);
        projectileScript.magicParticles.Play(true);
        projectileScript.magic = true;
        cooldown = 2;
        magicThrowed = true;
        curve.transform.SetParent(transform.parent);
    }


    private void Move()
    {

        Vector3 direction = new Vector3(horizontal, 0.0f, vertical).normalized;

        //gravity apply
        if (!characterController.isGrounded)
        {
            _velocity = -gravity * Time.deltaTime;
            characterController.Move(new Vector3(0, _velocity, 0));
            //print("gravity");
        }


        if (direction.magnitude >= 0.1f)
        {
         
            //get the angle of the camera
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            // Human speed / zombie Speed / crounchSpeed
            if (!isHuman)
            {
                characterController.Move(moveDirection.normalized * zombieSpeed * Time.deltaTime);
            }
            else if (isCrounch)
            {
                characterController.Move(moveDirection.normalized * crounchSpeed * Time.deltaTime);
            }
            else
            {
                characterController.Move(moveDirection.normalized * runSpeed * Time.deltaTime);
            }

            if (playerVelicity < 1f)
            {
                playerVelicity += Time.deltaTime * acceleration;
            }

        }
        else if (direction.magnitude == 0.0f)
        {
            if (playerVelicity > 0.0f)
            {
                playerVelicity -= Time.deltaTime * deaceleration;
            }
            if (playerVelicity < 0.0f)
            {
                playerVelicity = 0.0f;
            }
        }
        animator.SetFloat(hashVelocity, playerVelicity);

        if (direction.magnitude != 0) animator.SetBool("Running", true);
        else animator.SetBool("Running", false);

     
        

    }

    void AimingMove()
    {
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        //Send input to animator
        
        //gravity apply
        if (!characterController.isGrounded)
        {
            _velocity = -gravity * Time.deltaTime;
            characterController.Move(new Vector3(0, _velocity, 0));
            //print("gravity");
        }

        if (xInput < 1.5f)
        {
        }

        xInput += Time.deltaTime * horizontal * 0.5f;
        yInput += Time.deltaTime * vertical * 0.5f;

        //SendInputToAnimator();
        // press to run

        if (direction.magnitude >= 0.1f)
        {
            //float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            //float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnVelocity, turnSmoothTime);

            //transform.rotation = Quaternion.Euler(0f, angle, 0f);


            //get the angle of the camera
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            characterController.Move(moveDirection * AimRunSpeed * Time.deltaTime);

            if (xInput < 1f)
            {
                xInput += Time.deltaTime * horizontal;
            }
            if (yInput < 1f)
            {
                yInput += Time.deltaTime * vertical;
            }

        }
        else if (direction.magnitude == 0.0f)
        {
            xInput = 0;
            yInput = 0;
        }

        animator.SetFloat("xInput", xInput);
        animator.SetFloat("yInput", yInput);
    }

    public IEnumerator ResetGame()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("animationTest");

        /*
        dad.transform.position = resetTrasform.position;
        dad.transform.rotation = resetTrasform.rotation;
        currentHeatlh = maxHealth;
        healthBar.SetHelth(currentHeatlh);
        canvas.ResetTime();
        goodToad.ResetGame();
        animEvents.ResetGame();
        */
    }

    void ZombieMove()
    {

    }
}
