using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDialogue : MonoBehaviour
{
    public GameObject canvasDialogo;
    public GameObject player;
    private PlayerMovInputSystem playerMovement;
    private Animator playerAnimator;
    private bool isDialogueActive = false;
    private bool hasTriggered = false;
    public Collider2D dialogueTrigger;
    public BrainstormingMinigameController minigameController; // Referencia al minijuego
    public bool startMinigameOnEnd = false; // Controla si se debe iniciar el minijuego tras el primer diálogo
    public Collider2D nextDialogueTrigger; // Trigger del segundo diálogo

    private void Start()
    {
        playerMovement = player.GetComponent<PlayerMovInputSystem>();
        playerAnimator = player.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isDialogueActive && !hasTriggered)
        {
            hasTriggered = true;
            isDialogueActive = true;
            canvasDialogo.SetActive(true);
            playerMovement.canMove = false;

            // Oculta el joystick al iniciar el diálogo
            playerMovement.HideJoystick();

            playerAnimator.SetFloat("MovX", 0);
            playerAnimator.SetFloat("MovY", 0);
            playerAnimator.SetBool("IsMove", false);
        }
    }

    public void EndDialogue()
    {
        canvasDialogo.SetActive(false);
        playerMovement.canMove = true;
        isDialogueActive = false;

        // Muestra el joystick al finalizar el diálogo
        playerMovement.ShowJoystick();

        // Inicia el minijuego solo si es el primer diálogo y está configurado para hacerlo
        if (startMinigameOnEnd && minigameController != null)
        {
            minigameController.StartMinigameOnce();
        }

        // Activa el segundo trigger después del minijuego
        if (nextDialogueTrigger != null)
        {
            nextDialogueTrigger.enabled = true;
        }
    }
}
