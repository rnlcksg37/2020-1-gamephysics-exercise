using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControl : MonoBehaviour
{
    [SerializeField]
    private Transform _agent = null;

    private Vector3 _pickPos = Vector3.zero;

    [SerializeField]
    private float _maxSpeed = 1.0f;

    [SerializeField]
    private float _mass = 1.0f;

    private Vector3 _velocity = Vector3.zero;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 mouse_pos = Input.mousePosition;

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenToWorldPoint(mouse_pos), -Vector3.up, out hit, 1000))
            {
                _pickPos = hit.point;
            }
        }

        // 1프레임만큼의 이동속도를 계산.
        _velocity = _velocity + (seek(_pickPos) * Time.deltaTime);

        // 속도를 기반으로 새로운 위치 계산.
        _agent.transform.position = _agent.transform.position + _velocity;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_pickPos, 1.0f);
    }

    private Vector3 seek(Vector3 target_pos)
    {
        Vector3 desired_velocity = ((target_pos - _agent.transform.position).normalized) * _maxSpeed;

        // y축의 값이 있을 수 있으므로, 최초에 0으로 만들어 y축의 값을 사용하지 않도록 한다.
        desired_velocity.y = 0.0f;

        return (desired_velocity - _velocity);
    }
}
