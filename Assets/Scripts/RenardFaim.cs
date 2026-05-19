using UnityEngine;
using UnityEngine.SceneManagement;

public class RenardFaim : MonoBehaviour
{
    public float detectionRange = 0.5f;
    private Animator anim;
    public static bool renardNourri = false;
    public Transform detectionPoint;

    void Start()
    {
        
        anim = GetComponentInChildren<Animator>();
        Debug.Log("Animator trouv� : " + anim); 
        Debug.Log("Controller : " + anim.runtimeAnimatorController);
    }

    void Update()
    {
        if (renardNourri) return; 

        
        GameObject tarte = TrouverTarte();

        if (tarte != null)
        {
            Absorber(tarte);
        }
    }

    GameObject TrouverTarte()
    {
        // Cherche tous les objets tagg�s "Objet"
        GameObject[] objets = GameObject.FindGameObjectsWithTag("Objet");

        foreach (var obj in objets)
        {
            if (!obj.name.Contains("Tarte")) continue; //  filtre par nom

            float dist = Vector2.Distance(detectionPoint.position, obj.transform.position);
            if (dist <= detectionRange)
                return obj;
        }
        return null;
    }

    void Absorber(GameObject tarte)
    {
        renardNourri = true;
        VerifierFin();
        Destroy(tarte);
        Debug.Log("Controller : " + anim.runtimeAnimatorController);
        Debug.Log("Tentative SetBool estNourri");
        anim.SetBool("estNourri", true);
        Debug.Log("SetBool fait, estNourri = " + anim.GetBool("estNourri"));
    }

    void VerifierFin()
    {
        if (renardNourri && LapinFaim.lapinNourri)
        {
            SceneManager.LoadScene("FinDuJEu");
        }
    }

}
