using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpSmallCapsuleController : MonoBehaviour
{
    private int maxHealth, remainingHpBarsToFill;
    private PlayerController playerScript;
    private CanvasHUDController hudScript;
    private ExtraLivesIndicatorController extraLivesIndicatorScript;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 15;
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
            playerScript.PlayHpRecoverSmallSound();
            if(playerScript.zeroHealth >= (maxHealth-3))
            {
                remainingHpBarsToFill = maxHealth - playerScript.zeroHealth;
            }
            else{
                remainingHpBarsToFill = 3;
            }
            StartCoroutine("FillHpBar");
        }
    }
    //Fill 1 bar every 0.2 secs
    IEnumerator FillHpBar()
    {
        while(remainingHpBarsToFill>0){
            playerScript.zeroHealth += 1;
            remainingHpBarsToFill -= 1;
            yield return new WaitForSeconds(0.2f);
            Debug.Log("Quedan " + remainingHpBarsToFill);
        }
        gameObject.SetActive(false);
    }
}
