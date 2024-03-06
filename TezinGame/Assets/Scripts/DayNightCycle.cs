using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public float dayDuration = 60f; // Duração de um ciclo dia-noite em segundos
    public Gradient dayNightColors; // Gradiente de cores para transição dia-noite

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        InvokeRepeating("UpdateDayNightCycle", 0f, 1f); // Chama a função a cada segundo
    }

    private void UpdateDayNightCycle()
    {
        float timeOfDay = Mathf.Repeat(Time.time, dayDuration) / dayDuration; // Calcula o tempo do dia

        // Atualiza a cor do sprite com base no gradiente de cores
        spriteRenderer.color = dayNightColors.Evaluate(timeOfDay);
    }
}
