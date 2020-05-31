using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvent : BasicAnimationEvent
{
    private CharacterBaseValue cbv;

    void Start()
    {
        cbv = GetComponentInParent<CharacterBaseValue>();
    }

    public void CharacterForceForward(float force) {
        Fundamental.ComboForceForward(cbv.rb,force);
    }
}
