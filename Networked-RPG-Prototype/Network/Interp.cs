using UnityEngine;
using System.Collections;

public class Interp : MonoBehaviour {

    void Example()
    {
        GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;
    }
}
