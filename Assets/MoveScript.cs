using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour {
    public float MovementSpeed = 1400f;
    public float RotationSpeed = 20f;
    Quaternion zero;
    Rigidbody myRigid;
	// Use this for initialization
	void Start () {
        myRigid = GetComponent<Rigidbody>();
        zero = transform.rotation;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 dir = Vector3.zero;
#if UNITY_IPHONE || UNITY_ANDROID || UNITY_BLACKBERRY || UNITY_WP8
#if UNITY_3_5 && UNITY_ANDROID
                dir.x = Mathf.Clamp(-Input.acceleration.y * 2f, -1f, 1f);
                dir.z = 1f;
#else
                dir.x = Mathf.Clamp(Input.acceleration.x * 2f, -1f, 1f);
                dir.z = 1f;
#endif
#else
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.z = Input.GetAxisRaw("Vertical");
#endif

        // Move backwards at half speed
        float speed = dir.z > 0f ? dir.z : dir.z * 0.5f;

        // Apply movement
        Vector3 force = new Vector3(transform.right.x, 0f, transform.right.z) * speed * MovementSpeed;
        GetComponent<Rigidbody>().AddForce(force * Time.deltaTime, ForceMode.VelocityChange);

        // Apply rotation
        GetComponent<Rigidbody>().AddTorque(0f, dir.x * RotationSpeed * Time.deltaTime, 0f, ForceMode.VelocityChange);

        if (Input.GetKey(KeyCode.Space))
        {
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        }
    }
}
