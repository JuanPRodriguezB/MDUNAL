using UnityEngine;

/// Gestiona el puntaje, errores, bonificaciones y guarda el top 5.
/// Esta clase debe estar presente durante toda la ejecución del juego.
public class ScoreManager : MonoBehaviour
{
    // Patrón Singleton: permite acceder fácilmente desde cualquier parte
    public static ScoreManager Instance;

    [Header("Puntaje actual")]
    public int currentScore = 0;       // Puntos actuales del jugador
    public float currentTime = 0f;     // Tiempo acumulado del jugador
    public int currentLevel = 1;       // Nivel actual del jugador

    [Header("Errores")]
    public int mistakes = 0;           // Cantidad de errores cometidos
    public int maxMistakes = 3;        // Límite de errores antes de perder

    [Header("Bonificaciones")]
    public int streak = 0;                 // Racha actual de respuestas correctas
    public int streakBonusThreshold = 2;  // Racha mínima para obtener bonificación
    public float fastAnswerTime = 5f;     // Tiempo en segundos para recibir bonificación por rapidez

    [Header("Top 5 puntajes")]
    private const int MAX_SCORES = 5;     // Solo se guardan los 5 mejores puntajes

    // Este método se ejecuta al cargar la escena
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Opcional: si deseas que persista entre escenas
        }
        else
        {
            Destroy(gameObject); // Evita duplicados
        }
    }


    /// Llamar cuando el jugador responde correctamente.
    /// Calcula puntaje y aplica bonificaciones.
    public void RegisterCorrectAnswer(float timeTaken)
    {
        int basePoints = 100;
        int bonus = 0;

        // Bonificación por respuesta rápida
        if (timeTaken <= fastAnswerTime)
        {
            bonus += 50;
        }

        // Aumentar racha de respuestas correctas
        streak++;

        // Bonificación por racha
        if (streak >= streakBonusThreshold)
        {
            bonus += 75;
            Debug.Log("🔥 ¡Racha de aciertos!");
        }

        // Sumar al puntaje total
        currentScore += basePoints + bonus;

        Debug.Log($"✅ Correcto. +{basePoints + bonus} puntos (total: {currentScore})");
    }


    /// Llamar cuando el jugador comete un error (respuesta incorrecta o se acaba el tiempo).
    public void RegisterMistake()
    {
        mistakes++;
        streak = 0; // se rompe la racha

        Debug.Log($"❌ Error. Total de errores: {mistakes}/{maxMistakes}");

        // Si excede el máximo de errores, el jugador pierde
        if (mistakes >= maxMistakes)
        {
            Debug.Log("☠️ Has perdido. Se alcanzó el máximo de errores.");
            // Aquí se puede llamar a GameManager.Instance.LoseGame() cuando esté implementado
        }
    }


    /// Guarda el puntaje y el tiempo actual si están entre los 5 mejores.
    public void SaveHighScore()
    {
        for (int i = 0; i < MAX_SCORES; i++)
        {
            int savedScore = PlayerPrefs.GetInt($"HighScore_{i}", 0);

            if (currentScore > savedScore)
            {
                // Desplazar los puntajes hacia abajo
                for (int j = MAX_SCORES - 1; j > i; j--)
                {
                    PlayerPrefs.SetInt($"HighScore_{j}", PlayerPrefs.GetInt($"HighScore_{j - 1}", 0));
                    PlayerPrefs.SetFloat($"HighTime_{j}", PlayerPrefs.GetFloat($"HighTime_{j - 1}", 0f));
                }

                // Guardar nuevo puntaje en la posición i
                PlayerPrefs.SetInt($"HighScore_{i}", currentScore);
                PlayerPrefs.SetFloat($"HighTime_{i}", currentTime);
                break;
            }
        }

        PlayerPrefs.Save();
    }


    /// Reinicia todas las variables del puntaje.
    /// Usar al comenzar una nueva partida.
    public void ResetScore()
    {
        currentScore = 0;
        currentTime = 0;
        mistakes = 0;
        streak = 0;
        currentLevel = 1;
    }
}
