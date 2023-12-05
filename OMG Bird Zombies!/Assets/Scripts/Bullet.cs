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
    private void Awake()
    {
        // Rigidbody 컴포넌트를 찾아서 할당
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        Destroy(this.gameObject, 3.0f);
    }
    public void BulletSetting(float size, float speed ,float axis)
    {
        gameObject.transform.localScale = new Vector3(size, size, size);
        moveSpeed = speed;

        Quaternion rot = Quaternion.AngleAxis(axis, Vector3.up);

        Vector3 go = rot * transform.forward;
        rb.velocity = go * moveSpeed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && !hasDamaged)
        {
            other.GetComponent<MonsterController>().GetDamage((int)damageAmount);
            hasDamaged = true;
            Destroy(gameObject);
        }

    }


}
