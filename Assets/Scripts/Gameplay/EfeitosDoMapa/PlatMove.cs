using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatMove : MonoBehaviour
{
    [SerializeField] Transform posA, posB;
    [SerializeField] Vector3 posDestino;
    [SerializeField] float speed;
    [SerializeField] private bool podeSeMover = false;


    private void Start() 
    {
        transform.position = posA.position;
    }

    private void FixedUpdate() 
    {
        if (Vector2.Distance(transform.position, posA.position) < 0.1f && posDestino != posB.position)
        {
            posDestino = posB.position;
            podeSeMover = false;
        }

        if (Vector2.Distance(transform.position, posB.position) < 0.1f && posDestino != posA.position)
        {
            posDestino = posA.position;
            podeSeMover = false;
        }

        if (podeSeMover)
        {
            transform.position = Vector2.MoveTowards(transform.position, posDestino, speed * Time.deltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            podeSeMover = true;

            other.gameObject.transform.SetParent(this.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.SetParent(null);
        }
    }

}
