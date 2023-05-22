using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Photon.Pun;

public class TempNetworkPlayer : MonoBehaviour
{
    private float moveSpeed = 6; // move speed
    private float turnSpeed = 90; // turning speed (degrees/second)
    private float lerpSpeed = 10; // smoothing speed
    private float gravity = 10; // gravity acceleration
    private bool isGrounded;
    private float deltaGround = 0.2f; // character is grounded up to this distance
    private float jumpSpeed = 10; // vertical jump initial speed
    private float jumpRange = 10; // range to detect target wall
    private Vector3 surfaceNormal; // current surface normal
    private Vector3 myNormal; // character normal
    private float distGround; // distance from character position to ground
    private bool jumping = false; // flag &quot;I'm jumping to wall&quot;
    private float vertSpeed = 0; // vertical jump current speed

    private Transform myTransform;
    
    public BoxCollider boxCollider; // drag BoxCollider ref in editor

    public XRNode inputSource;
    private Vector2 inputAxis;
    public PhotonView photonView;
    public Transform body;
    public Transform lArm;
    public Transform rArm;

    private Transform leftHandRig;
    private Transform rightHandRig;
    private Camera _camera; // player camera
    private AudioListener _audioListener; // audio controller
    [SerializeField] private Animator animator; //DEBUG network animator
    [SerializeField] private GameObject _model; //DEBUG Model

    private void Start()
    {
        myNormal = transform.up; // normal starts as character up direction
        myTransform = transform;
        GetComponent<Rigidbody>().freezeRotation = true; // disable physics rotation
                                                         // distance from transform.position to ground
        distGround = boxCollider.size.y - boxCollider.center.y;
        photonView = GetComponent<PhotonView>();
        _camera = GetComponentInChildren<Camera>(); //only activates the camera of the player that ownes the object
        _audioListener = GetComponentInChildren<AudioListener>();;

        if (photonView.IsMine)
        {
            _camera.enabled = true;
            _audioListener.enabled = true;
            _model.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            // apply constant weight force according to character normal:
            GetComponent<Rigidbody>().AddForce(-gravity * GetComponent<Rigidbody>().mass * myNormal);
        }
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
            device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);


            // jump code - jump to wall or simple jump
            if (jumping) return; // abort Update while jumping to a wall

            Ray ray;
            RaycastHit hit;

            if (Input.GetButtonDown("Jump"))
            { // jump pressed:
                ray = new Ray(myTransform.position, myTransform.forward);
                if (Physics.Raycast(ray, out hit, jumpRange))
                { // wall ahead?
                    JumpToWall(hit.point, hit.normal); // yes: jump to the wall
                }
                else if (isGrounded)
                { // no: if grounded, jump up
                    GetComponent<Rigidbody>().velocity += jumpSpeed * myNormal;
                }
            }

            // movement code - turn left/right with Horizontal axis:
            myTransform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);
            // update surface normal and isGrounded:
            ray = new Ray(myTransform.position, -myNormal); // cast ray downwards
            if (Physics.Raycast(ray, out hit))
            { // use it to update myNormal and isGrounded
                isGrounded = hit.distance <= distGround + deltaGround;
                surfaceNormal = hit.normal;
            }
            else
            {
                isGrounded = false;
                // assume usual ground normal to avoid "falling forever"
                surfaceNormal = Vector3.up;
            }
            myNormal = Vector3.Lerp(myNormal, surfaceNormal, lerpSpeed * Time.deltaTime);
            // find forward direction with new myNormal:
            Vector3 myForward = Vector3.Cross(myTransform.right, myNormal);
            // align character to the new myNormal while keeping the forward direction:
            Quaternion targetRot = Quaternion.LookRotation(myForward, myNormal);
            myTransform.rotation = Quaternion.Lerp(myTransform.rotation, targetRot, lerpSpeed * Time.deltaTime);
            // move the character forth/back with Vertical axis:
            myTransform.Translate(0, 0, Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime);
            
            MapPosition(body, transform);
            MapPosition(lArm, transform);
            MapPosition(rArm, transform);
        }
    }

    void MapPosition(Transform target, Transform rigTransform)
    {
        target.SetPositionAndRotation(rigTransform.position, rigTransform.rotation);
    }

    private void JumpToWall(Vector3 point, Vector3 normal)
    {
        // jump to wall
        jumping = true; // signal it's jumping to wall
        GetComponent<Rigidbody>().isKinematic = true; // disable physics while jumping
        Vector3 orgPos = myTransform.position;
        Quaternion orgRot = myTransform.rotation;
        Vector3 dstPos = point + normal * (distGround + 0.5f); // will jump to 0.5 above wall
        Vector3 myForward = Vector3.Cross(myTransform.right, normal);
        Quaternion dstRot = Quaternion.LookRotation(myForward, normal);

        StartCoroutine(jumpTime(orgPos, orgRot, dstPos, dstRot, normal));
        //jumptime
    }

    private IEnumerator jumpTime(Vector3 orgPos, Quaternion orgRot, Vector3 dstPos, Quaternion dstRot, Vector3 normal)
    {
        for (float t = 0.0f; t < 1.0f;)
        {
            t += Time.deltaTime;
            myTransform.position = Vector3.Lerp(orgPos, dstPos, t);
            myTransform.rotation = Quaternion.Slerp(orgRot, dstRot, t);
            yield return null; // return here next frame
        }
        myNormal = normal; // update myNormal
        GetComponent<Rigidbody>().isKinematic = false; // enable physics
        jumping = false; // jumping to wall finished

    }
}
