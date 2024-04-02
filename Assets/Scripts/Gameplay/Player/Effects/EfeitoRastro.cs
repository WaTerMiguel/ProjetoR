using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfeitoRastro : MonoBehaviour
{
    [SerializeField] private Material dashEffectMaterial;
    [SerializeField] private GameObject SandevistanGrupo;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private float tempoCadaInstantiate = 0.5f;

    private void OnEnable() 
    {
        StartCoroutine(EfeitoSande());
    }

    private void OnDisable() 
    {
        StopAllCoroutines();
    }


    IEnumerator EfeitoSande()
    {
        while(true)
        {
            GameObject cloneSR = Instantiate(sr.gameObject, transform.position, Quaternion.identity, SandevistanGrupo.transform);
            cloneSR.AddComponent<EfeitoDesaparecer>();
            if (cloneSR.TryGetComponent(out SpriteRenderer csr))
            {
                csr.color = new Color(0f,0.6f,1f,1f);
                csr.sortingOrder = -1;
                csr.material = dashEffectMaterial;
            }
            yield return new WaitForSeconds(tempoCadaInstantiate);
        }
    }


}
