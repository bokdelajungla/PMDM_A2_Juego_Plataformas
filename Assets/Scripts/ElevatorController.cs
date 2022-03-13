using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    //PRIVATE
    private float speed;
    private Vector3 start, end;
    
    //PUBLIC
    public Transform destination;
    


    // Start is called before the first frame update
    void Start()
    {
        speed = 5f;
        start = transform.position;
        end = destination.position;   
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination.position, speed*Time.deltaTime);
        if(transform.position == destination.position)
        {
            if(destination.position == end){
                destination.position = start;
            }
            else
            {
                destination.position = end;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Incluimos como hijo de la plataforma a cualquier objeto que entre en contancto con la plataforma
        //salvo el Ground para evitar bugs
        if(collision.gameObject.tag != "Ground"){
            collision.transform.parent = transform;
        }
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //Cuando el objeto deja de estar en contaco con la plataforma hacemos que deje de ser hijo de la plataforma
        collision.transform.parent = null;
    }
}