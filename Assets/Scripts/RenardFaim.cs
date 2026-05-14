using UnityEngine;

public class RenardFaim : MonoBehaviour
{
    public float detectionRange = 1.5f;
    private Animator anim;
    private bool nourri = false;

    void Start()
    {
        
        anim = GetComponentInChildren<Animator>();
        Debug.Log("Animator trouvé : " + anim); 
        Debug.Log("Controller : " + anim.runtimeAnimatorController);
    }

    void Update()
    {
        if (nourri) return; 

        
        GameObject tarte = TrouverTarte();

        if (tarte != null)
        {
            Absorber(tarte);
        }
    }

    GameObject TrouverTarte()
    {
        // Cherche tous les objets taggés "Objet"
        GameObject[] objets = GameObject.FindGameObjectsWithTag("Objet");

        foreach (var obj in objets)
        {
            if (!obj.name.Contains("Tarte")) continue; //  filtre par nom

            float dist = Vector2.Distance(transform.position, obj.transform.position);
            if (dist <= detectionRange)
                return obj;
        }
        return null;
    }

    void Absorber(GameObject tarte)
    {
        nourri = true;
        Destroy(tarte);
        Debug.Log("Controller : " + anim.runtimeAnimatorController);
        Debug.Log("Tentative SetBool estNourri");
        anim.SetBool("estNourri", true);
        Debug.Log("SetBool fait, estNourri = " + anim.GetBool("estNourri"));
    }
}
