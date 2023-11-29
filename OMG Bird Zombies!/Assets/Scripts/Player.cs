using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Cinemachine;
using System.Numerics;
using Photon.Pun.Demo.Asteroids;

public class Player : MonoBehaviour
{
    public Transform firePos;
    public float moveSpeed = 5.0f;
    public float rotationSpeed = 20.0f;
    // PhotonView ������Ʈ ĳ��ó���� ���� ����
    public PhotonView pv;
    // �ó׸ӽ� ���� ī�޶� ������ ����
    public CinemachineVirtualCamera virtualCamera;
    public GameObject bullet;
    void Start()
    {

        pv = GetComponent<PhotonView>();
        virtualCamera = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
        //PhotonView�� �ڽ��� ���� ��� �ó׸ӽ� ����ī�޶� ����
        if (pv.IsMine)
        {
            virtualCamera.Follow = transform;
            virtualCamera.LookAt = transform;
        }
    }

    void Update()
    {
        // ȸ�� �Է� ó��
        RotateTowardsMouse();

    }
    private void LateUpdate()
    {
        // �̵� �Է� ó��
        Move();
        if(Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    void RotateTowardsMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            UnityEngine.Vector3 targetPosition = new UnityEngine.Vector3(hit.point.x, transform.position.y, hit.point.z);
            UnityEngine.Vector3 direction = targetPosition - transform.position;
            UnityEngine.Quaternion rotation = UnityEngine.Quaternion.LookRotation(direction);
            transform.rotation = UnityEngine.Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
    }

    void Move()
    {
        float verticalInput = Input.GetAxis("Vertical");
        if(verticalInput > 0)
        {
            UnityEngine.Vector3 movement = new UnityEngine.Vector3(0f, 0f, verticalInput).normalized;

            // ���� ������Ʈ�� ���� �������� �̵� ���͸� ��ȯ�մϴ�.
            movement = transform.TransformDirection(movement);

            // �̵� ����
            transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
        }

    }
    void Fire()
    {
        if(bullet == null)
        {
            bullet = Resources.Load<GameObject>("Prefabs/Bullet");
            Instantiate(bullet, firePos.position, firePos.rotation);
        }
        else
        {
            Instantiate(bullet, firePos.position, firePos.rotation);
        }    

    }
}

