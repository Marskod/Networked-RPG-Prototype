var speed = 8.0;
var jumpSpeed = 15.0;
var gravity = 20.0;
var rotateSpeed = 3.0;
private var grounded : boolean = false;
private var moveDirection = Vector3.zero;
function Update() {
if(GetComponent.<NetworkView>().isMine)
{
var controller : CharacterController = GetComponent(CharacterController);
transform.Rotate(0, Input.GetAxis ("Horizontal") * rotateSpeed, 0);
var forward = transform.TransformDirection(Vector3.forward);
var curSpeed = speed * Input.GetAxis ("Vertical");
controller.SimpleMove(forward * curSpeed);
if (grounded) {
if (Input.GetButton ("Jump")) {

moveDirection.y = jumpSpeed;

}

}

moveDirection.y -= gravity * Time.deltaTime;

var flags = controller.Move(moveDirection * Time.deltaTime);

grounded = (flags & CollisionFlags.CollidedBelow) != 0;
}
}
