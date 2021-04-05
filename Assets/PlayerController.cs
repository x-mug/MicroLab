using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// Public 变量
    /// </summary>

    public float moveSpeed;
    public float jumpSpeed;
    public float mouseSensitivity;


    /// <summary>
    /// private 变量
    /// </summary>

    // 这里涉及三维向量
    private Vector3 moveInput;

    // 这里涉及四元数与欧拉角
    private Vector3 rotateInput;

    // 这是一个刚体组件
    private Rigidbody rb;
    private bool isGrounded;

    // 这是我们要用的相机（其实可以不用，后续会解释）
    private Camera cam;

    private void Start()
    {
        // 获得所需要的组件
        rb = this.GetComponent<Rigidbody>();

        // 初始化一些变量
        moveInput = new Vector3();
        rotateInput = new Vector3();
    }

    void Update()
    {
        // 输入获取
        moveInput.Set(
            Input.GetAxisRaw("Horizontal"), // x轴， 在左手笛卡尔坐标系中，代表左右
            Input.GetAxisRaw("Jump"),       // y轴， 在左手笛卡尔坐标系中，代表上下
            Input.GetAxisRaw("Vertical")    // z轴， 在左手笛卡尔坐标系中，代表前后
        );

        // 这里我们使用欧拉角转换为四元数的方式来简单使用
        // 关于四元数，这里有个很棒的视频可以帮助到你：https://www.bilibili.com/video/BV1SW411y7W1
        rotateInput.Set(
            Input.GetAxis("Mouse Y"), // x, 这里的x，即围绕笛卡尔坐标系的x轴旋转，因此关乎 上下 旋转
            Input.GetAxis("Mouse X"), // y，这里的y，即围绕笛卡尔坐标系的y轴旋转，因此关乎 左右 旋转
            0                         // z，这里的z，即围绕笛卡尔坐标系的z轴旋转，因此关乎 偏转 旋转, 例如：吃鸡的偏头
        );

        // 进行输入处理
        moveInput.Set(
            moveInput.x * moveSpeed,
            moveInput.y * jumpSpeed != 0 ? moveInput.y * jumpSpeed : rb.velocity.y,
            moveInput.z * moveSpeed
        );

        rotateInput.Set(
            -1 * rotateInput.x * mouseSensitivity * Time.deltaTime,
            rotateInput.y * mouseSensitivity * Time.deltaTime,
            0
        );

        // 输入应用
        
        // 为刚体施加速度
        rb.velocity =
                this.transform.right * moveInput.x +
                this.transform.up * moveInput.y +
                this.transform.forward * moveInput.z;
        
        // 人物模型进行左右旋转
        this.transform.rotation = Quaternion.Euler(
            this.transform.rotation.eulerAngles.x,
            this.transform.rotation.eulerAngles.y + rotateInput.y,
            this.transform.rotation.eulerAngles.z
        );

        // 单独相机进行上下旋转
        Camera.main.transform.rotation = Quaternion.Euler(
            Camera.main.transform.rotation.eulerAngles.x + rotateInput.x,
            Camera.main.transform.rotation.eulerAngles.y,
            Camera.main.transform.rotation.eulerAngles.z
        );
    }
}
