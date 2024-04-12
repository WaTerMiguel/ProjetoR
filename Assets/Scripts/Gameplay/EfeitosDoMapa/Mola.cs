using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mola : MonoBehaviour
{
    [SerializeField] float forcaDaMola;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.TryGetComponent(out Rigidbody2D rb))
            {
                rb.velocity = Vector2.up * forcaDaMola;
            }
        }
    }
}
