using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walls : MonoBehaviour
{
    private FightBall _fightBall;
    private void Start()
    {
        _fightBall = GameObject.Find("FightBall").GetComponent<FightBall>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "FightBall")
        {
            _fightBall.InverseVelocityX();
        }
    }
}
