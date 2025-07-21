
using TMPro;
using UnityEngine;

public class TimeLeft : MonoBehaviour
{
    static public TimeLeft instance;

    [SerializeField]int min, seg;
    [SerializeField]TextMeshProUGUI timeText;

    private float timeLeft;
    private float timeElapsed;
    private float timeElapsedSinceLastWin;
    private bool enMarcha;
    [SerializeField]
    private float timeAccelerator;

    public bool EnMarcha { get => enMarcha; set => enMarcha = value; }
    public float TimeLeftInClock { get => timeLeft; set => timeLeft = value; }
    public float TimeElapsed { get => timeElapsed; set => timeElapsed = value; }
    public float TimeElapsedSinceLastWin { get => timeElapsedSinceLastWin; set => timeElapsedSinceLastWin = value; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        TimeLeftInClock = 
            (min * 60) +seg;
        EnMarcha = true;
    }

    private void Start()
    {
        TimeLeftInClock -= Time.deltaTime * timeAccelerator;
        TimeElapsed += Time.deltaTime * timeAccelerator;
        TimeElapsedSinceLastWin += Time.deltaTime;
        if (TimeLeftInClock < 1)
        {
            EnMarcha = false;

            //GameManager.sharedInstanceGameManager.SetGameState();
        }
        int tempMin = Mathf.FloorToInt(TimeLeftInClock / 60);
        int tempSeg = Mathf.FloorToInt(TimeLeftInClock % 60);
        timeText.text = string.Format("{00:00}:{01:00}", tempMin, tempSeg);
    }

    private void Update()
    {
        if (EnMarcha)
        {
            TimeLeftInClock -= Time.deltaTime/**timeAccelerator*/;
            TimeElapsed += Time.deltaTime/**timeAccelerator*/;
            TimeElapsedSinceLastWin += Time.deltaTime;

            if (TimeLeftInClock < timeLeft)
            {
                EnMarcha = false;

                //GameManager.sharedInstanceGameManager.SetGameState();
            }
                int tempMin = Mathf.FloorToInt(TimeLeftInClock / 60);
                int tempSeg = Mathf.FloorToInt(TimeLeftInClock % 60);

            //if (timeElapsedevery15Seconds >= 15)
            //{
              //  timeElapsedevery15Seconds = 0;
            timeText.text = string.Format("{00:00}:{01:00}", tempMin,tempSeg);
                //timeText.text = string.Format("{00:00}:{01:00}", tempMin, 00);
            //}
        }
    }
}
