﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSuperShield : MonoBehaviour
{
    private float _speed = 0.5f;

    [SerializeField]
    private AudioClip _audioClip;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }

    //EVENTS
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (!player)
        {
            return;
        }

        AudioSource.PlayClipAtPoint(_audioClip, Camera.main.transform.position);

        player.SuperShieldOn();

        Destroy(this.gameObject);
    }
}
