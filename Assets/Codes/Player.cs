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
            if (AudioManager.Instance.soundEffect.isPlaying == false)
                AudioManager.Instance.soundEffect.Play();
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            _speed = _saveSpeed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Point"))
        {
            if(AudioManager.Instance.soundEffect.isPlaying == false)
            AudioManager.Instance.soundEffect.Play();
            UIManager.Instance.point += 100;
            Destroy(other.gameObject);
            UIManager.Instance.updatePoint();
        }
        if (other.gameObject.CompareTag("Time"))
        {
            if (AudioManager.Instance.soundEffect.isPlaying == false)
                AudioManager.Instance.soundEffect.Play();
            UIManager.sec += 15;
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Finish"))
        {
            if (AudioManager.Instance.soundEffect.isPlaying == false)
                AudioManager.Instance.soundEffect.Play();
            UIManager.Instance.ActivePannel();
        }

        if (other.gameObject.CompareTag("Path"))
        {
            StartCoroutine(GenerationWall.Instance.destroyTimePath(transform.position));
            Destroy(other.gameObject);
        }

    }

}
