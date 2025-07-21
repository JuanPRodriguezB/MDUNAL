using UnityEngine;

public class VisualCardPos : MonoBehaviour
{
    [Header("Prefab de la carta")]
    public GameObject cardPrefab;

    [Header("Cantidad de cartas")]
    public int numberOfCards = 5;

    [Header("Espaciado entre cartas")]
    public float spacing = 1.5f;

    [Header("Altura desde la parte inferior de la pantalla (en unidades world)")]
    public float verticalOffset = 1.5f;

    void Start()
    {
        SpawnHand();
    }

    void SpawnHand()
    {
        if (cardPrefab == null || numberOfCards <= 0)
        {
            Debug.LogWarning("Falta prefab o número inválido de cartas.");
            return;
        }

        // Obtener el ancho de una carta (asumiendo spriteRenderer centrado)
        float cardWidth = cardPrefab.GetComponent<SpriteRenderer>().bounds.size.x;

        // Calcular el ancho total de la mano
        float totalWidth = (numberOfCards - 1) * (cardWidth * spacing);

        // Posición inicial (centrada en X, a la izquierda del grupo)
        Vector3 center = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0f, 10f)); // z = distancia a cámara
        float startX = center.x - totalWidth / 2;

        for (int i = 0; i < numberOfCards; i++)
        {
            float x = startX + i * (cardWidth * spacing);
            Vector3 spawnPosition = new Vector3(x, center.y + verticalOffset, 0f);

            Instantiate(cardPrefab, spawnPosition, Quaternion.identity, transform);
        }
    }
}
