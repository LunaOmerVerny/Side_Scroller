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

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.GetComponent<Ingredients>())
        {
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

            collision.gameObject.transform.SetParent(transform);
            
        }

        if (heldObject.Count >= 3)
        {

            StartCoroutine(cooking());


        }
        
    }

    IEnumerator cooking()
    {
        int myRecipeIndex = CheckRecipe();
        yield return new WaitForSeconds(2f);

        foreach (GameObject obj in heldObject)
        {
            Destroy(obj);

        }

        yield return new WaitForSeconds(2f);
        //LANCE L'naimation

        GameObject h; 

         if (myRecipeIndex >= 0 ) h =  Instantiate(recipe[myRecipeIndex],transform.position, Quaternion.identity);
        else  h = Instantiate(TrashRecipe,transform.position, Quaternion.identity);



            yield return new WaitForSeconds(2f);
        //ON SORT L'OBJET UD POT
        h.transform.parent = null;
        heldObject = new List<GameObject>();

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
