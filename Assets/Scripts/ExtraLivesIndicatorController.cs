using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExtraLivesIndicatorController : MonoBehaviour
{
    private GameObject hiddenPosition, showPosition;
    private float speed;
    private bool showIndicator;
    private CanvasHUDController canvasHUDScript;
    public Text extraLivesText;
    // Start is called before the first frame update
    void Start()
    {
        hiddenPosition = GameObject.Find("ExtraLivesIndicatorHidden");
        showPosition = GameObject.Find("ExtraLivesIndicatorShowing");
        speed = 200f;
        canvasHUDScript = FindObjectOfType<CanvasHUDController>();
    }

    // Update is called once per frame
    void Update()
    {
        extraLivesText.text = "x " + canvasHUDScript.extraLives;

        if(transform.position != showPosition.transform.position && showIndicator)
        {
            transform.position = Vector3.MoveTowards(transform.position, showPosition.transform.position, speed * Time.deltaTime);
        }

        if(transform.position == showPosition.transform.position && showIndicator)
        {
            StartCoroutine("WaitAndHide");
        }

        if(transform.position != hiddenPosition.transform.position && !showIndicator)
        {
            transform.position = Vector3.MoveTowards(transform.position, hiddenPosition.transform.position, speed * Time.deltaTime);
        }
    }

    public void ShowIndicator()
    {
        showIndicator = true;
        
        StartCoroutine("WaitAndHide");
    }

    IEnumerator WaitAndHide(){
        yield return new WaitForSeconds(4f);
        showIndicator = false;
    }
}
