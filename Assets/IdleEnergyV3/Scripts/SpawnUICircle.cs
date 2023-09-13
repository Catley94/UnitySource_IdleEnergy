using System.Collections;
using System.Collections.Generic;
using Idle_Engine.API;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpawnUICircle : MonoBehaviour
{
    public RectTransform canvas;

    [SerializeField] private GameObject energyPrefab;

    [SerializeField] private float circleValuePerSecond;

    void Start()
    {
        // SpawnCircle();
    }

    public void SpawnCircle()
    {
        
        GameObject energy = Instantiate(energyPrefab);
        
        // GameObject circle = new GameObject("Circle");
        energy.transform.SetParent(canvas.transform, false);

        RectTransform circleRectTransform = energy.GetComponent<RectTransform>();
        // circleRectTransform.sizeDelta = new Vector2(100, 100); // Set the size of the circle

        Vector2 randomPosition = GetRandomPositionWithinCanvasBounds(circleRectTransform);
        circleRectTransform.anchoredPosition = randomPosition;

        energy.transform.GetChild(0).GetComponent<ValueAnimation>().SetValue(circleValuePerSecond);
        
        IE_IdleEngine.Instance.IncreaseCurrencyPerSecondBy(circleValuePerSecond);

        // Image circleImage = circle.AddComponent<Image>();
        // circleImage.sprite = circleSprite;
    }

    Vector2 GetRandomPositionWithinCanvasBounds(RectTransform rectTransform)
    {
        RectTransform canvasRectTransform = canvas;

        float halfWidth = rectTransform.rect.width * 0.5f;
        float halfHeight = rectTransform.rect.height * 0.5f;

        float xMin = canvasRectTransform.rect.xMin + halfWidth;
        float xMax = canvasRectTransform.rect.xMax - halfWidth;
        float yMin = canvasRectTransform.rect.yMin + halfHeight;
        float yMax = canvasRectTransform.rect.yMax - halfHeight;

        float randomX = Random.Range(xMin, xMax);
        float randomY = Random.Range(yMin, yMax);

        return new Vector2(randomX, randomY);
    }

    public void IncreaseCircleValueBy(float amount)
    {
        circleValuePerSecond += amount;
    }

    public void IncreaseCircleValueTo(float amount)
    {
        circleValuePerSecond = amount;
    }
}