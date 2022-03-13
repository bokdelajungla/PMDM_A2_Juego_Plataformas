using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomlessPitController : MonoBehaviour
{
    private PlayerController playerScript;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Remove MainCamera from Player
        if(collision.gameObject.tag == "Player"){
            Transform childToRemove = collision.transform.Find("MainCamera");
            childToRemove.parent = null;
            playerScript.PlayDeathSound();
            playerScript.CheckGameOver();
        }
        
    }

}
