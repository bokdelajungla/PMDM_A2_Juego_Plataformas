using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    public Transform target;
    private float speed = 2.2f;
    private Vector3 start, end;
    // Start is called before the first frame update
    void Start()
    {
        start = transform.position;
        end = target.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        if(target.position == end) {
            target.position = start;
        } else {
            target.position = end;
        }
    }
}
