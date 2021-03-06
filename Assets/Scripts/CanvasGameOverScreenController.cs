using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasGameOverScreenController : MonoBehaviour
{
    public AudioClip buttonPressed, buttonSelected;
    private AudioSource canvasAudioSource;
    private float delay;
    private IEnumerator coroutine;
    // Start is called before the first frame update
    void Start()
    {
        delay = 1.5f;
        canvasAudioSource = GetComponent<AudioSource>();
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

