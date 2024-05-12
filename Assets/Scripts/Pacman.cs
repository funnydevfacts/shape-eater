using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pacman : MonoBehaviour
{
    [Header("Private Variables")]
    private bool circleMission = false, squareMission = true, hexagonMission = false;
    private bool generateFood = false;
    private Vector3 randomCoords;
    private Transform playerTransform;
    private Vector3 playerCoords;
    private float counter = 1;
    private Vector2 coordCounter;
    private int bgorder = 0;
    [Header("References")]
    [SerializeField] private TextMeshProUGUI progressBarText;
    [SerializeField] private Image progressBarImage;
    [SerializeField] private Image progressBarBackgroundImage;
    [SerializeField] private Image questImage;
    [SerializeField] private Sprite[] questSprites;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button startButton;
    [SerializeField] private GameObject confetti;
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private GameObject[] Prefabs;
    [SerializeField] private GameObject progressBarPrefab;
    [SerializeField] private GameObject squareRenderer;
    [SerializeField] private GameObject circleRenderer;
    [SerializeField] private GameObject hexagonRenderer;
    [SerializeField] private GameObject[] background;
    [SerializeField] private RuntimeAnimatorController[] animatorControllers;

    [Header("Public Variables")]
    [SerializeField] public float verticalSpeed = 15;
    [SerializeField] public float horizontalSpeed = 10;
    [SerializeField] public int circleFood = 0;
    [SerializeField] public int squareFood = 0;
    [SerializeField] public int hexagonFood = 0;




    public enum FoodType
    {
        Circle,
        Hexagon,
        Square,
        Cross
    }

    void Start()
    {
        horizontalSpeed = 0;
        verticalSpeed = 0;
        retryButton.gameObject.SetActive(false);
        questImage.sprite = questSprites[2];
        progressBarImage.fillAmount = squareFood / 5f;
        progressBarText.text = squareFood + " / 5";
        progressBarImage.fillAmount = 0;

        playerTransform = GetComponent<Transform>();
        coordCounter = new Vector2(playerTransform.position.x, playerTransform.position.y);
        squareRenderer.SetActive(true);
        circleRenderer.SetActive(false);
        hexagonRenderer.SetActive(false);
    }

    public void CollectFood(FoodType type)
    {
        switch (type)
        {
            case FoodType.Circle:
                GetComponent<AudioSource>().PlayOneShot(audioClips[1]);
                circleRenderer.SetActive(true);
                hexagonRenderer.SetActive(false);
                squareRenderer.SetActive(false);
                if (circleMission)
                {
                    circleFood++;
                    progressBarImage.fillAmount = circleFood / 5f;
                    progressBarText.text = circleFood + " / 5";
                    if (circleFood == 5)
                    {
                        circleMission = false;
                        squareMission = false;
                        hexagonMission = true;
                        questImage.sprite = questSprites[1];
                        questImage.color = new Color(74 / 255f, 217 / 255f, 38 / 255f);
                        progressBarImage.fillAmount = hexagonFood / 5f;
                        progressBarText.text = hexagonFood + " / 5";
                        progressBarImage.color = new Color(74 / 255f, 217 / 255f, 38 / 255f);
                        progressBarBackgroundImage.color = new Color(58 / 255f, 157 / 255f, 35 / 255f);
                    }
                }
                gameObject.GetComponent<Animator>().runtimeAnimatorController = animatorControllers[0];
                break;
            case FoodType.Hexagon:
                GetComponent<AudioSource>().PlayOneShot(audioClips[1]);
                hexagonRenderer.SetActive(true);
                circleRenderer.SetActive(false);
                squareRenderer.SetActive(false);
                if (hexagonMission)
                {
                    hexagonFood++;
                    progressBarImage.fillAmount = hexagonFood / 5f;
                    progressBarText.text = hexagonFood + " / 5";
                    if (hexagonFood == 5)
                    {
                        GetComponent<AudioSource>().PlayOneShot(audioClips[0]);
                        GetComponent<AudioSource>().PlayOneShot(audioClips[3]);
                        confetti.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0);
                        horizontalSpeed = 0;
                        verticalSpeed = 0;
                        retryButton.gameObject.SetActive(true);
                    }
                }
                gameObject.GetComponent<Animator>().runtimeAnimatorController = animatorControllers[1];
                break;
            case FoodType.Square:
                GetComponent<AudioSource>().PlayOneShot(audioClips[1]);
                squareRenderer.SetActive(true);
                circleRenderer.SetActive(false);
                hexagonRenderer.SetActive(false);
                if (squareMission)
                {
                    squareFood++;
                    questImage.sprite = questSprites[2];
                    progressBarImage.fillAmount = squareFood / 5f;
                    progressBarText.text = squareFood + " / 5";
                    if (squareFood == 5)
                    {
                        circleMission = true;
                        squareMission = false;
                        hexagonMission = false;
                        questImage.sprite = questSprites[0];
                        questImage.color = new Color(255 / 255f, 12 / 255f, 0 / 255f);
                        progressBarImage.fillAmount = circleFood / 5f;
                        progressBarText.text = circleFood + " / 5";
                        progressBarImage.color = new Color(255 / 255f, 12 / 255f, 0 / 255f);
                        progressBarBackgroundImage.color = new Color(155 / 255f, 35 / 255f, 35 / 255f);

                    }
                }
                gameObject.GetComponent<Animator>().runtimeAnimatorController = animatorControllers[2];
                break;
            case FoodType.Cross:
                StartCoroutine(Shake(.15f, .2f));
                break;

        }
    }

    void FixedUpdate()
    {
        playerCoords = new Vector3(playerTransform.position.x, playerTransform.position.y, playerTransform.position.z);

        transform.position += new Vector3(horizontalSpeed * Time.deltaTime, 0, 0);


        counter += 1 * Time.deltaTime;

        if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = Input.mousePosition;
            if (Camera.main.ScreenToWorldPoint(mousePos).y > playerTransform.position.y + .1f)
            {
                transform.position += new Vector3(0, verticalSpeed * Time.deltaTime, 0);
                transform.rotation = Quaternion.Euler(0, 0, 60);
            }
            else if (Camera.main.ScreenToWorldPoint(mousePos).y < playerTransform.position.y - .1f)
            {
                transform.position += new Vector3(0, -verticalSpeed * Time.deltaTime, 0);
                transform.rotation = Quaternion.Euler(0, 0, -60);
            }
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (counter >= 0.35 && generateFood)
        {
            counter = 0;
            int prefab = Random.Range(0, 4);
            randomCoords = new Vector3(playerCoords.x + 33, Random.Range(-60, 60) / 10, 0);
            Instantiate(Prefabs[prefab], randomCoords, Quaternion.identity);
        }

        if (playerTransform.position.x >= coordCounter.x + 35)
        {
            background[bgorder].transform.Translate(17 * 2.048f * 3, 0, 0);
            coordCounter = new Vector2(playerTransform.position.x, playerTransform.position.y);
            bgorder += 1;
            if (bgorder == 3)
            {
                bgorder = 0;
            }

        }
    }
    private IEnumerator Shake(float duration, float magnitude)
    {
        float elapsed = 0.0f;
        float originalY = transform.position.y;

        while (elapsed < duration)
        {

            float y = Random.Range(-1f, 1f) * magnitude;

            transform.position += new Vector3(0, y, 0);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.position = new Vector3(transform.position.x, originalY, transform.position.z);
    }
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void StartGame()
    {
        GetComponent<AudioSource>().PlayOneShot(audioClips[2]);
        generateFood = true;
        horizontalSpeed = 10;
        verticalSpeed = 15;
        startButton.gameObject.SetActive(false);
    }
}
