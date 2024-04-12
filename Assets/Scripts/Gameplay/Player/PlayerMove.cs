using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{
    //Movimento
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] public bool podeAndar = true;
    [SerializeField] public Vector2 moveValue;
    [SerializeField] private bool noChao;
    [SerializeField] private float forcaPulo;

    //Energia
    [SerializeField] Image[] cargasEnergia;
    [SerializeField] Sprite[] cargasSprites;
    [SerializeField] [Range(0,4)]public int quantiEnergia;

    //Dashs
    [SerializeField] private float dashTempo;
    [SerializeField] private float dashForca;

    //Grab
    [SerializeField] private float cordaRadius;
    [SerializeField] private LayerMask cordaLayer;
    [SerializeField] private GameObject cordaTarget, cordaSelect, cordaPivot;
    [SerializeField] private SpringJoint2D cordaEffect;
    private Rigidbody2D pointGrab;
    private bool podeGrabar = false;
    [SerializeField] private bool estaGrab = false;
    [SerializeField] private LineRenderer cordaLine;

    //Animacoes
    [SerializeField] private string AnimacaoIdle, AnimacaoWalk;
    [SerializeField] private Animator anim; 
    [SerializeField] private SpriteRenderer sr;

    //Efeitos
    [SerializeField] EfeitoRastro sandeEffect;
    private bool sandeRapido = false;
    [SerializeField] public float velocidadeEfeitoSande;

    private void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        cordaEffect.anchor = cordaPivot.transform.localPosition;
    }

    private void FixedUpdate() 
    {
        Movimento();
    }

    private void Update()
    {
        
        moveValue = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        VerificacaoGrab();
        VerificacaoEnergia();

        if (quantiEnergia > 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Dash();
            }
            if (Input.GetMouseButton(1))
            {
                Grab(true);
            }
        }

        if (Input.GetMouseButtonUp(1))
        {
            Grab(false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Pulo();
        }

        if (rb.velocity.magnitude > velocidadeEfeitoSande)
        {
            sandeEffect.enabled = true;
            sandeRapido = true;
        }
        else if (sandeRapido)
        {
            sandeRapido = false;
            sandeEffect.enabled = false;
        }
        
    }

    void Movimento()
    {
        if (noChao)
        {
            if (podeAndar)
            {
                rb.velocity = new Vector2(moveValue.x * speed, rb.velocity.y);
            }
        }
        else
        {
            if (estaGrab)
            {
                rb.AddForce(moveValue * Vector2.right * speed / 2, ForceMode2D.Force);
            }
            else
            {
                rb.AddForce(moveValue * Vector2.right * speed, ForceMode2D.Force);
            }
        }

        if (rb.velocity.x != 0)
        {
            anim.SetBool("estaAndando", true);
        }
        else
        {
            anim.SetBool("estaAndando", false);
        }

        if (moveValue.x > 0)
        {
            sr.flipX = false;
        }

        if (moveValue.x < 0)
        {
            sr.flipX = true;
        }
        
    }

    void Pulo() 
    {
        if (noChao)
        {
            rb.velocity = new Vector2(rb.velocity.x, forcaPulo);
        }
    }

    void VerificacaoEnergia()
    {
        foreach(Image carga in cargasEnergia)
        {
            carga.sprite = cargasSprites[0];
        }

        for (int i = 0; i < quantiEnergia ; i++)
        {
            cargasEnergia[i].sprite = cargasSprites[1];
        }
    }

    public void AddEnergia(int quantiAdd)
    {
        quantiEnergia += quantiAdd;
        if (quantiEnergia > 4)
        {
            quantiEnergia = 4;
        }
    }


    void Dash()
    {
        if (moveValue != Vector2.zero)
        {
            podeAndar = false;
            rb.velocity = moveValue * dashForca;
            rb.gravityScale = 0f;
            quantiEnergia--;
            StartCoroutine(DashContador());
        }
        
    }

    IEnumerator DashContador()
    {
        sandeEffect.enabled = true;
        yield return new WaitForSeconds(dashTempo);
        rb.gravityScale = 3f;
        podeAndar = true;
        rb.velocity = rb.velocity.normalized * speed;
        sandeEffect.enabled = false;
    }

    void Grab(bool p)
    {
        if (podeGrabar)
        {
            if (p)
            {
                cordaEffect.connectedBody = pointGrab;
                if (Input.GetMouseButtonDown(1))
                {
                    quantiEnergia--;
                    cordaLine.gameObject.SetActive(true);
                    estaGrab = true;;
                }
            }
            else
            {
                cordaEffect.connectedBody = null;
                cordaLine.gameObject.SetActive(false);
                estaGrab = false;
            }

            podeAndar = !p;
            cordaEffect.enabled = p;
        }
    }

    void VerificacaoGrab()
    {
        Collider2D[] pontosCordas = Physics2D.OverlapCircleAll(transform.position, cordaRadius, cordaLayer);
        if (cordaLine.gameObject.activeSelf == true)
        {
            cordaLine.SetPosition(0,cordaPivot.transform.position);
            cordaLine.SetPosition(1,cordaTarget.transform.position);
        }
        if (pontosCordas.Length > 0)
        {
            podeGrabar = true;
            cordaSelect.SetActive(true);

            if (!Input.GetMouseButton(1))
            {
                foreach(Collider2D cordas in pontosCordas)
                {
                    if (cordaTarget == null || Vector2.Distance(transform.position, cordas.gameObject.transform.position) <
                                                Vector2.Distance(transform.position, cordaTarget.transform.position))
                    {
                        cordaTarget = cordas.gameObject;
                        pointGrab = cordas.GetComponent<Rigidbody2D>();
                    }
                }
            }
            
            cordaSelect.transform.position = cordaTarget.transform.position;
            

        }
        else
        {
            cordaSelect.SetActive(false);
            podeGrabar = false;
            cordaTarget = null;
        }
    }

    public void ChecandoChao(int encostando)
    {
        switch (encostando)
        {
            
            case 0:
            noChao = false;
                break;
            
            case 1:
            rb.velocity = Vector2.zero;
            noChao = true;
                break;

            case 2:
            noChao = true;
                break;
        }
    }

}