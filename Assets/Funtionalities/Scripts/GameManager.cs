
using UnityEngine;
using UnityEngine.SceneManagement; // Permite cambiar entre escenas

public class GameManager : MonoBehaviour
{
    // Singleton: permite que exista solo una instancia global del GameManager
    public static GameManager instance;

    // Guarda el estado actual del men� (Main, InGame, Lost, Win)
    public Menus currentMenu;

    private void Awake()
    {
        // Si no hay otra instancia, esta se convierte en la �nica
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            // Si ya existe una instancia previa, destruye esta nueva
            Destroy(this.gameObject);
        }

        // Hace que este objeto no se destruya al cambiar de escena
        DontDestroyOnLoad(gameObject);
    }

    // Cambia el estado al men� principal
    public void ToMain()
    {
        currentMenu = Menus.Main;
    }

    // Cambia el estado al juego y carga la escena del juego (�ndice 1 en Build Settings)
    public void ToInGame()
    {
        currentMenu = Menus.InGame;
        SceneManager.LoadScene(1); 
    }

    // Cambia el estado al men� de derrota
    public void ToLost()
    {
        currentMenu = Menus.Lost;
    }

    // Cambia el estado al men� de victoria
    public void ToWin()
    {
        currentMenu = Menus.Win;
    }
}

// Enumeraci�n de todos los estados posibles del men�
public enum Menus
{
    Main,   // Men� principal
    InGame, // Juego activo
    Lost,   // Pantalla de derrota
    Win     // Pantalla de victoria
}
