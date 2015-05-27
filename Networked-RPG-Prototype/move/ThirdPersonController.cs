/*var turnSpeed = 10.0;
var moveSpeed = 10.0;
var mouseTurnMultiplier = 1;
 
private var x : float;
private var z : float;
function Update ()
{
    // x is used for the x axis.  set it to zero so it doesn't automatically rotate
    x = 0;
   
    // check to see if the W or S key is being pressed.  
    z = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
   
    // Move the character forwards or backwards
    transform.Translate(0, 0, z);
           
    // Check to see if the A or S key are being pressed
    if (Input.GetAxis("Horizontal"))
    {
        // Get the A or S key (-1 or 1)
        x = Input.GetAxis("Horizontal");
    }
   
    // Check to see if the right mouse button is pressed
    if (Input.GetMouseButton(1))
    {
        // Get the difference in horizontal mouse movement
        x = Input.GetAxis("Mouse X") * turnSpeed * mouseTurnMultiplier;
    }
 
    // rotate the character based on the x value
    transform.Rotate(0, x, 0);
}*/