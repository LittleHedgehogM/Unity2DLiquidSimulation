using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;             // 跟随的角色
    public Vector3 offset = new Vector3(0, 3, -5);  // 相机偏移
    public float followSpeed = 10f;      // 跟随平滑度

    void LateUpdate()
    {
        if (target == null) return;

        // 计算目标位置
        Vector3 desiredPosition = target.position + target.rotation * offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

        // 始终看向角色
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}
