using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    static public float balance;

    public Text balanceText;
    public GameObject graph_1GameObject, graph_2GameObject, graph_3GameObject, graph_4GameObject, graph_5GameObject, graph_6GameObject, graph_7GameObject, graph_8GameObject;
    public GameObject graph_1, graph_2, graph_3, graph_4, graph_5, graph_6, graph_7, graph_8;
    public GameObject graph_1Buttons, graph_2Buttons, graph_3Buttons, graph_4Buttons, graph_5Buttons, graph_6Buttons, graph_7Buttons, graph_8Buttons;

    private void Start()
    {
        balance = 200;
    }

    private void Update()
    {
        float balanceRounded = Mathf.Round(balance * 100) / 100;
        balanceText.text = balanceRounded.ToString();
    }

    public void LoadGraph(GameObject graph)
    {
        LineRenderer graph_1LineRenderer = graph_1.GetComponent<LineRenderer>();
        graph_1LineRenderer.sortingOrder = -1;

        LineRenderer graph_2LineRenderer = graph_2.GetComponent<LineRenderer>();
        graph_2LineRenderer.sortingOrder = -1;

        LineRenderer graph_3LineRenderer = graph_3.GetComponent<LineRenderer>();
        graph_3LineRenderer.sortingOrder = -1;

        LineRenderer graph_4LineRenderer = graph_4.GetComponent<LineRenderer>();
        graph_4LineRenderer.sortingOrder = -1;

        LineRenderer graph_5LineRenderer = graph_5.GetComponent<LineRenderer>();
        graph_5LineRenderer.sortingOrder = -1;

        LineRenderer graph_6LineRenderer = graph_6.GetComponent<LineRenderer>();
        graph_6LineRenderer.sortingOrder = -1;

        LineRenderer graph_7LineRenderer = graph_7.GetComponent<LineRenderer>();
        graph_7LineRenderer.sortingOrder = -1;

        LineRenderer graph_8LineRenderer = graph_8.GetComponent<LineRenderer>();
        graph_8LineRenderer.sortingOrder = -1;

        LineRenderer graphLineRenderer = graph.GetComponent<LineRenderer>();
        graphLineRenderer.sortingOrder = 1;
    }
    
    public void LoadGraphGameObject(GameObject graphGameObject)
    {
        graph_1GameObject.transform.localScale = Vector3.zero;
        graph_2GameObject.transform.localScale = Vector3.zero;
        graph_3GameObject.transform.localScale = Vector3.zero;
        graph_4GameObject.transform.localScale = Vector3.zero;
        graph_5GameObject.transform.localScale = Vector3.zero;
        graph_6GameObject.transform.localScale = Vector3.zero;
        graph_7GameObject.transform.localScale = Vector3.zero;
        graph_8GameObject.transform.localScale = Vector3.zero;
        graphGameObject.transform.localScale = Vector3.one;
    }

    public void LoadButtons(GameObject buttons)
    {
        graph_1Buttons.SetActive(false);
        graph_2Buttons.SetActive(false);
        graph_3Buttons.SetActive(false);
        graph_4Buttons.SetActive(false);
        graph_5Buttons.SetActive(false);
        graph_6Buttons.SetActive(false);
        graph_7Buttons.SetActive(false);
        graph_8Buttons.SetActive(false);
        buttons.SetActive(true);
    }

    public void LoadLines(string lineName)
    {
        GameObject line1 = GameObject.Find("Line1");
        GameObject line2 = GameObject.Find("Line2");
        GameObject line3 = GameObject.Find("Line3");
        GameObject line4 = GameObject.Find("Line4");
        GameObject line5 = GameObject.Find("Line5");
        GameObject line6 = GameObject.Find("Line6");
        GameObject line7 = GameObject.Find("Line7");
        GameObject line8 = GameObject.Find("Line8");

        if (line1 != null) { line1.transform.position = new Vector3(line1.transform.position.x, line1.transform.position.y, 120); }
        if (line2 != null) { line2.transform.position = new Vector3(line2.transform.position.x, line2.transform.position.y, 120); }
        if (line3 != null) { line3.transform.position = new Vector3(line3.transform.position.x, line3.transform.position.y, 120); }
        if (line4 != null) { line4.transform.position = new Vector3(line4.transform.position.x, line4.transform.position.y, 120); }
        if (line5 != null) { line5.transform.position = new Vector3(line5.transform.position.x, line5.transform.position.y, 120); }
        if (line6 != null) { line6.transform.position = new Vector3(line6.transform.position.x, line6.transform.position.y, 120); }
        if (line7 != null) { line7.transform.position = new Vector3(line7.transform.position.x, line7.transform.position.y, 120); }
        if (line8 != null) { line8.transform.position = new Vector3(line8.transform.position.x, line8.transform.position.y, 120); }

        GameObject activeLine = GameObject.Find(lineName);
        if (activeLine != null) { activeLine.transform.position = new Vector3(activeLine.transform.position.x, activeLine.transform.position.y, 0); }
    }
}
