using Microlight.MicroBar;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance; //singleton

    [SerializeField] public MicroBar healthBar; //Clase de la barra de vida
    [SerializeField] public TextMeshProUGUI score; //texto con el puntaje
    [SerializeField] public int currentLife;

    [Header("Puntaje actual")]
    [SerializeField] public int points = 0;
    public float currentTime = 0f;     // Tiempo acumulado del jugador

    [Header("Errores")]
    public int mistakes = 0;           // Cantidad de errores cometidos
    public int maxMistakes = 3;        // Límite de errores antes de perder

    [Header("Bonificaciones")]
    public int streak = 0;                 // Racha actual de respuestas correctas
    public int streakBonusThreshold = 2;  // Racha mínima para obtener bonificación
    public float fastAnswerTime = 5f;     // Tiempo en segundos para recibir bonificación por rapidez

    [Header("Top 5 puntajes")]
    private const int MAX_SCORES = 5;     // Solo se guardan los 5 mejores puntajes


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        //consigue los gameObjects que existan en pantalla de tipo healthBar y score

        healthBar = GameObject.FindGameObjectWithTag("H").GetComponent<MicroBar>();
        score = GameObject.FindGameObjectWithTag("S").GetComponent<TextMeshProUGUI>();
        Init();
    }

    private void Init()
    {
        healthBar.Initialize(3);    //inicializa la barra de vida
        currentLife = 3;            //añade vida maxima
        points = 0;                 //inicializa puntos iniciales
        score.text = "score: 0";    //actualiza puntos en pantalla
    }

    private void Update()
    {
        //si se acaba la vida o se acaba el tiempo 
        if (currentLife <= 0 || TimeLeft.instance.TimeLeftInClock <= 0f)
        {
            Init();
            TimeLeft.instance.TimeLeftInClock = 120f;
            GameManager.instance.ToLost();
        }
    }

    /// Llamar cuando el jugador responde correctamente.
    /// Calcula puntaje y aplica bonificaciones.
    public int RegisterCorrectAnswer(float timeTaken)
    {
        int basePoints = 100;
        int bonus = 0;

        // Bonificación por respuesta rápida
        if (timeTaken <= fastAnswerTime)
        {
            bonus += 200;
        }

        // Aumentar racha de respuestas correctas
        streak++;

        // Bonificación por racha
        if (streak >= streakBonusThreshold)
        {
            bonus += 350;
            Debug.Log("¡Racha de aciertos!");
        }

        // Sumar al puntaje total
        points += basePoints + bonus;

        Debug.Log($"Correcto. +{basePoints + bonus} puntos (total: {points})");

        return basePoints + bonus;
    }
}
