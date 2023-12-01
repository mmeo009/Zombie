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
    public PlayerData data;
    public float rotationSpeed = 20.0f;
    // PhotonView ������Ʈ ĳ��ó���� ���� ����
    public PhotonView pv;
    // �ó׸ӽ� ���� ī�޶� ������ ����
    public CinemachineVirtualCamera virtualCamera;
    public GameObject bullet;
    public Rigidbody rb;
    public bool lookMe = true;
    public float coolTime;

    // ���ŵ� ��ġ�� ȸ������ ������ ����
    private UnityEngine.Vector3 receivePos;
    private UnityEngine.Quaternion receiveRot;
    // ���ŵ� ��ǥ�� �̵� �� ȸ�� �ӵ��� �ΰ���
    public float damping = 10.0f;
    void Start()
    {
        data = GetComponent<PlayerData>();
        pv = GetComponent<PhotonView>();
        virtualCamera = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
        rb = GetComponent<Rigidbody>();
        //PhotonView�� �ڽ��� ���� ��� �ó׸ӽ� ����ī�޶� ����
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
            // ȸ�� �Է� ó��
            RotateTowardsMouse();
            if(coolTime > 0)
            {
                coolTime -= Time.deltaTime;
            }
        }
        else
        {
            // ���ŵ� ��ǥ�� ������ �̵�ó��
            transform.position = UnityEngine.Vector3.Lerp(transform.position,
            receivePos,
            Time.deltaTime * damping);

            // ���ŵ� ȸ�������� ������ ȸ��ó��
            transform.rotation = UnityEngine.Quaternion.Slerp(transform.rotation,
            receiveRot,
            Time.deltaTime * damping);

        }
    }
    private void LateUpdate()
    {
        if (pv.IsMine)
        {
            // �̵� �Է� ó��
            Move();
            if(Input.GetMouseButtonDown(0))
            {
                pv.RPC("Fire", RpcTarget.All, null);
            }
            else if (Input.GetMouseButton(0))
            {
                pv.RPC("FireCoolDown", RpcTarget.All, null);
            }
        }
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // �ڽ��� ���� ĳ������ ��� �ڽ��� �����͸� �ٸ� ��Ʈ��ũ �������� �۽�
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

            // ���� ������Ʈ�� ���� �������� �̵� ���͸� ��ȯ�մϴ�.
            movement = transform.TransformDirection(movement);
            rb.drag = 0f;
            // Rigidbody�� ���� ���� ���������� �̵�
            rb.velocity = movement * data.moveSpeed;
        }
        else
        {
            rb.drag = 5f;
        }
    }
    [PunRPC]
    public void FireCoolDown()
    {
        if(coolTime <= 0)
        {
            pv.RPC("Fire", RpcTarget.All, null);
            coolTime = data.bulletCoolDown;
        }
    }
    [PunRPC]
    public void Fire()
    {
        int amount = data.bulletLevel;

        if (bullet == null)
        {
            bullet = Resources.Load<GameObject>("Prefabs/Bullet");
        }

        for (int i = 0; i < amount; i++)
        {
            float axis;

            if (i % 2 == 0)
            {
                axis = -(45f / amount) * i;
            }
            else
            {
                axis = (45f / amount) * i;
            }

            GameObject temp = Instantiate(bullet, firePos.position, firePos.rotation);
            temp.GetComponent<Bullet>().BulletSetting(data.bulletSize, data.bulletSpeed, axis);
        }
    }
}

