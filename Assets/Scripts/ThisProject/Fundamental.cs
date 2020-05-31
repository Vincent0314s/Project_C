using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Fundamental 
{
    public static void ComboForceForward(Rigidbody rb,float force) {
        rb.AddForce(rb.transform.forward * force);
    }
}
