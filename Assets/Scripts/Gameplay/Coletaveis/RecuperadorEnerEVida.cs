using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecuperadorEnerEVida : MonoBehaviour
{
    [SerializeField] bool vaiRecuperarVida = false;
    [SerializeField] int vidaQuantoVaiRecuperar = 0;
    [SerializeField] bool vaiRecuperarEnergia = false;
    [SerializeField] [Range(0,4)] int energiaQuantoVaiRecuperar = 0;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.TryGetComponent(out PlayerMove player))
            {
                if (vaiRecuperarEnergia)
                {
                    player.AddEnergia(energiaQuantoVaiRecuperar);
                }
            }

            Destroy(this.gameObject);
            
        }
    }

}
