using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controla el tiempo límite por ronda.
/// Si se acaba el tiempo, se registra como error.
/// </summary>
public class TimerController : MonoBehaviour
{
    public static TimerController Instance;

    [Header("Configuración de tiempo")]
    public float timeLimit = 25f;      // Tiempo máximo por nivel (se ajustará por dificultad)
    private float timeRemaining;       // Tiempo restante en la ronda actual
    private bool isRunning = false;    // ¿El temporizador está activo?

    [Header("UI (opcional)")]
    public Text timerText;             // Referencia al texto que muestra el tiempo

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        if (!isRunning) return;

        // Contar hacia atrás
        timeRemaining -= Time.deltaTime;

        // Actualizar visualmente el tiempo
        UpdateTimerUI();

        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            isRunning = false;

            Debug.Log("⏰ Tiempo agotado");
            ScoreManager.Instance.RegisterMistake(); // Penaliza como error
        }
    }


    /// Inicia el temporizador para una nueva ronda.
    public void StartTimer(float customLimit = -1f)
    {
        timeLimit = customLimit > 0 ? customLimit : timeLimit;
        timeRemaining = timeLimit;
        isRunning = true;
    }


    /// Detiene el temporizador (por ejemplo, si el jugador responde antes).
    public void StopTimer()
    {
        isRunning = false;
    }


    /// Devuelve cuánto tiempo tomó la respuesta actual.
    public float GetTimeTaken()
    {
        return timeLimit - timeRemaining;
    }


    /// Actualiza el componente visual del temporizador.
    private void UpdateTimerUI()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(timeRemaining / 60f);
            int seconds = Mathf.FloorToInt(timeRemaining % 60f);
            timerText.text = $"{minutes:00}:{seconds:00}";
        }
    }
}
