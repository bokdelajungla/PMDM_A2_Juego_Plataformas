using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasTitleScreenController : MonoBehaviour
{
    public AudioClip buttonPressed, buttonSelected;
    private AudioSource canvasAudioSource;
    private float delay;
    private IEnumerator coroutine;
    // Start is called before the first frame update
    void Start()
    {
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
        SceneManager.LoadScene("Game");
        
    }
    public void ViewScores(){
        canvasAudioSource.PlayOneShot(buttonPressed);
        SceneManager.LoadScene("Highscore");
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
