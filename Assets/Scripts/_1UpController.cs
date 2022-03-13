using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _1UpController : MonoBehaviour
{
    private PlayerController playerScript;
    private CanvasHUDController hudScript;
    private ExtraLivesIndicatorController extraLivesIndicatorScript;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = FindObjectOfType<PlayerController>();
        hudScript = FindObjectOfType<CanvasHUDController>();
        extraLivesIndicatorScript = FindObjectOfType<ExtraLivesIndicatorController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerScript.Play1UpSound();
            hudScript.extraLives += 1;
            extraLivesIndicatorScript.ShowIndicator();
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    } 
}
