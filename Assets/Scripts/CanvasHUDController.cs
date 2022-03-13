using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class CanvasHUDController : MonoBehaviour
{

    public static float elapsedTime;
    private bool timerIsRunning;
    private float cyberElvesRemaining;
    
    public Text elapsedTimeText;
    public Text cyberElvesRemainingText;
    
    public int playerHP, extraLives;
    public GameObject hpBar;
    private Slider slider;

    public Image missionStart, missionFailed, missionGameOver;
    public GameObject endGamePanel;
    public AudioClip missionStartSound, missionAccomplished;

    private PlayerController playerScript;
    private ExtraLivesIndicatorController extraLivesScript;

    private IEnumerator coroutine;

    private AudioSource canvasAudioSource;
    public AudioSource backgroundMusic;
    private float backgroundMusicVolume;

    // Start is called before the first frame update
    void Start()
    {
        cyberElvesRemaining = GameObject.FindGameObjectsWithTag("CyberElf").Length;
        cyberElvesRemainingText.text = "CyberElves x "+ cyberElvesRemaining;
        playerHP = 15;
        extraLives = 2;
        
        elapsedTime = 0f;
        playerScript = FindObjectOfType<PlayerController>();
        
        extraLivesScript = FindObjectOfType<ExtraLivesIndicatorController>();
        
        canvasAudioSource = GetComponent<AudioSource>();

        backgroundMusic = GameObject.Find("BackgroundMusic").GetComponent<AudioSource>();
        backgroundMusicVolume = 0.5f;
        
        slider = hpBar.GetComponent<Slider>();
        missionFailed.enabled=false;
        missionStart.enabled=false;
        missionGameOver.enabled=false;
        PlayMissionStartAnimation();
        StartTimer();        

    }

    // Update is called once per frame
    void Update()
    {
        if(timerIsRunning)
        {
            elapsedTime = elapsedTime + Time.deltaTime;
            TimeSpan time = TimeSpan.FromSeconds(elapsedTime);
            elapsedTimeText.text = "Time: " + time.ToString(@"mm\'ss\.ff");
        }
        //HP Update
        playerHP = playerScript.zeroHealth;
        slider.value = playerHP;

    }
    private void StartTimer()
    {
        timerIsRunning = true;
    }
    private void StopTimer()
    {
        timerIsRunning = false;
    }


    public void PlayMissionStartAnimation(){
        missionStart.enabled = true;
        backgroundMusic.volume = backgroundMusicVolume;
        backgroundMusic.Play();

        missionStart.GetComponent<Animator>().Play("MissionStart");
        canvasAudioSource.PlayOneShot(missionStartSound);
        coroutine = WaitForAnimation(missionStart, 1.42f);
        StartCoroutine(coroutine);

        extraLivesScript.ShowIndicator();
    }
    public void PlayMissionFailedAnimation(){
        missionFailed.enabled = true;
        missionFailed.GetComponent<Animator>().Play("MissionFailed");
        
        coroutine = MusicFadeOut(1.42f);
        StartCoroutine(coroutine);
        coroutine = WaitForAnimation(missionFailed, 3f);
        StartCoroutine(coroutine);
    }
    public void PlayMissionGameOverAnimation(){
        missionGameOver.enabled = true;
        missionGameOver.GetComponent<Animator>().Play("MissionGameOver");
        
        coroutine = MusicFadeOut(8.1f);
        StartCoroutine(coroutine);
        coroutine = WaitForAnimation(missionGameOver, 8.1f);
        StartCoroutine(coroutine);
        StartCoroutine("LoadGameOverScreen");
    }

    IEnumerator WaitForAnimation(Image animatedImage, float waitTime){
        yield return new WaitForSeconds(waitTime);
        animatedImage.enabled = false;
    }
    
    IEnumerator MusicFadeOut(float timeToFade)
    {
        while(backgroundMusic.volume > 0){
            backgroundMusic.volume -= backgroundMusicVolume * Time.deltaTime/timeToFade;
            yield return null;
        }
        backgroundMusic.Stop();
        backgroundMusic.volume=backgroundMusicVolume;   
    }

    IEnumerator LoadGameOverScreen()
    {
        yield return new WaitForSeconds(9f);
        endGamePanel.SetActive(true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("GameOver");
    }

    public void WinGame()
    {
        StopTimer();
        coroutine = MusicFadeOut(2f);
        StartCoroutine(coroutine);
        canvasAudioSource.PlayOneShot(missionAccomplished);
        //SAVE SCORE:

        StartCoroutine("LoadWinGameScreen");
    }
    IEnumerator LoadWinGameScreen()
    {
        yield return new WaitForSeconds(7f);
        endGamePanel.SetActive(true);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("WinGame");
    }

}
