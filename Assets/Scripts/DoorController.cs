using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    private Animator animatorDoor;
    // al principio no se tiene la llave
    public bool hasKey = false;
    // panel de fin de juego (ganado)
    public GameObject panelWin;

    // Start is called before the first frame update
    void Start()
    {
        animatorDoor = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        // Si entramos en contacto con el player
        if(collision.gameObject.tag == "Player" && hasKey) {
            animatorDoor.SetBool("isOpen", true);
            panelWin.SetActive(true);
        }
    }
}
