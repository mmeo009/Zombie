using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CM : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float rotationSpeed = 5.0f;

    // 컴포넌트 캐시 처리를 위한 변수
    private new Transform transform;
    private new Camera camera;
    // 가상의 Plane에 레이캐스팅하기 위한 변수
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
        // 좌우 이동 입력 처리
        Move();

        // 마우스 위치를 기준으로 회전 처리
        Turn();
    }

    // 회전 처리하는 함수
    void Turn()
    {
        // 마우스의 2차원 좌푯값을 이용해 3차원 광선(레이)를 생성
        ray = camera.ScreenPointToRay(Input.mousePosition);
        float enter = 0.0f;
        // 가상의 바닥에 레이를 발사해 충돌한 지점의 거리를 enter
        // 변수로 반환
        plane.Raycast(ray, out enter);
        // 가상의 바닥에 레이가 충돌한 좌푯값 추출
        hitPoint = ray.GetPoint(enter);
        // 회전해야 할 방향의 벡터를 계산
        Vector3 lookDir = hitPoint - transform.position;
        lookDir.y = 0;
        // 주인공 캐릭터의 회전값 지정
        transform.localRotation = Quaternion.LookRotation(lookDir);
    }

    void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // 이동 방향 계산
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
        movement.Normalize(); // 방향 벡터를 정규화하여 대각선 이동 시에도 일정한 속도로 이동하도록 합니다.

        // 이동 벡터를 카메라가 바라보는 방향으로 변환
        movement = Camera.main.transform.TransformDirection(movement);

        // y 축 값은 0으로 설정하여 수직 이동을 방지
        movement.y = 0f;

        // 플레이어 이동
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
    }
}
