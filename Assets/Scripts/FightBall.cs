using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightBall : MonoBehaviour
{
    private Vector3 _velocity;
    private bool _isShooted;
    
    public bool GetIsShooted => _isShooted;

    public void InverseVelocityX()
    {
        _velocity.x = -_velocity.x;
    }

    public void SetDefault()
    {
        transform.position = new Vector3(-100f, -100f, 0f);
        transform.localScale = new Vector3(0.1f, 0.1f);
        _isShooted = false;
    }
    private void Start()
    {
        _velocity = new Vector3(0f, 0f, 90f);
        _isShooted = false;
    }

    private void Update()
    {
        if (_isShooted)
            transform.position += new Vector3(_velocity.x * Time.deltaTime,
                                              _velocity.y * Time.deltaTime,
                                              0f);
    }

    public void SetVelocity(bool isShooted, Vector3 velocity)
    {
        _velocity = velocity;
        _isShooted = isShooted;
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name != "MC")
            this.SetDefault();
    }
}
