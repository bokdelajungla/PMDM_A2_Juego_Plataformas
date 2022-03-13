using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
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

    private void OnCollisionEnter2D(Collision2D collision) {
        // Si entramos en contacto con el player
        if(collision.gameObject.tag == "Player") {
            playerScript.KeySound();
            FindObjectOfType<DoorController>().hasKey = true;
            Destroy(gameObject);
        }
    }
}
