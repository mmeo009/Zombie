using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public Rigidbody rb;
    public Banana banana;
    public int hp;
    public int speed;
    public int monsterId;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        banana = FindObjectOfType<Banana>();
    }
    private void Update()
    {
        gameObject.transform.LookAt(banana.transform);
        if(banana != null)
        {
            // ��ǥ ��ġ������ ���� ���� ���
            Vector3 direction = banana.transform.position - transform.position;
            direction.Normalize();

            // RigidBody�� ���� ���� �̵�
            rb.velocity = direction * speed;
        }
    }

    public void GetDamage(int amount)
    {
        hp -= amount;
        if(hp <= 0)
        {
            GameManager.Instance.MonsterDie(this);
        }
    }
}
