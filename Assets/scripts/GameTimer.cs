using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public float timerDuration = 50f;
    private float timer;
    public TMP_Text timerText; // Assurez-vous que ce type correspond à votre objet TextMeshPro

    private void Start()
    {
        timer = timerDuration;

        // Debug pour vérifier l'association
        if (timerText == null)
        {
            Debug.LogError("Le champ timerText n'est pas assigné dans l'inspecteur !");
        }
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        // Vérifie si timerText est nul avant de l'utiliser
        if (timerText != null)
        {
            timerText.text = "Temps restant : " + Mathf.CeilToInt(timer) + "s";
        }

        if (timer <= 0f)
        {
            EndGame();
        }
    }

    private void EndGame()
{
    Debug.Log("Temps écoulé !");

    #if UNITY_EDITOR
    // Arrête le jeu dans l'éditeur Unity
    UnityEditor.EditorApplication.isPlaying = false;
    #else
    // Quitte le jeu dans une build
    Application.Quit();
    #endif
}

}
