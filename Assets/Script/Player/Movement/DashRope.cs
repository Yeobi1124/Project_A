using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashRope : MonoBehaviour
{
    private int _power;
    private int _dir;
    private Rigidbody2D _rb;
    private Rigidbody2D rb
    {
        get
        {
            if (_rb == null)
            {
                _rb = GetComponent<Rigidbody2D>();
            }
            return _rb;
        }
    }

    private void Start()
    {
        
    }

    public void Init(int power)
    {
        _power = power;
    }

    public void SetDir(int dir)
    {
        _dir = dir;
    }

    public void Act(Vector3 anchorPos)
    {
        Vector3 forceDir = Quaternion.Euler(0, 0, -90) * (anchorPos - transform.position).normalized;
        rb.AddForce(forceDir * _dir * _power, ForceMode2D.Impulse);
    }
}
