var speed = 8.0;
var jumpSpeed = 15.0;
var gravity = 20.0;
var rotateSpeed = 3.0;
var currSpeed : float = 0.0; 
private var grounded : boolean = false;
private var moveDirection = Vector3.zero;

var anim : Animator;

function Start () 
{
    anim = GetComponent("Animator");
}


function Update() {

    if(GetComponent.<NetworkView>().isMine)
    {
        var controller : CharacterController = GetComponent(CharacterController);
        transform.Rotate(0, Input.GetAxis ("Horizontal") * rotateSpeed, 0);
        var forward = transform.TransformDirection(Vector3.forward);
    
        currSpeed = speed * Input.GetAxis ("Vertical");

        controller.SimpleMove(forward * currSpeed);

        GetComponent.<NetworkView>().RPC("NetworkAnimate", RPCMode.All, 0); 

        if (grounded) {

            if (Input.GetButton ("Jump")) {
                moveDirection.y = jumpSpeed;
            }

            moveDirection.y -= gravity * Time.deltaTime;

            var flags = controller.Move(moveDirection * Time.deltaTime);

            grounded = (flags & CollisionFlags.CollidedBelow) != 0;
        }
    }
}

/*@RPC
function SendFloat(name:String, value:float){
    //animator.SetFloat(name, value);
    anim.SetFloat("Speed", currSpeed);

}*/

@RPC
function NetworkAnimate (anims:int) {
    anim.SetFloat("Speed", currSpeed);
}