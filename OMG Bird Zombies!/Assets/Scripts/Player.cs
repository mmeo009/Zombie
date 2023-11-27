using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float rotationSpeed = 5.0f;

    void Update()
    {
        // ȸ�� �Է� ó��
        RotateTowardsMouse();

    }
    private void LateUpdate()
    {
        // �̵� �Է� ó��
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

        // ���� ������Ʈ�� ���� �������� �̵� ���͸� ��ȯ�մϴ�.
        movement = transform.TransformDirection(movement);

        // �̵� ����
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
    }
}

