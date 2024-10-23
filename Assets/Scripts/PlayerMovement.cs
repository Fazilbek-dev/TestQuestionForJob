using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float lookSensitivity = 2f;
    public float jumpForce = 5f;
    public float gravity = -9.81f;
    public Transform cameraTransform;
    public CharacterController controller;

    private float verticalVelocity;
    private Vector3 moveDirection;
    private float verticalLookRotationY = 0f;  // ��� ����������� �������� �� ��� Y
    private float verticalLookRotationX = 0f;  // ��� ����������� �������� �� ��� X

    private void Awake()
    {
        HideMouse();
    }

    void Update()
    {
        // ���������� ������� (�������� �� ���� X � Y)
        float mouseX = Input.GetAxis("Mouse X") * lookSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity;

        // �������� �� �������������� ��� (Y), ������� ���� ������
        transform.Rotate(Vector3.up, mouseX);
        transform.Rotate(Vector2.right, mouseY);

        // �������� �� ������������ ��� (X), �������� ���� ������
        verticalLookRotationY -= mouseY;
        verticalLookRotationX += mouseX;
        verticalLookRotationY = Mathf.Clamp(verticalLookRotationY, -90f, 90f); // ����������� ���� ������ �� ���������

        cameraTransform.localRotation = Quaternion.Euler(verticalLookRotationY, 0f, 0f); // ��� �������� �� ��� Z

        this.transform.localRotation = Quaternion.Euler(0f, verticalLookRotationX, 0f);

        // �������� ���������
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        moveDirection = transform.right * moveX + transform.forward * moveZ;
        moveDirection *= moveSpeed;

        // ������
        if (controller.isGrounded)
        {
            verticalVelocity = -1f; // ���� ���� �����, ����� �������������
            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = jumpForce;
            }
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        moveDirection.y = verticalVelocity;

        // �������� � ������ ����������
        controller.Move(moveDirection * Time.deltaTime);
    }

    // ������ ������ � ������������� ���
    public void HideMouse()
    {
        Cursor.lockState = CursorLockMode.Locked; // ��������� ������ � ������ ������
        Cursor.visible = false;                   // ������ ������ ���������
    }

    // �������� ������ � �������������� ���
    public void ShowMouse()
    {
        Cursor.lockState = CursorLockMode.None;   // ������������ ������, �� �������� ������������
        Cursor.visible = true;                    // ������ ������ �������
    }
}