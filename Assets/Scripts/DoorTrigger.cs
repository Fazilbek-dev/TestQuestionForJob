using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public bool _isCanOpen;

    [SerializeField] private DoorOpen _doorOpen;

    [SerializeField] private float rayDistance = 100f; // ���������, �� ������� ��������� ���
    [SerializeField] private LayerMask hitLayers;      // ����, � �������� ����� �������

    private bool _isOpened;

    private void Update()
    {
        ShootRayFromCenter();
        if (_isCanOpen)
        {
            if (Input.GetKeyDown(KeyCode.E) && !_isOpened)
            {
                _doorOpen.OpenDoor();
                _isOpened = true;
            }
            else if (Input.GetKeyDown(KeyCode.E) && _isOpened)
            {
                _doorOpen.CloseDoor();
                _isOpened = true;
            }
        }
    }

    /// <summary>
    /// ��������� ��� �� ������ ������.
    /// </summary>
    void ShootRayFromCenter()
    {
        // �������� ������, � ������� ��������� ��� (������ ��� ������ ������)
        Camera camera = Camera.main;

        // ������� ��� �� ������ ������
        Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        // ��������� ��������� ����
        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance, hitLayers) && hit.transform.TryGetComponent<DoorTrigger>(out DoorTrigger d))
        {
            _isCanOpen = true;
        }
        else
        {
            _isCanOpen = false;
        }
    }
}
