
using TMPro;
using UnityEngine;

public class TimeLeft : MonoBehaviour
{
    // Singleton para acceder fácilmente a esta instancia desde otros scripts
    static public TimeLeft instance;

    // Tiempo inicial en minutos y segundos
    [SerializeField] int min, seg;

    // Referencia al texto UI donde se mostrará el tiempo restante
    [SerializeField] TextMeshProUGUI timeText;

    // Tiempo restante (en segundos)
    private float timeLeft;

    // Tiempo total transcurrido desde el inicio del juego
    private float timeElapsed;

    // Tiempo transcurrido desde el último "win"
    private float timeElapsedSinceLastWin;

    // Indica si el temporizador está corriendo
    private bool enMarcha;

    // Factor de aceleración para que el tiempo pase más rápido (actualmente no usado en Update)
    [SerializeField] private float timeAccelerator;

    // Propiedades públicas para acceder/controlar las variables privadas
    public bool EnMarcha { get => enMarcha; set => enMarcha = value; }
    public float TimeLeftInClock { get => timeLeft; set => timeLeft = value; }
    public float TimeElapsed { get => timeElapsed; set => timeElapsed = value; }
    public float TimeElapsedSinceLastWin { get => timeElapsedSinceLastWin; set => timeElapsedSinceLastWin = value; }

    private void Awake()
    {
        // Asignación del singleton
        if (instance == null)
        {
            instance = this;
        }

        // Se calcula el tiempo total inicial en segundos
        TimeLeftInClock = (min * 60) + seg;

        // El reloj empieza corriendo
        EnMarcha = true;
    }

    private void Start()
    {
        TimeLeftInClock -= Time.deltaTime * timeAccelerator;
        TimeElapsed += Time.deltaTime * timeAccelerator;
        TimeElapsedSinceLastWin += Time.deltaTime;

        // Si el tiempo ya es menor a 1 segundo, se detiene el reloj
        if (TimeLeftInClock < 1)
        {
            EnMarcha = false;
        }

        // Se formatea y actualiza el texto del reloj en la UI
        int tempMin = Mathf.FloorToInt(TimeLeftInClock / 60);
        int tempSeg = Mathf.FloorToInt(TimeLeftInClock % 60);
        timeText.text = string.Format("{00:00}:{01:00}", tempMin, tempSeg);
    }

    private void Update()
    {
        // Solo ejecuta si el temporizador está activo
        if (EnMarcha)
        {
            // Disminuye el tiempo restante (sin usar timeAccelerator)
            TimeLeftInClock -= Time.deltaTime;

            // Aumentan los contadores de tiempo
            TimeElapsed += Time.deltaTime;
            TimeElapsedSinceLastWin += Time.deltaTime;

            if (TimeLeftInClock < timeLeft)
            {
                EnMarcha = false;
            }

            // Se actualiza el texto con el tiempo restante en formato mm:ss
            int tempMin = Mathf.FloorToInt(TimeLeftInClock / 60);
            int tempSeg = Mathf.FloorToInt(TimeLeftInClock % 60);
            timeText.text = string.Format("{00:00}:{01:00}", tempMin, tempSeg);
        }
    }
}
