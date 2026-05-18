using UnityEngine;

public class NoBatterie : MonoBehaviour
{  
     [Header("Battery")]
    public int battery = 100;

    [Header("UI")]
    public GameObject noBatteryScreen;
    private bool empty = false;

    void Update()
    {
        // Vérifie si la batterie est vide
        if (battery <= 0 && !empty)
        {
            EmptyBattery();
        }
    }

    void EmptyBattery()
    {
        empty = true;

        // Affiche l'écran
        noBatteryScreen.SetActive(true);

        // Pause le jeu
        Time.timeScale = 0f;

        // Affiche la souris
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}

   