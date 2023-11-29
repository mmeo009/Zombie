using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody rb;           // ���� ��ü ����

    public float moveSpeed = 10;         // �̵� �ӵ�
    public float damageAmount;      // ������ ��
    public bool hasDamaged;         // ������ �˻�
    void Start()
    {
       if(TryGetComponent(out Rigidbody rbT))
        {
            if (rbT == true)
            {
                rb = GetComponent<Rigidbody>();
            }
            else
            {
                gameObject.AddComponent<Rigidbody>();
                rb = GetComponent<Rigidbody>();
            }
        }
        rb.velocity = transform.forward * moveSpeed;        // �����Ҷ� �ش� ��ü ���� �������� MoveSpeed ��ŭ�� �ӵ��� �Է�
        Destroy(this.gameObject, 3.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && !hasDamaged)
        {
            //other.GetComponent<EnemyHealthController>().TakeDamage((int)damageAmount);
            hasDamaged = true;
            Destroy(gameObject);
        }

    }


}
