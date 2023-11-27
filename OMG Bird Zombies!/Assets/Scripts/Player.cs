using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float rotationSpeed = 5.0f;

    void Update()
    {
        // 회전 입력 처리
        RotateTowardsMouse();

    }
    private void LateUpdate()
    {
        // 이동 입력 처리
        Move();
    }

    void RotateTowardsMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            Vector3 direction = targetPosition - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
    }

    void Move()
    {
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(0f, 0f, verticalInput).normalized;

        // 현재 오브젝트의 전방 방향으로 이동 벡터를 변환합니다.
        movement = transform.TransformDirection(movement);

        // 이동 적용
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
    }
}

