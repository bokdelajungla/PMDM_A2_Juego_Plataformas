using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    // para poder acceder al Script del player
    private PlayerController playerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Player") {
            playerScript.LifeSound();
            Destroy(gameObject);
        }
    }
}
