using UnityEngine;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Player playerHealth;

    private void Start()
    {
        healthSlider.maxValue = playerHealth.MaxHealth;
    }

    private void Update()
    {
        healthSlider.value = playerHealth.CurrentHealth;
    }
}
