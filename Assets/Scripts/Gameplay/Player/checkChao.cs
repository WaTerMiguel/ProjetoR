using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkChao : MonoBehaviour
{
    [SerializeField] PlayerMove move;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Chao"))
        {
            move.ChecandoChao(1);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Chao"))
        {
            move.ChecandoChao(2);
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Chao"))
        {
            move.ChecandoChao(0);
        }
    }
}
