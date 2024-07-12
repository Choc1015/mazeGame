using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float _speed;

    Rigidbody _rigidbody;

    float inputX;
    float inputY;
    float _saveSpeed;
    private void Awake()
    {
        _saveSpeed = _speed;
        _rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {

        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(inputX, 0, inputY) * _speed;

        _rigidbody.velocity = movement;

    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Spike"))
        {
            _speed = 0;
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            _speed = _saveSpeed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Finish"))
        {
            Debug.Log("³¡");
        }
    }

}
