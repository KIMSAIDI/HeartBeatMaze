using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_camera : MonoBehaviour
{
    public float m_sensitivity = 2.0f; // Sensibilité de la souris
    private float m_mouse_x;
    private float m_mouse_y;

    public float arrowKeySensitivity = 50f; // Sensibilité des flèches pour la rotation
    public float movementSpeed = 2.0f; // Vitesse d'avancement
    public Transform playerTransform; // Référence au joueur
    private CharacterController m_controller; // Pour déplacer le joueur

    private bool isLookingBack = false; // Indique si on regarde en arrière
    public float lookBackAngle = 180f; // Angle pour regarder en arrière

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        m_controller = playerTransform.GetComponent<CharacterController>(); // Récupère le CharacterController
    }

    private void LateUpdate()
    {
        // Si la touche "C" est enfoncée, basculer vers la vue arrière
        if (Input.GetKeyDown(KeyCode.C))
        {
            isLookingBack = true;
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            isLookingBack = false;
        }

        if (isLookingBack)
        {
            LookBack();
        }
        else
        {
            NormalView();
        }

        // Gestion du mouvement uniquement avec la flèche haut
        HandleMovement();
    }

    private void NormalView()
    {
        // Rotation normale basée sur la souris
        m_mouse_x += (Input.GetAxisRaw("Mouse X") * m_sensitivity);
        m_mouse_y -= (Input.GetAxisRaw("Mouse Y") * m_sensitivity);

        m_mouse_y = Mathf.Clamp(m_mouse_y, -90f, 90f);

        // Appliquer les mouvements de la souris
        Quaternion camera_rotation = Quaternion.Euler(m_mouse_y, 0f, 0f);
        Quaternion player_rotation = Quaternion.Euler(0f, m_mouse_x, 0f);

        transform.localRotation = camera_rotation;
        playerTransform.localRotation = player_rotation;

        // Rotation avec les flèches gauche/droite
        RotateWithArrowKeys();
    }

    private void RotateWithArrowKeys()
    {
        // Si les touches fléchées sont pressées, uniquement tourner
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            m_mouse_x -= arrowKeySensitivity * Time.deltaTime; // Tourner à gauche
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            m_mouse_x += arrowKeySensitivity * Time.deltaTime; // Tourner à droite
        }
    }

    private void HandleMovement()
    {
        // Avancer uniquement si la flèche haut est pressée
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Vector3 forwardMovement = playerTransform.forward * movementSpeed * Time.deltaTime;
            m_controller.Move(forwardMovement);
        }
    }

    private void LookBack()
    {
        // Vue arrière : orientation vers l'arrière
        transform.localRotation = Quaternion.Euler(0f, 0f, 0f); // Caméra centrée
        playerTransform.localRotation = Quaternion.Euler(0f, m_mouse_x + lookBackAngle, 0f); // Ajoute un angle de 180° pour regarder en arrière
    }
}
