using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Fundamental 
{
    public static void AddForceLocalZ(Rigidbody rb,float force) {
        rb.AddForce(rb.transform.forward * force);
    }

    public static void AddForceLocalX(Rigidbody rb, float force) {
        rb.AddForce(rb.transform.right * force);
    }

    public static bool IsCloseTarget(Transform _self,Transform _target,float stoppedDistance) {
        float dis = Vector3.Distance(_self.position,_target.position);
        if (dis > stoppedDistance)
        {
            return false;
        }
        else {
            return true;
        }
    }
}
