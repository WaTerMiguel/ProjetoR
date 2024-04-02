using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfeitoDesaparecer : MonoBehaviour
{
    public float tempoDesaparecer = 0.25f;
    private SpriteRenderer sr;

    private void Start() 
    {
        StartCoroutine(Desaparecer());
        sr = GetComponent<SpriteRenderer>();
        
    }

    IEnumerator Desaparecer()
    {
        yield return new WaitForSeconds(tempoDesaparecer);
        for (int i = 0 ; i < 8 ; i++)
        {
            sr.material.color -= new Color(0,0,0,0.1f);
            yield return new WaitForSeconds(tempoDesaparecer/8);
        }
        Destroy(this.gameObject);
    }
}
