using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDialogue : MonoBehaviour
{
    public GameObject canvasDialogo;
    public GameObject player; 
    private PlayerMovInputSystem playerMovement; 
    private Animator playerAnimator;
    private bool isDialogueActive = false; // Para evitar reactivar el trigger

    private void Start()
    {
        playerMovement = player.GetComponent<PlayerMovInputSystem>(); 
        playerAnimator = player.GetComponent<Animator>(); 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isDialogueActive)
        {
            isDialogueActive = true;
            canvasDialogo.SetActive(true); // Activa el canvas de diálogo
            playerMovement.canMove = false; // Bloquea el movimiento del jugador

            // Configura la animación en estado idle
            playerAnimator.SetFloat("MovX", 0);
            playerAnimator.SetFloat("MovY", 0);
            playerAnimator.SetBool("IsMove", false); 
        }
    }

    public void EndDialogue()
    {
        canvasDialogo.SetActive(false); // Desactiva el canvas de diálogo
        playerMovement.canMove = true; // Permite que el jugador vuelva a moverse
        GetComponent<Collider2D>().enabled = false; // Desactiva el trigger para que no se reactive
        isDialogueActive = false; // Permite que el trigger no se vuelva a activar
    }
}
