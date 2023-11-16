using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoolController : MonoBehaviour
{
    Vector3 defaultLocalPos;
    Rigidbody2D m_rigidBody;

    [SerializeField] private GamePlayController m_gameController;
    [SerializeField] private BasketController m_basketController;

    public Rigidbody2D Rigidbody
    {
        get
        {
            if (m_rigidBody == null)
            {
                m_rigidBody = GetComponent<Rigidbody2D>();
            }    
            return m_rigidBody;
        }
    }

    public GamePlayController gameController
    {
        get
        {
            if (m_gameController == null)
            {
                m_gameController = GameObject.FindObjectOfType<GamePlayController>();
            }
            return m_gameController;
        }
    }

    void Start()
    {
        defaultLocalPos = transform.localPosition;
    }

    void Update()
    {
        
    }

    public void ResetState()
    {
        Rigidbody.bodyType = RigidbodyType2D.Kinematic;
        Rigidbody.velocity = Vector3.zero;
        transform.localPosition = defaultLocalPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var collisionGameObject = collision.gameObject;
        if (collisionGameObject.transform.name.Equals("ConstraintTop") && !gameController.IsFalling)
        {
            gameController.PullDownWalls();
        }

        if (!gameController.IsFalling)
        {
            return;
        }    

        //if((collisionGameObject.transform.parent != null && collisionGameObject.transform.parent == transform.parent)
        //    || collisionGameObject.transform.name.Equals("ConstraintBottom")
        //    || collision.transform.parent.position.y < transform.parent.position.y)
        if(collisionGameObject.transform.position.y <= gameController.CurrentBasket.transform.position.y && !collisionGameObject.transform.name.Equals("ConstraintTop"))
        {
            gameController.IsFalling = false;
            gameController.SubtractHeart();
            m_basketController.ResetState();
            gameController.IsFlying = false;
        }   
        else if(!collisionGameObject.transform.name.Equals("ConstraintTop"))
        {
            gameObject.SetActive(false);
            gameController.IsFalling= false;
            var basket = collision.GetComponentInParent<BasketController>();
            if (basket != null)
            {
                gameController.IsFlying = false;
                gameController.UpdateScore();
                basket.SetCurrentBasket();
            }
        }
    }

    
}
