using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speed = 0.5f;
    public float jumpPower = 10f;

    private void Update() {
        if (Input.GetKey(KeyCode.UpArrow)) {
            gameObject.transform.Translate(new Vector3(1, 0, 0)* speed *Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            gameObject.transform.Translate(new Vector3(-1, 0, 0) * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            gameObject.transform.Translate(new Vector3(0, 0, 1) * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            gameObject.transform.Translate(new Vector3(0, 0, -1) * speed * Time.deltaTime);
        }
        if (Input.GetKeyUp(KeyCode.Space)) {
            gameObject.GetComponentInChildren<Rigidbody>().AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
        }


    }



}
