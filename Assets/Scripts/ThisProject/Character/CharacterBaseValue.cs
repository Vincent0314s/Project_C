using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBaseValue : MonoBehaviour
{
    public Characters characters;
    public float maxHP = 100f;
    private float currentHP;

    public Animator anim { get; private set; }
    public Rigidbody rb { get; private set; }

    //Boolean Property
    public bool isDodging { get; set; }
    public bool isDefending { get; set; }


    private void Start()
    {
        currentHP = maxHP;
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
    }

}
