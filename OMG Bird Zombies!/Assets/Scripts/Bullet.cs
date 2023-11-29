using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody rb;           // 물리 강체 선언

    public float moveSpeed = 10;         // 이동 속도
    public float damageAmount;      // 데미지 량
    public bool hasDamaged;         // 데미지 검사
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
        rb.velocity = transform.forward * moveSpeed;        // 시작할때 해당 물체 앞쪽 방향으로 MoveSpeed 만큼의 속도를 입력
    }

    private void OnTriggerEnter(Collider other)
    {
/*        if (other.tag == "Enemy" && !hasDamaged)
        {
            other.GetComponent<EnemyHealthController>().TakeDamage((int)damageAmount);
            hasDamaged = true;
        }*/

        Destroy(gameObject);                                // 충돌이 일어날 경우 파괴
    }


}
