using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    [SerializeField] private DoorTrigger _doorTrigger;

    public float rotationSpeed = 5f;  // —корость вращени€
    public float maxRotation = 45f;   // ћаксимальное значение поворота дл€ открытой двери
    private float initialRotationY;   // Ќачальный угол поворота двери (когда она закрыта)
    private Vector3 targetRotation;   // ÷елева€ ориентаци€

    private bool _isOpen;

    private void Start()
    {
        // «апоминаем начальный угол поворота двери (по оси Y)
        initialRotationY = transform.eulerAngles.y;

        // »значально целева€ ориентаци€ совпадает с начальной ориентацией объекта
        targetRotation = transform.eulerAngles;
    }

    /// <summary>
    /// ѕоворачивает объект на указанный угол по оси Y с ограничением.
    /// </summary>
    /// <param name="rotationY">«начение поворота по оси Y (в градусах)</param>
    public void RotateObject(float rotationY)
    {
        // ќграничиваем целевую ориентацию, чтобы не превышала maxRotation
        targetRotation.y = Mathf.Clamp(rotationY, initialRotationY, initialRotationY + maxRotation);
    }

    void Update()
    {
        // ѕлавное вращение к целевой ориентации с заданной скоростью
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(targetRotation), Time.deltaTime * rotationSpeed);

        if (_doorTrigger._isCanOpen)
        {
            if (Input.GetKeyDown(KeyCode.E) && !_isOpen)
            {
                // ќткрываем дверь (поворачиваем до максимального угла)
                RotateObject(initialRotationY + maxRotation);
                _isOpen = true;
            }
            else if (Input.GetKeyDown(KeyCode.E) && _isOpen)
            {
                // «акрываем дверь (возвращаем на начальный угол)
                RotateObject(initialRotationY);
                _isOpen = false;
            }
        }
    }

    public void OpenDoor()
    {
        RotateObject(initialRotationY + maxRotation);
    }
    public void CloseDoor()
    {
        RotateObject(initialRotationY);
    }
}