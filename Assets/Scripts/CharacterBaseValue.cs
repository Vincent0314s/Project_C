using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBaseValue : MonoBehaviour
{
    public Characters characters;
    public float maxHP = 100f;
    private float currentHP;

    private void Start()
    {
        currentHP = maxHP;
    }

}
