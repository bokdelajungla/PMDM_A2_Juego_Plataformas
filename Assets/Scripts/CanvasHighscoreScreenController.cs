using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class CanvasHighscoreScreenController : MonoBehaviour
{
    public AudioClip buttonPressed, buttonSelected;
    private AudioSource canvasAudioSource;
    private float delay;
    private IEnumerator coroutine;
    public Text scoreText;
    private float storedHighscore;

    // Start is called before the first frame update
    void Start()
    {
        delay = 1.5f;
        canvasAudioSource = GetComponent<AudioSource>();
        
        storedHighscore = PlayerPrefs.GetFloat("Highscore");
        
        TimeSpan time = TimeSpan.FromSeconds(storedHighscore);
        scoreText.text = "Best Time\n" + time.ToString(@"mm\'ss\.ff");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMouseOverButton(){
        canvasAudioSource.PlayOneShot(buttonSelected);
    }
    public void ResetScore(){
        canvasAudioSource.PlayOneShot(buttonPressed);
        PlayerPrefs.SetFloat("Highscore", 3599999f);
        storedHighscore = PlayerPrefs.GetFloat("Highscore");
        TimeSpan time = TimeSpan.FromSeconds(storedHighscore);
        scoreText.text = "Best Time\n" + time.ToString(@"mm\'ss\.ff");
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