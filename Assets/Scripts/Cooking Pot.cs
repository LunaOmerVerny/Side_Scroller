using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using static UnityEngine.GraphicsBuffer;
using System.Linq;
using JetBrains.Annotations;

public class CookingPot : MonoBehaviour
{
    public List<GameObject> heldObject = new List<GameObject>();
    public Transform target;
    public List<GameObject> recipe = new List<GameObject>();
    public GameObject TrashRecipe;

    public List<string> recipeBook = new List<string>();

    private bool isCooking = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isCooking) return;

        if (collision.gameObject.GetComponent<Ingredients>())
        {
            if (heldObject.Contains(collision.gameObject)) return;
            collision.gameObject.transform.SetParent(transform);

            //Debug.Log("Collided with " + collision.gameObject.name);
            collision.gameObject.GetComponent<Rigidbody2D>().linearVelocity = Vector3.zero;
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            //held.transform.position = this.transform.position + new Vector3(0, 0.5f, 0);
            //held.transform.position = this.transform.position;
            collision.gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            collision.gameObject.transform.position = target.position + new Vector3(heldObject.Count * 1, 0f, 0f);
            heldObject.Add(collision.gameObject);

          
            
        }

        if (heldObject.Count >= 3)
        {

            StartCoroutine(cooking());


        }
        
    }

    IEnumerator cooking()
    {
        isCooking=true;
        int myRecipeIndex = CheckRecipe();
        yield return new WaitForSeconds(2f);

        foreach (GameObject obj in heldObject)
        {
            if (obj != null) Destroy(obj);

        }
        heldObject.Clear();

        yield return new WaitForSeconds(2f);
        //LANCE L'naimation

        Vector3 spawnPos = transform.position + new Vector3(0f, 1.5f, 0f);
        GameObject h;

        //if (myRecipeIndex >= 0 ) h =  Instantiate(recipe[myRecipeIndex],transform.position, Quaternion.identity);
        //else  h = Instantiate(TrashRecipe,transform.position, Quaternion.identity);

        if (myRecipeIndex >= 0)
            h = Instantiate(recipe[myRecipeIndex], transform.position + new Vector3(0f, 1.5f, 0f), Quaternion.identity);
        else
            h = Instantiate(TrashRecipe, transform.position + new Vector3(0f, 1.5f, 0f), Quaternion.identity);

        Rigidbody2D rb = h.GetComponent<Rigidbody2D>();
        BoxCollider2D col = h.GetComponent<BoxCollider2D>(); //j'ai ajout� �a mais pas sur
        /* if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
        //Physics2D.IgnoreCollision(h.GetComponent<Collider2D>(), GetComponent<Collider2D>(), true);


        */
        //ON SORT L'OBJET UD POT
        h.transform.parent = null;

        if (rb != null)
            rb.bodyType = RigidbodyType2D.Dynamic;
       // if (col != null) col.enabled = true;
            
        
            isCooking = false;

    }

    int CheckRecipe ()
    {
        int nIndex = 0;
        int value = 0;

        foreach (string obj in recipeBook)
        {
            value= 0;
            int[] indexOut = new int []{ -1, -1, -1};

            string[] p = obj.Split('/');

            for (int i = 0; i < p.Length; i++)
            {
                for (int j = 0; j < heldObject.Count; j++)
                {
                    bool bSwitch = true;

                    for (int k = 0; k < indexOut.Length; k++)
                    {
                        if (indexOut[k] == j) bSwitch = false;
                        Debug.Log("ALREADY FOUND");
                    }

                    if (bSwitch)
                    {
                        Debug.Log("RECETTE : " + p[i]);
                        Debug.Log("INGREDIENTS A TEST : " + heldObject[j].GetComponent<Ingredients>().sName);

                        if (p[i] == heldObject[j].GetComponent<Ingredients>().sName)
                        {

                            p[i] = "none";
                            indexOut[value] = j;
                            value++;
                        }
                    }
                }

                if (value == 3) return nIndex;
            }

            nIndex++;
        }
        return -1;
    }

    
}
