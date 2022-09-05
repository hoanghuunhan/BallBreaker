using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sticker : MonoBehaviour
{
    public float moveSpeed;
    Rigidbody2D m_rb;
    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        LimitPos();
    }
    private void FixedUpdate()
    {
        Move();
    }
    void Move()
    {
        if (!m_rb) return;
        if (GamePadsController.Ins.CanMoveLeft)
        {
            m_rb.velocity = Vector2.left * moveSpeed * Time.deltaTime;
        } else if (GamePadsController.Ins.CanMoveRight)
        {
            m_rb.velocity = Vector2.right * moveSpeed * Time.deltaTime;
        } else
        {
            m_rb.velocity = Vector2.zero;
        }
    }
    void LimitPos()
    {
        if (transform.position.x >= 3)
        {
            transform.position = new Vector3(3, transform.position.y, transform.position.z);
        } else if (transform.position.x <= -1)
        {
            transform.position = new Vector3(-1, transform.position.y, transform.position.z);
        }
    }
}
