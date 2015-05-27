using UnityEngine;
using System.Collections;

public class ObjectCollision : MonoBehaviour
{
    public float pushPower = 8.0F;
    private float timer = 3.0f;
    private bool shot = false; 
    void Update()
    {
        if(Input.GetKeyDown("f"))
        {
            timer = 3.0f;
            pushPower = 25.0f;
            shot = true;
        }
        if(shot == true)
            timer -= Time.deltaTime; 

        if(timer < 0)
        {
            shot = false;
            pushPower = 8.0f;
            timer = -1.0f;
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body == null || body.isKinematic)
            return;

        if (hit.moveDirection.y < -0.3F)
            return;

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        body.velocity = pushDir * pushPower;
    }
}