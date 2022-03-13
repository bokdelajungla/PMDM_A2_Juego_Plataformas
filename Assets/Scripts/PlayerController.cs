using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private int lives = 3;
    private int points = 0;
    private int maxPoints;
    private float speed = 5.5f;
    private float jumpForce = 6.5f;
    // HUD vidas
    public Text livesText;
    // HUD puntos
    public Text pointsText;
    // HUD puntos maximos
    public Text maxPointsText;
    // para saber si el player esta en el suelo
    private bool isGround = true;
    private float movementX;
    // componente SpriteRenderer
    private SpriteRenderer flipPlayer;
    // componente RigidBody
    private Rigidbody2D playerRb;
    // componente AudioSource
    private AudioSource audioSourcePlayer;
    // componentes AudioClip
    public AudioClip bronzeSound;
    public AudioClip silverSound;
    public AudioClip goldSound;
    public AudioClip jumpSound;
    public AudioClip attackSound;
    public AudioClip hittedSound;
    public AudioClip keySound;
    public AudioClip lifeSound;
    // la camara
    public GameObject cameraPlayer;
    // la explosion al morir
    public GameObject explosionPrefab;
    // componente Animator
    private Animator animatorPlayer;
    // panel game over
    public GameObject panelGameOver;

    // Start is called before the first frame update
    void Start()
    {
        flipPlayer = GetComponent<SpriteRenderer>();
        playerRb = GetComponent<Rigidbody2D>();
        audioSourcePlayer = GetComponent<AudioSource>();
        animatorPlayer = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movementX = Input.GetAxis("Horizontal");
        // me muevo hacia la derecha
        if(movementX > 0) {
            flipPlayer.flipX = false;
        }
        // me muevo hacia la izquierda
        if(movementX < 0) {
            flipPlayer.flipX = true;
        } 
        transform.position += new Vector3(movementX, 0, 0) * speed * Time.deltaTime;

        // salto
        if(Input.GetKeyDown(KeyCode.UpArrow) && isGround) {
            audioSourcePlayer.PlayOneShot(jumpSound);
            playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        }        
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        // Si entramos en contacto con el suelo
        if(collision.gameObject.tag == "Ground") {
            isGround = true;
        }
        // Si entramos en contacto con el agua
        if(collision.gameObject.tag == "Water") {
            livesText.text = "LIFE: 0";
            cameraPlayer.transform.parent = null;
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            panelGameOver.SetActive(true);
            Destroy(gameObject);
        }
        // Si entramos en contacto con los enemigos
        if(collision.gameObject.tag == "Enemy1" || collision.gameObject.tag == "Enemy2") {
            lives -= 1;
            if(lives == 0) {
                ImDead();
            } else {
                audioSourcePlayer.PlayOneShot(hittedSound);
                if(lives == 1) {
                    livesText.text = "LIFE: ❤";
                } else if(lives == 2) {
                    livesText.text = "LIFE: ❤❤️";
                } else if(lives == 3) {
                    livesText.text = "LIFE: ❤❤️❤️";
                } else if(lives == 4) {
                    livesText.text = "LIFE: ❤❤️❤️❤";
                } else if(lives == 5) {
                    livesText.text = "LIFE: ❤❤️❤️❤️❤";
                }
            }
        }
        // Si entramos en contacto con una vida
        if(collision.gameObject.tag == "Life") {
            lives += 1;
            if(lives == 2) {
                livesText.text = "LIFE: ❤❤️";
            } else if(lives == 3) {
                livesText.text = "LIFE: ❤❤️❤️";
            } else if(lives == 4) {
                livesText.text = "LIFE: ❤❤️❤️❤";
            } else if(lives == 5) {
                livesText.text = "LIFE: ❤❤️❤️❤️❤";
            }
        }
    } 

    // Si dejamos de estar en contacto con el ground
    private void OnCollisionExit2D(Collision2D collision) {
        if(collision.gameObject.tag == "Ground") {
            isGround = false;
        }
    } 

    public void BronzeCoinSound() {
        audioSourcePlayer.PlayOneShot(bronzeSound);
        points += 1;
        pointsText.text = "PUNTOS: " + points.ToString();
    }

    public void SilverCoinSound() {
        audioSourcePlayer.PlayOneShot(silverSound);
        points += 5;
        pointsText.text = "PUNTOS: " + points.ToString();
    }

    public void GoldCoinSound() {
        audioSourcePlayer.PlayOneShot(goldSound);
        points += 10;
        pointsText.text = "PUNTOS: " + points.ToString();
    }

    public void KeySound() {
        audioSourcePlayer.PlayOneShot(keySound);
    }

    public void LifeSound() {
        audioSourcePlayer.PlayOneShot(lifeSound);
    }

    public void ImDead() {
        livesText.text = "LIFE: 0";
        cameraPlayer.transform.parent = null;
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        panelGameOver.SetActive(true);
        Destroy(gameObject);
        if(!PlayerPrefs.HasKey("maxPoints")) 
            PlayerPrefs.SetInt("maxPoints", points);
        else {
            maxPoints = PlayerPrefs.GetInt("maxPoints");
            if (maxPoints < points) {
                PlayerPrefs.SetInt("maxPoints", points);
            }
        }
        maxPoints = PlayerPrefs.GetInt("maxPoints");
        maxPointsText.text = "HIGHSCORE: " + maxPoints.ToString();  
    }


}
