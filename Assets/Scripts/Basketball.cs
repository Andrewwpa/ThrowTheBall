using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basketball : MonoBehaviour
{
    [SerializeField] float speed = 10;
    [SerializeField] Transform ball;
    [SerializeField] Transform arms;
    [SerializeField] Transform positionOverHead;
    [SerializeField] Transform positionDribble;

    private bool _isBallInHands = true;

    void Start()
    {
        
    }

    void Update()
    {
        //���������� �� wasd, �������� ����������� �� ������� ������
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        //�������� ���������
        transform.position += direction * speed * Time.deltaTime;
        //������� ��������� �� �����������
        transform.LookAt(transform.position + direction);

        if (_isBallInHands)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                ball.position = positionOverHead.position;
            }
            else
            {
                ball.position = positionDribble.position + Vector3.up * Mathf.Abs(Mathf.Sin(Time.time * 5));
            }
        }




    }
}
