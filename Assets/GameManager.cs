using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float velocidad = 2;
    public Renderer bg;
    public GameObject col1;
    public GameObject piedra1;
    public GameObject piedra2;
    public bool start = false;
    public bool gameOver = false;

    // SCORE
    public float scoreFloat = 0; // score real
    public int score = 0;        // score redondeado
    public TMPro.TextMeshProUGUI scoreText;

    public GameObject menuInicio;
    public GameObject menuGameOver;

    public List<GameObject> suelo;
    public List<GameObject> obstaculos;

    private void Start()
    {
        // Ocultar score al inicio
        if (scoreText != null)
        {
            scoreText.gameObject.SetActive(false);
            scoreText.text = "Score: 0";
        }

        // Crear Mapa
        for (int i = 0; i < 21; i++)
        {
            suelo.Add(Instantiate(col1, new Vector2(-10 + i, -3), Quaternion.identity));
        }

        // Crear Obstaculos
        obstaculos.Add(Instantiate(piedra1, new Vector2(15, -2), Quaternion.identity));
        obstaculos.Add(Instantiate(piedra2, new Vector2(20, -2), Quaternion.identity));
    }

    private void Update()
    {
        if (!start && !gameOver)
        {
            menuInicio.SetActive(true);

            if (Input.GetKeyDown(KeyCode.X))
            {
                start = true;

                scoreFloat = 0;
                score = 0;

                if (scoreText != null)
                {
                    scoreText.gameObject.SetActive(true);
                    scoreText.text = "Score: 0";
                }
            }
        }
        else if (gameOver)
        {
            menuGameOver.SetActive(true);

            if (Input.GetKeyDown(KeyCode.X))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        else
        {
            menuInicio.SetActive(false);

            // Mover BG
            bg.material.mainTextureOffset += new Vector2(0.015f, 0) * velocidad * Time.deltaTime;

            // Mover suelo
            for (int i = 0; i < suelo.Count; i++)
            {
                if (suelo[i].transform.position.x <= -10)
                {
                    suelo[i].transform.position = new Vector3(10f, -3, 0);
                }
                suelo[i].transform.position += new Vector3(-1, 0, 0) * velocidad * Time.deltaTime;
            }

            // Mover obstaculos
            for (int i = 0; i < obstaculos.Count; i++)
            {
                if (obstaculos[i].transform.position.x <= -10)
                {
                    float randomObs = Random.Range(10, 18);
                    obstaculos[i].transform.position = new Vector3(randomObs, -2, 0);
                }
                obstaculos[i].transform.position += new Vector3(-1, 0, 0) * velocidad * Time.deltaTime;
            }

            // SCORE — AHORA FUNCIONA DE VERDAD
            scoreFloat += 2f * Time.deltaTime; // sube 2 por segundo real
            score = Mathf.FloorToInt(scoreFloat);

            scoreText.text = "Score: " + score.ToString();
        }
    }
}
