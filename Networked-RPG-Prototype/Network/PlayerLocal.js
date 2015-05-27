private var p : Vector3;
private var r : Quaternion;
private var m : int = 0;
 
function Start() {
    GetComponent.<NetworkView>().observed = this;
}
 
function OnSerializeNetworkView(stream : BitStream) {
    p = GetComponent.<Rigidbody>().position;
    r = GetComponent.<Rigidbody>().rotation;
    m = 0;
    stream.Serialize(p);
    stream.Serialize(r);
    stream.Serialize(m);
}