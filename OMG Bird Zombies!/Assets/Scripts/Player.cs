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
    // PhotonView 컴포넌트 캐시처리를 위한 변수
    public PhotonView pv;
    // 시네머신 가상 카메라를 저장할 변수
    public CinemachineVirtualCamera virtualCamera;
    public GameObject bullet;
    void Start()
    {

        pv = GetComponent<PhotonView>();
        virtualCamera = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
        //PhotonView가 자신의 것일 경우 시네머신 가상카메라를 연결
        if (pv.IsMine)
        {
            virtualCamera.Follow = transform;
            virtualCamera.LookAt = transform;
        }
    }

    void Update()
    {
        // 회전 입력 처리
        RotateTowardsMouse();

    }
    private void LateUpdate()
    {
        // 이동 입력 처리
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

            // 현재 오브젝트의 전방 방향으로 이동 벡터를 변환합니다.
            movement = transform.TransformDirection(movement);

            // 이동 적용
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

