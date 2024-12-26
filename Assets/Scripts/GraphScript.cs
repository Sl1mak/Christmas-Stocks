using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphScript : MonoBehaviour
{
    public float yMaxLimit = 2f, yMinLimit = -2f, priceChangeMax, priceChangeMin, startPrice, priceMultiplier, waitSeconds, changeUp, changeDown;

    private float currentPrice = 0f, xPos = -4f, price, nowPrice, priceChange;
    private int maxPoints = 40, sell;

    public GameObject lineStripPrefab;
    public Text priceText, priceFixText;
    public Button buyButton, sellButton;

    private LineRenderer lineRenderer;
    private GameObject currentLineStrip;
    private TextMesh textMesh;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        lineRenderer.positionCount = 0;
        sell = 0;

        price = startPrice;
        StartCoroutine(GraphCoroutine());
    }

    IEnumerator GraphCoroutine()
    {
        while (true)
        {
            Debug.Log(price);
            AddPoint();
            CenterGraphIfNeeded();
            yield return new WaitForSeconds(waitSeconds);
        }
    }

    private void AddPoint()
    {
        // Добавляем новую точку с изменением цены
        float hui = Random.Range(changeDown, changeUp);
        if (price > 5) 
        {
            if (hui > 0) { priceChange = Random.Range(0, priceChangeMax); }
            else { priceChange = Random.Range(priceChangeMin, 0); }
        }
        else { priceChange = priceChangeMax; }
        if (priceChange > 0) { lineRenderer.endColor = Color.green; }
        else { lineRenderer.endColor = Color.red; }

        currentPrice += priceChange;
        price += priceChange * priceMultiplier;
        nowPrice = price;
        float nowPriceRounded = Mathf.Round(nowPrice * 100) / 100;
        priceText.text = nowPriceRounded.ToString();

        // Увеличиваем количество точек в LineRenderer
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, new Vector3(xPos, currentPrice, 0));

        // Если точек больше, чем maxPoints, удаляем самую левую
        if (lineRenderer.positionCount > maxPoints)
        {
            Vector3[] positions = new Vector3[lineRenderer.positionCount];
            lineRenderer.GetPositions(positions);

            List<Vector3> positionList = new List<Vector3>(positions);
            positionList.RemoveAt(0);

            for (int i = 0; i < positionList.Count; i++)
            {
                Vector3 position = positionList[i];
                position.x -= 0.5f;
                positionList[i] = position;
            }

            lineRenderer.positionCount = positionList.Count;
            lineRenderer.SetPositions(positionList.ToArray());
        }
        else { xPos += 0.25f; }
    }

    private void CenterGraphIfNeeded()
    {
        Vector3[] positions = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(positions);

        // Последняя добавленная точка
        Vector3 lastPoint = positions[positions.Length - 1];
        float yShift = 0f;

        // Проверяем, выходит ли последняя точка за пределы
        if (lastPoint.y > yMaxLimit)
        {
            yShift = lastPoint.y - yMaxLimit;
            if (currentLineStrip != null)
            {
                currentLineStrip.transform.position = new Vector3(currentLineStrip.transform.position.x, currentLineStrip.transform.position.y - yShift, currentLineStrip.transform.position.z);
            }
        }
        else if (lastPoint.y < yMinLimit)
        {
            yShift = lastPoint.y - yMinLimit;
            if (currentLineStrip != null)
            {
                currentLineStrip.transform.position = new Vector3(currentLineStrip.transform.position.x, currentLineStrip.transform.position.y - yShift, currentLineStrip.transform.position.z);
            }
        }

        // Если есть необходимость в смещении, корректируем все точки
        if (Mathf.Abs(yShift) > 0f)
        {
            for (int i = 0; i < positions.Length; i++)
            {
                positions[i].y -= yShift;
            }
            lineRenderer.SetPositions(positions);

            // Корректируем текущую цену
            currentPrice -= yShift;
        }
    }

    public void CreateLineStrip(string name)
    {
        Vector3 lastPoint = lineRenderer.GetPosition(lineRenderer.positionCount - 1);
        float freezePrice = nowPrice;
        float freezePriceRounded = Mathf.Round(freezePrice * 100) / 100;

        if (PlayerScript.balance >= freezePrice)
        {
            currentLineStrip = Instantiate(lineStripPrefab, new Vector3(0, lastPoint.y, 0), Quaternion.identity);
            currentLineStrip.name = name;
            textMesh = currentLineStrip.GetComponentInChildren<TextMesh>();
            textMesh.text = freezePriceRounded.ToString();
            priceFixText.text = freezePriceRounded.ToString();
            PlayerScript.balance -= freezePrice;
            sell = 1;
        }
    }

    public void DestroyLineStrip()
    {
        Image sellButtonImage = sellButton.GetComponent<Image>();
        Color color = sellButtonImage.color;

        if (currentLineStrip != null)
        {
            sell = 0;
            priceFixText.text = "0";
            PlayerScript.balance += nowPrice;
            Destroy(currentLineStrip);
        }
    }

    private void Update()
    {
        if (sell == 0)
        {
            Image buyButtonImage = buyButton.GetComponent<Image>();
            Color colorBuy = buyButtonImage.color;
            colorBuy.a = 1f;
            buyButtonImage.color = colorBuy;
            buyButton.interactable = true;

            Image sellButtonImage = sellButton.GetComponent<Image>();
            Color color = sellButtonImage.color;
            color.a = 0.5f;
            sellButtonImage.color = color;
            sellButton.interactable = false;
        }
        else
        {
            Image buyButtonImage = buyButton.GetComponent<Image>();
            Color colorBuy = buyButtonImage.color;
            colorBuy.a = 0.5f;
            buyButtonImage.color = colorBuy;
            buyButton.interactable = false;

            Image sellButtonImage = sellButton.GetComponent<Image>();
            Color color = sellButtonImage.color;
            color.a = 1f;
            sellButtonImage.color = color;
            sellButton.interactable = true;
        }
    }
}
