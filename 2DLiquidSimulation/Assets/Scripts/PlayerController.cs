using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 3f;          // 移动速度
    public float rotationSpeed = 10f;     // 旋转速度
    public Animator animator;            // 角色动画控制器

    private CharacterController controller;
    private Camera mainCamera;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        // 获取输入
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 inputDir = new Vector3(h, 0, v);

        // 相机方向转换（让前后方向跟随相机）
        Vector3 camForward = mainCamera.transform.forward;
        Vector3 camRight = mainCamera.transform.right;
        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 moveDir = camForward * v + camRight * h;

        // 如果有移动输入
        if (moveDir.magnitude > 0.1f)
        {
            // 移动
            controller.Move(moveDir.normalized * moveSpeed * Time.deltaTime);

            // 旋转角色朝向
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // 动画切换为 Walk
            if (animator != null)
            {
                animator.SetBool("isWalking", true);
            }
        }
        else
        {
            // Idle
            if (animator != null)
            {
                animator.SetBool("isWalking", false);
            }
        }
    }
}
