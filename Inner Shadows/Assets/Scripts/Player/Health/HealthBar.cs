using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health player_health;
    [SerializeField] private Image total_healthbar;
    [SerializeField] private Image current_healhbar;

    private void Start()
    {
        total_healthbar.fillAmount = player_health.current_health / 10;
    }

    private void Update()
    {
        current_healhbar.fillAmount = player_health.current_health / 10;
    }
}
