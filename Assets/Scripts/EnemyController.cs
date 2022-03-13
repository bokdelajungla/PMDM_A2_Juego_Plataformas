using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //PRIVATE
    private float speed, detectDistanceX, detectDistanceY, attackRange;
    private bool playerIsClose, playerInRange, enemyIsDead;
    private Vector3 enemySpawnPosition;
    private Transform playerTransform;
    private SpriteRenderer enemySpriteRenderer;
    private Animator enemyAnimator, enemyExplosionAnimator;
    private EnemyExplosionController enemyExplosionScript;
    private AudioSource audioSourceEnemy;
    private GameObject enemyWeapon;
    private Vector3 weaponScaleLeft, weaponScaleRight;
    
    //PUBLIC
    public AudioClip enemyExplosion, weaponSound;

    // Start is called before the first frame update
    void Start()
    {
        //LOAD WeaponCollider Object before deactivating it
        enemyWeapon = GameObject.FindWithTag("EnemyWeapon");
        
        weaponScaleRight = new Vector3(1f,1f,1f);
        weaponScaleLeft = new Vector3(-1f,1f,1f);

        speed = 2f;
        detectDistanceX = 7f;
        detectDistanceY = 3f;
        attackRange = 2.5f;
        enemySpawnPosition = transform.position;
        playerIsClose = false;
        playerInRange = false;
        enemyIsDead = false;

        playerTransform = GameObject.Find("Player").transform;
        enemySpriteRenderer = GetComponent<SpriteRenderer>();
        enemyAnimator = GetComponent<Animator>();
        enemyExplosionAnimator = GetComponent<Animator>();
        enemyExplosionScript = FindObjectOfType<EnemyExplosionController>();
        audioSourceEnemy = GetComponent<AudioSource>();

        enemyWeapon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //DETECTION
        float xDistance = Mathf.Abs(playerTransform.position.x - transform.position.x);
        float yDistance = Mathf.Abs(playerTransform.position.y - transform.position.y); 
        //Movement Trigger
        if(xDistance <= detectDistanceX && xDistance > attackRange && yDistance <= detectDistanceY)
        {
            enemyAnimator.SetBool("playerIsClose", true);
            enemyAnimator.SetBool("playerInRange", false);
            playerIsClose = true;
            playerInRange = false;
        }

        //Attack Trigger
        if(xDistance <= detectDistanceX && xDistance <= attackRange && yDistance <= detectDistanceY)
        {
            enemyAnimator.SetBool("playerIsClose", true);
            enemyAnimator.SetBool("playerInRange", true);
            playerIsClose = true;
            playerInRange = true;

        }
        //Stop trigger
        if(xDistance > detectDistanceX && yDistance > detectDistanceY)
        {
            enemyAnimator.SetBool("playerIsClose", false);
            enemyAnimator.SetBool("playerInRange", false);
            playerIsClose = false;
            playerInRange = false;
        }

        //Movement towards player
        if(!enemyIsDead && playerIsClose && !playerInRange && !(enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("EnemyAttack")))
        {
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
            if(transform.position.x < playerTransform.position.x)
            {
                enemySpriteRenderer.flipX = true;
            }
            else
            {
                enemySpriteRenderer.flipX = false;
            }
        }
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "ZSaber")
        {
            enemyAnimator.SetTrigger("Hit");
            enemyExplosionScript.ActivateHitTrigger();
            enemyIsDead = true;
            audioSourceEnemy.PlayOneShot(enemyExplosion);       
        }
    }
    public void playEnemyWeaponSound()
    {
        audioSourceEnemy.PlayOneShot(weaponSound);
    }
    public void ActivateWeaponCollider()
    {
        enemyWeapon.SetActive(true);
        if(enemySpriteRenderer.flipX == false)
            {
                enemyWeapon.transform.localScale = weaponScaleRight;
            }
            if(enemySpriteRenderer.flipX == true)
            {
                enemyWeapon.transform.localScale = weaponScaleLeft;
            }
            //Added event on animation to deactivate collider
    } 
    public void DeactivateWeaponCollider()
    {
        enemyWeapon.SetActive(false);
    }
    public void DeactivateEnemy(){
        Destroy(gameObject);
    }
    
}
