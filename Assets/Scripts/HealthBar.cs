using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public GameObject Player;
    float Barredevie;
    Slider slider;
    
    void Awake()
    {
        slider = GetComponent<Slider>();
    }
       void Update()
    {
        Barredevie = Player.GetComponent<Character>().currentHealth;
        slider.value = Barredevie;

    }

}