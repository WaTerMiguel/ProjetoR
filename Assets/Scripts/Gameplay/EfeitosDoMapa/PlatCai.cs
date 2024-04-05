using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting;

public class PlatCai : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float tempo = 2.5f; 
    private Vector2 posInicio;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        posInicio = transform.position;
    }

    private void OnCollisionEnter2D()
    {
        StartCoroutine(Contador());
    }

    IEnumerator Contador()
    {
        yield return new WaitForSeconds(tempo);
        rb.bodyType = RigidbodyType2D.Dynamic;
        yield return new WaitForSeconds(2f);
        Resetar();
    }

    void Resetar()
    {
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.velocity = Vector2.zero;
        transform.position = posInicio;
    }
}
