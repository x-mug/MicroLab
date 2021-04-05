using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject target;
    public float speed;
    private CharacterController charCon;
    private float horiInput;
    private float vertInput;


    // Start is called before the first frame update
    void Start()
    {
        charCon = this.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        horiInput = Input.GetAxisRaw("Horizontal");
        vertInput = Input.GetAxisRaw("Vertical");
        horiInput *= speed*Time.deltaTime;
        vertInput *= speed*Time.deltaTime;
        
        if(Vector3.Dot(this.transform.position, target.transform.position) < 0)
            this.transform.position = target.transform.position;
        
        Debug.Log(Vector3.Dot(this.transform.position, target.transform.position));
        charCon.Move(new Vector3(horiInput, 0, vertInput));
        
    }
}
