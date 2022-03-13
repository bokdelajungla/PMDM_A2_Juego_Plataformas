using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Private
    private float speed, jumpForce, movementX, movementY;
    private bool isGrounded, isJumping;
    private SpriteRenderer flipPlayer;
    private Rigidbody2D playerRB;
    private AudioSource audioSourcePlayer;
    private Animator playerAnimator;
    private GameObject zSaber;
    private Vector3 zSaberScaleLeft, ZSaberScaleRight, damageRecoil, startPosition;

    //Public
    public int zeroHealth;
    public AudioClip jumpSound, zSaberSound, zHurtSound, zDeathSound;
    public AudioClip cyberElfCollectSound, _1UpCollectSound, hpSmallCapsuleCollectSound;
    public GameObject playerCamera;
    public GameObject panelGameOver;
    public CanvasHUDController canvasHUDScript;
    public GameObject zeroExplosion;
    

    void Awake()
    {
        //LOAD ZSaber Object before deactivating it
        zSaber = GameObject.FindWithTag("ZSaber");
        
        //Set Variables Used by other scripts
        zeroHealth = 15;
    }

    // Start is called before the first frame update
    void Start()
    {
        speed = 8f;
        jumpForce = 12f;
        movementX = 0f;
        
        isGrounded = true;
        flipPlayer = GetComponent<SpriteRenderer>();

        playerRB = GetComponent<Rigidbody2D>();
        audioSourcePlayer = GetComponent<AudioSource>();
        playerAnimator = GetComponent<Animator>();

        canvasHUDScript = FindObjectOfType<CanvasHUDController>();
        
        ZSaberScaleRight = new Vector3(1f,1f,1f);
        zSaberScaleLeft = new Vector3(-1f,1f,1f);
        damageRecoil = new Vector3(0.5f,0,0);
        startPosition = new Vector3(-3f, 0.5f, 0f);
        zSaber.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //IF NOT PLAYING ATTACK ANIMATION
        if(!playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("ZeroAttack") && !playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("ZeroDeath"))
        {
            //WALK BEHAVIOR
            movementX = Input.GetAxis("Horizontal");
            if(movementX > 0)
            {
                playerAnimator.SetBool("isMoving", true);
                flipPlayer.flipX = false;
            }
            if(movementX < 0)
            {
                playerAnimator.SetBool("isMoving", true);
                flipPlayer.flipX = true;
            }
            if(movementX == 0 ){
                playerAnimator.SetBool("isMoving", false);
            }
            transform.position += new Vector3(movementX, 0, 0) * speed * Time.deltaTime;
            
            //FALL BEHAVIOR
            movementY = Input.GetAxis("Vertical");
            if(movementY < 0 && !isGrounded)
            {
                playerAnimator.SetBool("isGrounded", false);
            }

            //JUMP        
            if(Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
            {
                playerRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                playerAnimator.SetTrigger("Jump");
                audioSourcePlayer.PlayOneShot(jumpSound);
            }

                    
            //ATTACK
            if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                playerAnimator.SetTrigger("Attack");
                audioSourcePlayer.PlayOneShot(zSaberSound);
                zSaber.SetActive(true);
                if(flipPlayer.flipX == false)
                {
                    zSaber.transform.localScale = ZSaberScaleRight;
                }
                if(flipPlayer.flipX == true)
                {
                    zSaber.transform.localScale = zSaberScaleLeft;
                }
                //ADD EVENT IN ANIMATION TO DEACTIVATE BY FUNCTION
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            playerAnimator.SetBool("isGrounded", true);
        }

        if(collision.gameObject.tag == "Enemy" && !playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("ZeroDeath"))
        {
            playerAnimator.Play("ZeroHurt");
            zeroHealth -= 2;
            DamageRecoil();
            CheckHealth();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "EnemyWeapon" && !playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("ZeroDeath")){
            playerAnimator.Play("ZeroHurt");
            zeroHealth -= 3;
            DamageRecoil();
            CheckHealth();
        }
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
            playerAnimator.SetBool("isGrounded", false);
        }
    }

    public void PlayHurtSound()
    {
        audioSourcePlayer.PlayOneShot(zHurtSound);
    }

    public void PlayDeathSound()
    {
        audioSourcePlayer.PlayOneShot(zDeathSound);
    }

    public void PlayCyberElfSound()
    {
        audioSourcePlayer.PlayOneShot(cyberElfCollectSound);
    }

    public void Play1UpSound()
    {
        audioSourcePlayer.PlayOneShot(_1UpCollectSound);
    }

    public void PlayHpRecoverSmallSound()
    {
        audioSourcePlayer.PlayOneShot(hpSmallCapsuleCollectSound);
    }


    public void DeactivateZSaberCollider()
    {
        zSaber.SetActive(false);
    }

    private void DamageRecoil()
    {
        if(flipPlayer.flipX == false)
        {
            transform.position -= damageRecoil;
        }
        if(flipPlayer.flipX == true)
        {
            transform.position += damageRecoil;
        }
    }
    private void CheckHealth()
    {
        if(zeroHealth > 5){
            playerAnimator.SetBool("LowHealth",false);
        }
        if(zeroHealth <= 5)
        {
            playerAnimator.SetBool("LowHealth", true);
        }
        if(zeroHealth <= 0)
        {
            playerAnimator.Play("ZeroDeath");
            zeroExplosion.SetActive(true);
            CheckGameOver();
        }
    }

    public void CheckGameOver()
    {
        if(canvasHUDScript.extraLives > 0){
            canvasHUDScript.extraLives -= 1;
            StartCoroutine("RestartPosition");
            canvasHUDScript.PlayMissionFailedAnimation();
        }
        else{
            canvasHUDScript.PlayMissionGameOverAnimation();
        }
    }

    IEnumerator RestartPosition()
    {
        yield return new WaitForSeconds(5f);
        //Reset all relevat indicators
        transform.position = startPosition;
        playerAnimator.Play("ZeroIdle");
        zeroHealth = 15;
        playerAnimator.SetBool("LowHealth", false);
        zeroExplosion.SetActive(false);

        playerCamera.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, playerCamera.transform.position.z);
        playerCamera.transform.parent = transform;
        canvasHUDScript.PlayMissionStartAnimation();

    }
}
