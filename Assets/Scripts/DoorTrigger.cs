using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public bool _isCanOpen;

    [SerializeField] private DoorOpen _doorOpen;

    [SerializeField] private float rayDistance = 100f; // Дистанция, на которую выпускаем луч
    [SerializeField] private LayerMask hitLayers;      // Слои, с которыми может взаимод

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
    /// Выпускает луч из центра экрана.
    /// </summary>
    void ShootRayFromCenter()
    {
        // Получаем камеру, с которой выпускаем луч (обычно это камера игрока)
        Camera camera = Camera.main;

        // Создаем луч из центра экрана
        Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        // Проверяем попадание луча
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
