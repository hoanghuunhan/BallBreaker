using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float moveForce;
    public float maxVel;
    Rigidbody2D m_rb;
    bool m_isTrigger;
    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }
    public void Trigger()
    {
        if (m_rb)
        {
            m_isTrigger = true;
            m_rb.isKinematic = false;
            m_rb.AddForce(new Vector2(moveForce, moveForce));
            transform.SetParent(null);
        }
    }
    private void FixedUpdate()
    {
        if (m_rb && m_isTrigger)
        {
            m_rb.velocity = new Vector2(Mathf.Clamp(m_rb.velocity.x, -maxVel, maxVel), Mathf.Clamp(m_rb.velocity.y, -maxVel, maxVel));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(TagConsts.BRICK))
        {
            Brick brick = collision.gameObject.GetComponent<Brick>(); 
            if (brick)
            {
                brick.Hit();
            }
        }

        if (collision.gameObject.CompareTag(TagConsts.STICKER))
        {
            if (m_rb.velocity.x > 0)
            {
                m_rb.velocity = Vector2.zero;
                m_rb.AddForce(new Vector2(moveForce, moveForce));
            } else if (m_rb.velocity.x < 0)
            {
                m_rb.AddForce(new Vector2(-moveForce, moveForce));
            }
        }
        if (collision.gameObject.CompareTag(TagConsts.WALL_TOP))
        {
            if (m_rb.velocity.x > 0)
            {
                m_rb.velocity = Vector2.zero;
                m_rb.AddForce(new Vector2(moveForce, -moveForce));
            } else if (m_rb.velocity.x < 0)
            {
                m_rb.velocity = Vector2.zero;
                m_rb.AddForce(new Vector2(-moveForce, -moveForce));
            }
        }

        if (collision.gameObject.CompareTag(TagConsts.WALL_LEFT))
        {
            if (m_rb.velocity.y > 0)
            {
                m_rb.velocity = Vector2.zero;
                m_rb.AddForce(new Vector2(moveForce, moveForce));
            }
            else if (m_rb.velocity.y < 0)
            {
                m_rb.velocity = Vector2.zero;
                m_rb.AddForce(new Vector2(moveForce, -moveForce));
            }
        }

        if (collision.gameObject.CompareTag(TagConsts.WALL_RIGHT))
        {
            if (m_rb.velocity.y > 0)
            {
                m_rb.velocity = Vector2.zero;
                m_rb.AddForce(new Vector2(-moveForce, moveForce));
            }
            else if (m_rb.velocity.y < 0)
            {
                m_rb.velocity = Vector2.zero;
                m_rb.AddForce(new Vector2(-moveForce, -moveForce));
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(TagConsts.DEAD_ZONE))
        {
            GameGUIManager.Ins.gameOverDialog.Show(true);
            CineController.Ins.ShakeTrigger();
            AudioController.Ins.PlaySound(AudioController.Ins.lose);
            Destroy(gameObject);
        }
    }
}
