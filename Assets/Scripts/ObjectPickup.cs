using UnityEngine;

public class ObjectPickup : MonoBehaviour
{
    public Transform holdPoint;        // Точка, где объект будет удерживаться
    public float pickupRange = 20f;     // Дальность, на которой можно взять объект
    public float throwForce = 10f;     // Сила броска

    private GameObject heldObject = null;
    private Rigidbody heldObjectRb;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (heldObject == null)
            {
                TryPickupObject();  // Попробовать поднять объект
            }
            else
            {
                DropObject();  // Бросить объект
            }
        }
    }

    void TryPickupObject()
    {
        Camera camera = Camera.main;

        Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        if (Physics.Raycast(ray, out RaycastHit hit, 5f))
        {
            if (hit.collider.gameObject.TryGetComponent<PickupObject>(out PickupObject p))  // Проверка тега объекта
            {
                heldObject = hit.collider.gameObject;
                heldObjectRb = heldObject.GetComponent<Rigidbody>();

                if (heldObjectRb != null)
                {
                    heldObjectRb.useGravity = false;
                    heldObjectRb.drag = 10;
                    heldObjectRb.constraints = RigidbodyConstraints.FreezeRotation;  // Запретить вращение
                    heldObject.transform.position = holdPoint.position;
                    heldObject.transform.parent = holdPoint.transform;
                }
            }
        }
    }

    void DropObject()
    {
        heldObjectRb.useGravity = true;
        heldObjectRb.drag = 1;
        heldObjectRb.constraints = RigidbodyConstraints.None;
        heldObjectRb.transform.parent = null;

        heldObject = null;
        heldObjectRb = null;
    }
}
