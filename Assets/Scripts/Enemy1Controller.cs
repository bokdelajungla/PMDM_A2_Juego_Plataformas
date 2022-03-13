using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Controller : MonoBehaviour
{
    private float speed = 1.5f;
    // hacia donde gira segun la posicion del jugador
    private Transform playerPosition;
    private SpriteRenderer spriteRendererEnemy;

    // Start is called before the first frame update
    void Start()
    {
        playerPosition = GameObject.Find("Player").transform;
        spriteRendererEnemy = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerPosition.position, speed * Time.deltaTime);
        if(transform.position.x < playerPosition.position.x) {
            spriteRendererEnemy.flipX = true;
        } else{
            spriteRendererEnemy.flipX = false;
        }
    }

     private void OnCollisionEnter2D(Collision2D collision) {
        // Si entramos en contacto con el ground
        if(collision.gameObject.tag == "Player") {
            Destroy(gameObject);
        }

        // Si entramos en contacto con el ground
        if(collision.gameObject.tag == "Water") {
            Destroy(gameObject);
        }
    }
}
