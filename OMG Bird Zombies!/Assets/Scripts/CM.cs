using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CM : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float rotationSpeed = 5.0f;

    // ������Ʈ ĳ�� ó���� ���� ����
    private new Transform transform;
    private new Camera camera;
    // ������ Plane�� ����ĳ�����ϱ� ���� ����
    private Plane plane;
    private Ray ray;
    private Vector3 hitPoint;

    private void Start()
    {
        transform = GetComponent<Transform>();
        camera = Camera.main;
    }
    void Update()
    {
        // �¿� �̵� �Է� ó��
        Move();

        // ���콺 ��ġ�� �������� ȸ�� ó��
        Turn();
    }

    // ȸ�� ó���ϴ� �Լ�
    void Turn()
    {
        // ���콺�� 2���� ��ǩ���� �̿��� 3���� ����(����)�� ����
        ray = camera.ScreenPointToRay(Input.mousePosition);
        float enter = 0.0f;
        // ������ �ٴڿ� ���̸� �߻��� �浹�� ������ �Ÿ��� enter
        // ������ ��ȯ
        plane.Raycast(ray, out enter);
        // ������ �ٴڿ� ���̰� �浹�� ��ǩ�� ����
        hitPoint = ray.GetPoint(enter);
        // ȸ���ؾ� �� ������ ���͸� ���
        Vector3 lookDir = hitPoint - transform.position;
        lookDir.y = 0;
        // ���ΰ� ĳ������ ȸ���� ����
        transform.localRotation = Quaternion.LookRotation(lookDir);
    }

    void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // �̵� ���� ���
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
        movement.Normalize(); // ���� ���͸� ����ȭ�Ͽ� �밢�� �̵� �ÿ��� ������ �ӵ��� �̵��ϵ��� �մϴ�.

        // �̵� ���͸� ī�޶� �ٶ󺸴� �������� ��ȯ
        movement = Camera.main.transform.TransformDirection(movement);

        // y �� ���� 0���� �����Ͽ� ���� �̵��� ����
        movement.y = 0f;

        // �÷��̾� �̵�
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
    }
}
