using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CyberElfController : MonoBehaviour
{
    private static int cyberElvesRemaining;
    public Text cyberElvesRemainingText;
    private PlayerController playerScript;
    private CanvasHUDController hudScript;

    // Start is called before the first frame update
    void Start()
    {
        cyberElvesRemaining = GameObject.FindGameObjectsWithTag("CyberElf").Length;
        playerScript = FindObjectOfType<PlayerController>();
        hudScript = FindObjectOfType<CanvasHUDController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerScript.PlayCyberElfSound();
            cyberElvesRemaining -= 1;
            Debug.Log("Remaining: " + cyberElvesRemaining);
            cyberElvesRemainingText.text = "Cyber Elves x " + cyberElvesRemaining.ToString();
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
        
        if(cyberElvesRemaining == 0)
        {
            hudScript.WinGame();
        }
    } 
}
