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
    private float verticalLookRotationY = 0f;  // Для ограничения поворота по оси Y
    private float verticalLookRotationX = 0f;  // Для ограничения поворота по оси X

    private void Awake()
    {
        HideMouse();
    }

    void Update()
    {
        // Управление камерой (вращение по осям X и Y)
        float mouseX = Input.GetAxis("Mouse X") * lookSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity;

        // Вращение по горизонтальной оси (Y), вращаем весь объект
        transform.Rotate(Vector3.up, mouseX);
        transform.Rotate(Vector2.right, mouseY);

        // Вращение по вертикальной оси (X), изменяем угол камеры
        verticalLookRotationY -= mouseY;
        verticalLookRotationX += mouseX;
        verticalLookRotationY = Mathf.Clamp(verticalLookRotationY, -90f, 90f); // Ограничение угла обзора по вертикали

        cameraTransform.localRotation = Quaternion.Euler(verticalLookRotationY, 0f, 0f); // Без вращения по оси Z

        this.transform.localRotation = Quaternion.Euler(0f, verticalLookRotationX, 0f);

        // Движение персонажа
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        moveDirection = transform.right * moveX + transform.forward * moveZ;
        moveDirection *= moveSpeed;

        // Прыжок
        if (controller.isGrounded)
        {
            verticalVelocity = -1f; // Чуть ниже земли, чтобы приклеиваться
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

        // Движение с учётом гравитации
        controller.Move(moveDirection * Time.deltaTime);
    }

    // Скрыть курсор и заблокировать его
    public void HideMouse()
    {
        Cursor.lockState = CursorLockMode.Locked; // Блокирует курсор в центре экрана
        Cursor.visible = false;                   // Делает курсор невидимым
    }

    // Показать курсор и разблокировать его
    public void ShowMouse()
    {
        Cursor.lockState = CursorLockMode.None;   // Разблокирует курсор, он свободно перемещается
        Cursor.visible = true;                    // Делает курсор видимым
    }
}