using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI; // Importa el namespace correcto para UI

public class DialgoScript : MonoBehaviour
{
    public GameObject Player;
    public TextMeshProUGUI textoDialogo; // Campo para el texto del di�logo
    public TextMeshProUGUI nombrePersonajeTexto; // Campo para el nombre del personaje
    public UnityEngine.UI.Image personajeImagen; // Campo para la imagen del personaje (usa el namespace espec�fico)
    public string[] lines; // L�neas de di�logo
    public string[] nombres; // Nombres de los personajes correspondientes a cada l�nea de di�logo
    public Sprite[] sprites; // Sprites de los personajes correspondientes a cada l�nea de di�logo
    public float textSpeed;

    private int index;
    private PlayerMovInputSystem inpSys;

    void Start()
    {
        inpSys = Player.GetComponent<PlayerMovInputSystem>();
        textoDialogo.text = string.Empty;
        nombrePersonajeTexto.text = string.Empty; // Iniciar el nombre vac�o
        personajeImagen.sprite = null; // Iniciar la imagen vac�a
        StartDialogue();
        inpSys.canMove = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // O en m�viles: Input.touchCount > 0
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
        SetDialogueUI(); // Configura el nombre y la imagen del personaje
        StartCoroutine(TypeLine());
        InvokeRepeating("AutoAdvanceDialogue", 15f, 15f); // Avanza cada 15 segundos
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
            SetDialogueUI(); // Configura el siguiente nombre y la imagen del personaje
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
        // Configura el nombre y el sprite correspondiente a la l�nea actual
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
        if (textoDialogo.text == lines[index]) // Solo avanza si la l�nea completa ya est� mostrada
        {
            NextLine();
        }
    }

    void EndDialogue()
    {
        CancelInvoke("AutoAdvanceDialogue"); // Cancela el autoavance
        gameObject.SetActive(false);
        inpSys.canMove = true;
        FindObjectOfType<CharacterDialogue>().EndDialogue();
    }
}
