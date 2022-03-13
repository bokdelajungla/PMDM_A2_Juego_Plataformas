using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplosionController : MonoBehaviour
{
    private Animator enemyExplosionAnimator;
    // Start is called before the first frame update
    void Start()
    {
     enemyExplosionAnimator = GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateHitTrigger()
    {
        enemyExplosionAnimator.SetTrigger("Start");
    } 
}
