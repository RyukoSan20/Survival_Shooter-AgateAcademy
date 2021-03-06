using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;

    private float _normalSpeed;
    private Vector3 _movement;
    private Animator _animator;
    private Rigidbody _playerRigidbody;
    private int _floorMask;
    private float _camRayLength = 100f;
    private static readonly int IsWalking = Animator.StringToHash("IsWalking");


    private void Awake()
    {
        _normalSpeed = speed;
        _floorMask = LayerMask.GetMask("Floor");
        _animator = GetComponent<Animator>();
        _playerRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Turning();
    }

    public void Move(float horizontal, float vertical)
    {
        if (GetComponent<PlayerHealth>().currentHealth <= 0)
        {
            return;
        }
        
        _movement.Set(horizontal, 0f, vertical);

        _movement = _movement.normalized * speed * Time.deltaTime;
        
        _playerRigidbody.MovePosition(transform.position + _movement);
    }

    public void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, _camRayLength, _floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            
            _playerRigidbody.MoveRotation(newRotation);
        }
    }

    public void Animating(float h, float v)
    {
        bool walking = h != 0 || v != 0f;
        _animator.SetBool(IsWalking, walking);
    }

    public void SpeedUp(float speedUpAmount, float speedupTime)
    {
        speed *= speedUpAmount;

        StartCoroutine(ResetSpeed(speedupTime));
    }

    private IEnumerator ResetSpeed(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        speed = _normalSpeed;
    }
}
