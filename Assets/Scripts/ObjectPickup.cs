using UnityEngine;

public class ObjectPickup : MonoBehaviour
{
    public Transform holdPoint;        // �����, ��� ������ ����� ������������
    public float pickupRange = 20f;     // ���������, �� ������� ����� ����� ������
    public float throwForce = 10f;     // ���� ������

    private GameObject heldObject = null;
    private Rigidbody heldObjectRb;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (heldObject == null)
            {
                TryPickupObject();  // ����������� ������� ������
            }
            else
            {
                DropObject();  // ������� ������
            }
        }
    }

    void TryPickupObject()
    {
        Camera camera = Camera.main;

        Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        if (Physics.Raycast(ray, out RaycastHit hit, 5f))
        {
            if (hit.collider.gameObject.TryGetComponent<PickupObject>(out PickupObject p))  // �������� ���� �������
            {
                heldObject = hit.collider.gameObject;
                heldObjectRb = heldObject.GetComponent<Rigidbody>();

                if (heldObjectRb != null)
                {
                    heldObjectRb.useGravity = false;
                    heldObjectRb.drag = 10;
                    heldObjectRb.constraints = RigidbodyConstraints.FreezeRotation;  // ��������� ��������
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
