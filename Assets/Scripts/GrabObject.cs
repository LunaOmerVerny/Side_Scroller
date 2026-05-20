using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GrabObject : MonoBehaviour
{
    public List<GameObject> heldObject = new List<GameObject>();
    public Transform target;
    public float pickupRange = 2f;

    private Itemlot[] cachedSlots;

    void Start()
    {
        cachedSlots = FindObjectsByType<Itemlot>(FindObjectsSortMode.None);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (Input.GetKey(KeyCode.E) && collision.gameObject.CompareTag("Objet"))
        {
            //Debug.Log("Collided with " + collision.gameObject.name);

            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            collision.gameObject.GetComponent<Rigidbody2D>().constraints |= RigidbodyConstraints2D.FreezePositionX;

            collision.gameObject.transform.position = target.position + new Vector3(0f, heldObject.Count * 1, 0f);
            heldObject.Add(collision.gameObject);
            collision.gameObject.transform.SetParent(transform);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            TryPickupFromSlot();

        if (Input.GetKeyDown(KeyCode.Q) && heldObject.Count > 0)
        {
            GameObject objToDrop = heldObject[heldObject.Count - 1];
            objToDrop.transform.SetParent(null);
            //objToDrop.transform.gameObject.GetComponent<BoxCollider2D>().enabled = true;
            objToDrop.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            heldObject.RemoveAt(heldObject.Count - 1);
            StartCoroutine(EnableColliderDelayed(objToDrop));
        }

        float moveX = Input.GetAxisRaw("Horizontal");

        if (moveX > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (moveX < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }
    IEnumerator EnableColliderDelayed(GameObject obj)
    {
        yield return new WaitForSeconds(0.3f); 
        if (obj != null)
            obj.GetComponent<BoxCollider2D>().enabled = true;
    }

    void TryPickupFromSlot()
    {
        Itemlot nearest = null;
        float nearestDist = pickupRange + 0.01f;

        foreach (var slot in cachedSlots)
        {
            if (slot.IsEmpty) continue; // cherche uniquement les slots occupés

            float dist = Vector2.Distance(transform.position, slot.transform.position);
            if (dist < nearestDist)
            {
                nearestDist = dist;
                nearest = slot;
            }
        }

        if (nearest == null) return; 

        
        GameObject obj = nearest.currentItem;
        nearest.currentItem = null;

        obj.transform.SetParent(transform);
        obj.transform.position = target.position + new Vector3(0f, heldObject.Count * 1, 0f);
        obj.GetComponent<BoxCollider2D>().enabled = false;
        obj.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

        heldObject.Add(obj);
        Debug.Log("Objet récupéré depuis l'étagère : " + obj.name);


    }
}
