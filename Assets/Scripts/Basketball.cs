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
    [SerializeField] Transform target;

    private bool _isBallInHands = true;
    private bool _isBallFlying= false;
    float timeOfFly;

    void Update()
    {
        //управление на wasd, получаем направление по нажатию кнопок
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        //смещение персонажа
        transform.position += direction * speed * Time.deltaTime;
        //поворот персонажа по направлению
        transform.LookAt(transform.position + direction);

        if (_isBallInHands)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                ball.position = positionOverHead.position;
                arms.localEulerAngles = Vector3.right * 210;

                transform.LookAt(target.position);
            }
            else
            {
                ball.position = positionDribble.position + Vector3.up * Mathf.Abs(Mathf.Sin(Time.time * 5));
                arms.localEulerAngles = Vector3.right * 0;
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                _isBallInHands = false;
                _isBallFlying = true;
            }
        }

        if (_isBallFlying)
        {
            timeOfFly +=Time.deltaTime;
            float duration = 0.5f;
            float t1 = timeOfFly / duration;

            Vector3 a = positionOverHead.position;
            Vector3 b = target.position;
            Vector3 pos = Vector3.Lerp(a, b, t1);

            Vector3 arc = Vector3.up * 5 * Mathf.Sin(t1 * Mathf.PI);

            ball.position = pos + arc;

            if (t1 >= 1)
            {
                timeOfFly = 0;
                _isBallFlying = false;
                ball.GetComponent<Rigidbody>().isKinematic = false;
            }

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_isBallFlying && !_isBallInHands)
        {
            _isBallInHands = true;
            ball.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
