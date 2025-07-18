using UnityEngine;

// GameManager controla el flujo general del juego: inicio, avance, pérdida, victoria, etc.
public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton para acceder globalmente

    [Header("Configuración de niveles")]
    public int currentLevel = 1;
    public float baseTimePerLevel = 25f; // Tiempo para niveles fáciles
    public float timeReductionPerLevel = 2f; // Se reduce tiempo por nivel (dificultad)

    [Header("Estado del juego")]
    public bool isGameActive = false;

    private void Awake()
    {
        // Configurar Singleton
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        StartGame(); // Iniciar automáticamente al abrir escena
    }

    /// Inicia una nueva partida
    public void StartGame()
    {
        Debug.Log("🎮 Juego iniciado");
        isGameActive = true;
        currentLevel = 1;
        ScoreManager.Instance.ResetScore();
        StartLevel();
    }

    /// Inicia una nueva ronda de juego
    public void StartLevel()
    {
        Debug.Log($"📘 Iniciando nivel {currentLevel}");

        // Calcular el tiempo disponible para este nivel (más difícil → menos tiempo)
        float levelTime = Mathf.Max(5f, baseTimePerLevel - timeReductionPerLevel * (currentLevel - 1));
        TimerController.Instance.StartTimer(levelTime);
    }

    /// Llamado cuando el jugador completa correctamente una fórmula
    public void CompleteLevel(float timeUsed)
    {
        if (!isGameActive) return;

        Debug.Log($"✅ Nivel {currentLevel} completado");
        ScoreManager.Instance.currentLevel = currentLevel;
        ScoreManager.Instance.currentTime += timeUsed;
        ScoreManager.Instance.RegisterCorrectAnswer(timeUsed);

        currentLevel++;
        StartLevel(); // Pasar al siguiente nivel
    }

    /// Llamado cuando el jugador se equivoca (por respuesta incorrecta o se acaba el tiempo)
    public void RegisterMistake()
    {
        if (!isGameActive) return;

        ScoreManager.Instance.RegisterMistake(); // Lleva el conteo de errores

        if (ScoreManager.Instance.mistakes >= ScoreManager.Instance.maxMistakes)
        {
            LoseGame(); // Si ya se perdió, termina aquí
        }
    }

    /// Llamado cuando el jugador ha perdido
    public void LoseGame()
    {
        Debug.Log("💀 Has perdido el juego");
        isGameActive = false;
        TimerController.Instance.StopTimer();
        ScoreManager.Instance.SaveHighScore();

        // Aquí se va a una pantalla de derrota o al menú principal
    }

    /// Reinicia todo para volver a jugar
    public void RestartGame()
    {
        Debug.Log("🔁 Reiniciando juego");
        StartGame();
    }
}
