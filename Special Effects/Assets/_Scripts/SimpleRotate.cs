using UnityEngine;

public class SimpleRotate : MonoBehaviour
{
    [SerializeField] private float rotateZAmount = 15f;
    [SerializeField] private float rotateSpeed = 4;

    private float rotationZ = 0f;
    private bool rotateLeft;
    private float rotateMinZ;

    private void Awake()
    {
        rotateMinZ = rotateZAmount * -1f;
    }

    private void Update()
    {
        if (rotateLeft == true)
        {
            float angleZ = this.transform.localEulerAngles.z - (Time.deltaTime * rotateSpeed);
            this.transform.localEulerAngles = new Vector3(0f, 0f, angleZ);
            angleZ = ClampAngle(this.transform.localEulerAngles.z, rotateMinZ, rotateZAmount);

            if (angleZ >= rotateZAmount) {
                rotateLeft = false;
            }
        }
        else
        {
            float angleZ = this.transform.localEulerAngles.z + (Time.deltaTime * rotateSpeed);
            this.transform.localEulerAngles = new Vector3(0f, 0f, angleZ);
            angleZ = ClampAngle(this.transform.localEulerAngles.z, rotateMinZ, rotateZAmount);

            if (angleZ >= rotateZAmount) {
                rotateLeft = true;
            }
        }
    }

    float ClampAngle(float angle, float from, float to)
    {
        if (angle > 180) angle = 360 - angle;
        angle = Mathf.Clamp(angle, from, to);
        if (angle < 0) angle = 360 + angle;
        return angle;
    }
}
