using UnityEngine;
using UnityEngine.UI; // Asegura que estamos usando el espacio de nombres correcto para la UI

public class ScoreManager : MonoBehaviour
{
    public UnityEngine.UI.Image starImage; // Especifica el espacio de nombres completo aquí
    public Sprite oneStarSprite;
    public Sprite twoStarSprite;
    public Sprite threeStarSprite;
    public static ScoreManager Instance { get; private set; }

    private int currentStars = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        FindStarDisplay();
    }

    private void FindStarDisplay()
    {
        // Busca el objeto StarDisplay específicamente por nombre o etiqueta si tiene uno único
        GameObject starDisplayObject = GameObject.Find("StarDisplay"); // Asegúrate de que el objeto se llame "StarDisplay"

        if (starDisplayObject != null)
        {
            starImage = starDisplayObject.GetComponent<UnityEngine.UI.Image>();
            UpdateStarDisplay();
        }
        else
        {
            UnityEngine.Debug.LogWarning("StarDisplay no encontrado en la escena.");
        }
    }

    public void AddStar()
    {
        currentStars++;
        UpdateStarDisplay();
    }

    private void UpdateStarDisplay()
    {
        if (starImage == null)
        {
            UnityEngine.Debug.LogWarning("starImage no está asignado.");
            return;
        }

        switch (currentStars)
        {
            case 1:
                starImage.sprite = oneStarSprite;
                break;
            case 2:
                starImage.sprite = twoStarSprite;
                break;
            case 3:
                starImage.sprite = threeStarSprite;
                break;
            default:
                break;
        }
    }
}
