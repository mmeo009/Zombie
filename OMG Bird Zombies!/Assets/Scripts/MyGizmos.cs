using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGizmos : MonoBehaviour
{
    public Color _color = Color.cyan;
    public float _radius = 1.0f;
    void OnDrawGizmos()
    {
        // ����� ���� ����
        Gizmos.color = _color;
        // ��ü ����� ����� ����. ���ڴ� (���� ��ġ, ������)
        Gizmos.DrawSphere(transform.position, _radius);
    }
}
