using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class CanvasWinGameScreenController : MonoBehaviour
{
    public AudioClip buttonPressed, buttonSelected;
    private AudioSource canvasAudioSource;
    private float delay;
    private IEnumerator coroutine;
    public Text scoreText;
    private float timeScore, storedHighscore;
    private bool newRecord;
    // Start is called before the first frame update
    void Start()
    {
        delay = 1.5f;
        canvasAudioSource = GetComponent<AudioSource>();
        timeScore = CanvasHUDController.elapsedTime;
        
        storedHighscore = PlayerPrefs.GetFloat("Highscore");
        if(storedHighscore > timeScore){
            PlayerPrefs.SetFloat("Highscore", timeScore);
            newRecord = true;
        }

        TimeSpan time = TimeSpan.FromSeconds(timeScore);
        if(newRecord){
            scoreText.text = "Your Time\n" + time.ToString(@"mm\'ss\.ff") + " - NEW!";
        }
        else{
            scoreText.text = "Your Time\n" + time.ToString(@"mm\'ss\.ff");
        }        

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMouseOverButton(){
        canvasAudioSource.PlayOneShot(buttonSelected);
    }
    public void StartGame(){
        canvasAudioSource.PlayOneShot(buttonPressed);
        coroutine = ChangeSceneDelay("Game", delay);
        StartCoroutine(coroutine);
    }
    public void BackToTiltle(){
        canvasAudioSource.PlayOneShot(buttonPressed);
        coroutine = ChangeSceneDelay("Title", delay);
        StartCoroutine(coroutine);
    }
    public void QuitGame(){
        canvasAudioSource.PlayOneShot(buttonPressed);
        Application.Quit();
    }

    IEnumerator ChangeSceneDelay(string SceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneName);  
    }
}
