using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    [SerializeField] private DoorTrigger _doorTrigger;

    public float rotationSpeed = 5f;  // �������� ��������
    public float maxRotation = 45f;   // ������������ �������� �������� ��� �������� �����
    private float initialRotationY;   // ��������� ���� �������� ����� (����� ��� �������)
    private Vector3 targetRotation;   // ������� ����������

    private bool _isOpen;

    private void Start()
    {
        // ���������� ��������� ���� �������� ����� (�� ��� Y)
        initialRotationY = transform.eulerAngles.y;

        // ���������� ������� ���������� ��������� � ��������� ����������� �������
        targetRotation = transform.eulerAngles;
    }

    /// <summary>
    /// ������������ ������ �� ��������� ���� �� ��� Y � ������������.
    /// </summary>
    /// <param name="rotationY">�������� �������� �� ��� Y (� ��������)</param>
    public void RotateObject(float rotationY)
    {
        // ������������ ������� ����������, ����� �� ��������� maxRotation
        targetRotation.y = Mathf.Clamp(rotationY, initialRotationY, initialRotationY + maxRotation);
    }

    void Update()
    {
        // ������� �������� � ������� ���������� � �������� ���������
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(targetRotation), Time.deltaTime * rotationSpeed);

        if (_doorTrigger._isCanOpen)
        {
            if (Input.GetKeyDown(KeyCode.E) && !_isOpen)
            {
                // ��������� ����� (������������ �� ������������� ����)
                RotateObject(initialRotationY + maxRotation);
                _isOpen = true;
            }
            else if (Input.GetKeyDown(KeyCode.E) && _isOpen)
            {
                // ��������� ����� (���������� �� ��������� ����)
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