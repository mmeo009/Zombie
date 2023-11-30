using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody rb;           // 물리 강체 선언

    public float moveSpeed = 0;         // 이동 속도
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
        Destroy(this.gameObject, 3.0f);
/*        Vector3 forwardDirection = transform.forward;

        // 10도 왼쪽으로 회전시킬 Quaternion을 생성합니다.
        Quaternion rotation = Quaternion.AngleAxis(-10, Vector3.up);

        // 회전을 적용하여 새로운 방향을 얻습니다.
        Vector3 newForwardDirection = rotation * forwardDirection;*/
    
}
    public void BulletSetting(float size, int speed )
    {

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
