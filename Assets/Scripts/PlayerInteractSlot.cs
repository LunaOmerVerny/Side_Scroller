using UnityEngine;

public class PlayerInteractSlot : MonoBehaviour
{
    public float pickupRange = 2f;
    public KeyCode interactKey = KeyCode.F;
    private Itemlot[] cachedSlots;
    private GrabObject grabScript;

    void Start()
    {
        cachedSlots = FindObjectsByType<Itemlot>(FindObjectsSortMode.None);
        grabScript = GetComponent<GrabObject>(); // recupere le script de grabobject
        
        
    }

    void Update()
    {
        if (Input.GetKeyDown(interactKey))
            TryInteract();
    }

    void TryInteract()
    {
        // vérifie qu'on porte bien quelque chose
        if (grabScript == null || grabScript.heldObject.Count == 0)
        {
            Debug.Log("Rien en main");
            return;
        }
        TryPlace();
    }

    void TryPlace()
    {
        Itemlot nearest = GetNearestSlot();
        Debug.Log("Nearest slot : " + nearest);

        if (nearest != null)
        {
            
            GameObject objToPlace = grabScript.heldObject[grabScript.heldObject.Count - 1];

            nearest.currentItem = objToPlace;
            objToPlace.transform.SetParent(nearest.transform);
            objToPlace.transform.localPosition = Vector3.zero;

            objToPlace.GetComponent<BoxCollider2D>().enabled = true;
            objToPlace.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

            grabScript.heldObject.RemoveAt(grabScript.heldObject.Count - 1);
        }
        else
        {
            Debug.Log("Aucun slot vide à portée");
        }
    }

    Itemlot GetNearestSlot()
    {
        Itemlot nearest = null;
        float nearestDist = pickupRange + 0.01f;

        foreach (var slot in cachedSlots)
        {
            if (!slot.IsEmpty) continue; //  cherche les slots vides

            float dist = Vector2.Distance(transform.position, slot.transform.position);
            Debug.Log($"Slot {slot.name} | dist: {dist:F2} | IsEmpty: {slot.IsEmpty}");

            if (dist < nearestDist)
            {
                nearestDist = dist;
                nearest = slot;
            }
        }
        return nearest;
    }
}