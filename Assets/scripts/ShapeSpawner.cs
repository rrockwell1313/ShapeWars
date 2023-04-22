using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShapeSpawner : MonoBehaviour
{
    public CurrencyManager currencyManager;
    private GameManager gameManager;
    public GameObject trianglePrefab;
    public GameObject squarePrefab;
    public GameObject diamondPrefab;
    public GameObject capsulePrefab;
    public GameObject hexagonPrefab;
    public Collider2D playAreaCollider;

    private int cost = 0;
    private GameObject selectedShape;
    private GameObject shapeToSelect = null;


    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        currencyManager.UpdateCurrencyText();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && selectedShape != null)
        {
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPosition.z = 0;

            // Check if the mouse click is within the play area
            if (playAreaCollider.OverlapPoint(mouseWorldPosition) && !gameManager.gameEnded)
            {
                

                if (selectedShape.CompareTag("Player"))
                {
                    if (currencyManager.currency1 >= cost)
                    {
                        currencyManager.ChangeCurrency1(-cost);
                        currencyManager.UpdateCurrencyText();
                        Instantiate(selectedShape, mouseWorldPosition, Quaternion.identity);
                    }
                }
                else if (selectedShape.CompareTag("Player2"))
                {
                    if (currencyManager.currency2 >= cost)
                    {
                        currencyManager.ChangeCurrency2(-cost);
                        currencyManager.UpdateCurrencyText();
                        Instantiate(selectedShape, mouseWorldPosition, Quaternion.identity);
                    }
                }
            }
        }
    }




    public void SelectShape(int shapeType)
    {

        switch (shapeType)
        {
            case 1: // Triangle
                cost = 3;
                shapeToSelect = trianglePrefab;
                break;
            case 2: // Square
                cost = 4;
                shapeToSelect = squarePrefab;
                break;
            case 3: // Diamond
                cost = 4;
                shapeToSelect = diamondPrefab;
                break;
            case 4: // Pentagon
                cost = 5;
                shapeToSelect = capsulePrefab;
                break;
            case 5: // Hexagon
                cost = 6;
                shapeToSelect = hexagonPrefab;
                break;
        }
        selectedShape = shapeToSelect;

    }

}


