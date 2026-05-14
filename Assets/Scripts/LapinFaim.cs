using UnityEngine;

public class LapinFaim : MonoBehaviour
{
    public float detectionRange = 1.5f;
    private Animator anim;
    private bool nourri = false;

    void Start()
    {
        //  récupère l'Animator sur le child LAPIN FAIM 0000_0
        anim = GetComponentInChildren<Animator>();
        Debug.Log("Animator trouvé : " + anim); // ✅ vérifie qu'il est trouvé
        Debug.Log("Controller : " + anim.runtimeAnimatorController);
    }

    void Update()
    {
        if (nourri) return; //  plus rien à faire si déjà nourri

        //  cherche une brochette proche
        GameObject borchette = TrouverBorchette();

        if (borchette != null)
        {
            Absorber(borchette);
        }
    }

    GameObject TrouverBorchette()
    {
        // Cherche tous les objets taggés "Objet"
        GameObject[] objets = GameObject.FindGameObjectsWithTag("Objet");

        foreach (var obj in objets)
        {
            if (!obj.name.Contains("Borchette")) continue; //  filtre par nom

            float dist = Vector2.Distance(transform.position, obj.transform.position);
            if (dist <= detectionRange)
                return obj;
        }
        return null;
    }

    void Absorber(GameObject borchette)
    {
        nourri = true;
        Destroy(borchette);
        Debug.Log("Controller : " + anim.runtimeAnimatorController);
        Debug.Log("Tentative SetBool estNourri");
        anim.SetBool("estNourri", true);
        Debug.Log("SetBool fait, estNourri = " + anim.GetBool("estNourri"));
    }
}
