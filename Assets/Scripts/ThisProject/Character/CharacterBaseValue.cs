using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttackBasicValue
{
    public AttackType type;
    public float damage;
    public AttackBasicValue(AttackType _type, float _damage)
    {
        type = _type;
        damage = _damage;
    }
}

public class CharacterBaseValue : MonoBehaviour
{
    public Characters characters;
    public float maxHP = 100f;
    private float currentHP;
    public float damage;
    [ArrayElementTitle("type")]
    public List<AttackBasicValue> attacksSetting = new List<AttackBasicValue> {
        new AttackBasicValue(AttackType.LightAttack,11),
        new AttackBasicValue(AttackType.HeavyAttack,22),
        new AttackBasicValue(AttackType.DownAttack,30),
        new AttackBasicValue(AttackType.DefenceBreakAttack,3)
    };

    public Animator anim { get; private set; }
    public Rigidbody rb { get; private set; }
    public Transform target;
    public Transform cam;

    //Boolean Property
    public bool isDodging { get; set; }
    public bool isDefending { get; set; }
    public bool isLockingTarget { get; set; }

    private void Start()
    {
        currentHP = maxHP;
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        if (characters != Characters.Player) {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    public float GetDamageFromAttackType(AttackType _type)
    {
        for (int i = 0; i < attacksSetting.Count; i++)
        {
            if (attacksSetting[i].type == _type)
            {
                damage = attacksSetting[i].damage;
            }
        }
        return damage;
    }

    public bool IsDead() {
        return currentHP <= 0;
    }

    public bool IsInCurrentAnimationState(AnimationTag _tag) {
        if (anim.GetCurrentAnimatorStateInfo(0).IsTag(_tag.ToString()))
        {
            return true;
        }
        return false;
    }

    public bool IsInOriginalAnimationState() {
        if (IsInCurrentAnimationState(AnimationTag.Idle) || IsInCurrentAnimationState(AnimationTag.Movement)) {
            return true;
        }
        return false;
    }

    public float GetCurrentAnimationStateNormalizedTime() {
        return anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    public void CameraLookAtTarget() {
        if (target!=null) {
            cam.LookAt(target);
        }
    }
}
