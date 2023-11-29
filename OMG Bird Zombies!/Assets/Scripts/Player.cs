using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Cinemachine;
using System.Numerics;
using Photon.Pun.Demo.Asteroids;

public class Player : MonoBehaviour, IPunObservable
{
    public Transform firePos;
    public float moveSpeed = 10.0f;
    public float rotationSpeed = 20.0f;
    // PhotonView 컴포넌트 캐시처리를 위한 변수
    public PhotonView pv;
    // 시네머신 가상 카메라를 저장할 변수
    public CinemachineVirtualCamera virtualCamera;
    public GameObject bullet;
    public Rigidbody rb;
    public bool lookMe = true;

    // 수신된 위치와 회전값을 저장할 변수
    private UnityEngine.Vector3 receivePos;
    private UnityEngine.Quaternion receiveRot;
    // 수신된 좌표로 이동 및 회전 속도의 민감도
    public float damping = 10.0f;
    void Start()
    {

        pv = GetComponent<PhotonView>();
        virtualCamera = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
        rb = GetComponent<Rigidbody>();
        //PhotonView가 자신의 것일 경우 시네머신 가상카메라를 연결
        if (pv.IsMine)
        {
            virtualCamera.Follow = transform;
            virtualCamera.LookAt = transform;
            LookMe(false);
        }
    }

    void Update()
    {
        if (pv.IsMine)
        {
            if(Input.GetMouseButtonDown(1))
            {
                LookMe(true);
            }
            else if(Input.GetMouseButtonUp(1))
            {
                LookMe(false);
            }
            // 회전 입력 처리
            RotateTowardsMouse();
        }
        else
        {
            // 수신된 좌표로 보간한 이동처리
            transform.position = UnityEngine.Vector3.Lerp(transform.position,
            receivePos,
            Time.deltaTime * damping);

            // 수신된 회전값으로 보간한 회전처리
            transform.rotation = UnityEngine.Quaternion.Slerp(transform.rotation,
            receiveRot,
            Time.deltaTime * damping);

        }
    }
    private void LateUpdate()
    {
        if (pv.IsMine)
        {
            // 이동 입력 처리
            Move();
            if (Input.GetMouseButtonDown(0))
            {
                pv.RPC("Fire", RpcTarget.All, null);
            }
        }
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // 자신의 로컬 캐릭터인 경우 자신의 데이터를 다른 네트워크 유저에게 송신
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            receivePos = (UnityEngine.Vector3)stream.ReceiveNext();
            receiveRot = (UnityEngine.Quaternion)stream.ReceiveNext();
        }
    }
    public void LookMe(bool me)
    {
        if(me == true)
        {
            lookMe = true;
            virtualCamera.LookAt = transform;
        }
        else if(me == false)
        {
            lookMe = false;
            virtualCamera.LookAt = null;
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
        if (verticalInput > 0)
        {
            UnityEngine.Vector3 movement = new UnityEngine.Vector3(0f, 0f, verticalInput).normalized;

            // 현재 오브젝트의 전방 방향으로 이동 벡터를 변환합니다.
            movement = transform.TransformDirection(movement);
            rb.drag = 0f;
            // Rigidbody에 힘을 가해 물리적으로 이동
            rb.velocity = movement * moveSpeed;
        }
        else
        {
            rb.drag = 5f;
        }
    }
    [PunRPC]
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

