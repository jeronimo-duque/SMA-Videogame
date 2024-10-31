using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialgoScript : MonoBehaviour
{
    public GameObject Player;
    public TextMeshProUGUI textoDialogo;
    public TextMeshProUGUI nombrePersonajeTexto;
    public UnityEngine.UI.Image personajeImagen;
    public string[] lines;
    public string[] nombres;
    public Sprite[] sprites;
    public float textSpeed;
    public SceneLoader sceneLoader; // Referencia al SceneLoader
    public bool isFinalDialogue = false; // Indica si este es el último diálogo

    private int index;
    private PlayerMovInputSystem inpSys;

    void Start()
    {
        inpSys = Player.GetComponent<PlayerMovInputSystem>();
        textoDialogo.text = string.Empty;
        nombrePersonajeTexto.text = string.Empty;
        personajeImagen.sprite = null;
        StartDialogue();
        inpSys.canMove = false; // Bloquea el movimiento del jugador
        inpSys.HideJoystick(); // Oculta el joystick al iniciar el diálogo
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // O en móviles: Input.touchCount > 0
        {
            if (textoDialogo.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textoDialogo.text = lines[index];
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        SetDialogueUI();
        StartCoroutine(TypeLine());
        InvokeRepeating("AutoAdvanceDialogue", 30f, 30f);
    }

    IEnumerator TypeLine()
    {
        textoDialogo.text = string.Empty;
        foreach (char c in lines[index].ToCharArray())
        {
            textoDialogo.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            SetDialogueUI();
            textoDialogo.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }

    void SetDialogueUI()
    {
        if (index < nombres.Length)
        {
            nombrePersonajeTexto.text = nombres[index];
        }

        if (index < sprites.Length)
        {
            personajeImagen.sprite = sprites[index];
        }
    }

    void AutoAdvanceDialogue()
    {
        if (textoDialogo.text == lines[index])
        {
            NextLine();
        }
    }

    void EndDialogue()
    {
        CancelInvoke("AutoAdvanceDialogue");
        gameObject.SetActive(false);
        inpSys.canMove = true; // Permite el movimiento del jugador
        inpSys.ShowJoystick(); // Muestra el joystick al finalizar el diálogo

        // Verifica si es el último diálogo antes de cambiar de escena
        if (isFinalDialogue && sceneLoader != null)
        {
            sceneLoader.PlayVideoAndLoadScene();
        }
        else
        {
            FindObjectOfType<CharacterDialogue>().EndDialogue();
        }
    }
}
