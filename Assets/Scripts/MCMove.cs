using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCMove : MonoBehaviour
{
    private Transform _mc;
    private Vector3 _velocity;
    IEnumerator Move()
    {
        _mc.position += _velocity * Time.deltaTime;
        yield return new WaitForFixedUpdate();
    }

    private void Start()
    {
        _mc = gameObject.GetComponent<Transform>();
        var throne = GameObject.Find("Throne").transform.position;
        _velocity = (throne - _mc.position).normalized * 5f;
    }

    private void FixedUpdate()
    {
        StartCoroutine(Move());
    }
}
