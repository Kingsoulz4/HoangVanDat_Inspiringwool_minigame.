using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasketController : MonoBehaviour
{
    [SerializeField] protected WoolController m_wool;
    [SerializeField] private GamePlayController m_gameController;
    float velocity = 10f;
    LTDescr woolDropTween = null;

    bool IsMoving { get; set; }
    int direction = 1;

    public GamePlayController gameController
    {
        get
        {
            if(m_gameController == null)
            {
                m_gameController = GameObject.FindObjectOfType<GamePlayController>();
            }
            return m_gameController;
        }
    }

    protected virtual void Start()
    {
        IsMoving = true;
        SetWoolActive(false);
        direction = Random.Range(-1, 2);
    }

    protected virtual void Update()
    {
        if (IsMoving)
        {
            transform.position += Vector3.left * 0.05f * direction;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.transform.name.Equals("ConstraintLeft"))
        {
            direction = -1;
        }
        else if(collision.gameObject.transform.name.Equals("ConstraintRight"))
        {
            direction = 1;
        }
    }

    public void SetWoolActive(bool isActive)
    {
        m_wool.gameObject.SetActive(isActive);
    }    

    public virtual void SetCurrentBasket()
    {
        gameController.SetCurrentBasket(this);
    }

    public virtual void ResetState()
    {
        if (woolDropTween != null)
        {
            LeanTween.cancel(woolDropTween.id);
            woolDropTween = null;
        }
        m_wool.transform.SetParent(transform);
        m_wool.ResetState();

    }

    public void DropWool()
    {
        gameController.IsFalling = true;
        //Rigidbody2D rb = m_wool.Rigidbody;
        //rb.bodyType = RigidbodyType2D.Dynamic;
        //rb.velocity = Vector2.zero;
        if(woolDropTween != null)
        {
            LeanTween.cancel(woolDropTween.id);
            woolDropTween = null;
        }
        woolDropTween = LeanTween.move(m_wool.gameObject, m_wool.transform.position - Vector3.up* 1000f, 1000f/velocity);
    }

    public virtual void PushWool()
    {
        //Rigidbody2D rb = m_currentWool.GetComponent<Rigidbody2D>();
        //rb.bodyType = RigidbodyType2D.Dynamic;
        //rb.velocity = Vector2.zero;
        //rb.AddForce(Vector2.up * woolForce, ForceMode2D.Impulse);
        m_wool.transform.SetParent(gameController.m_pusherPoint.transform);
        LeanTween.moveLocalY(m_wool.gameObject, m_wool.transform.localPosition.y + 5f, 1f)
            .setEaseInExpo()
            .setEaseOutCubic()
            .setOnComplete(() =>
            {
                DropWool(); 
            });
    }
}
