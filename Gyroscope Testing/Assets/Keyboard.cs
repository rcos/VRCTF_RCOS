using UnityEngine;
using System.Linq;
using TMPro;

public class Keyboard : MonoBehaviour
{
    public string currentString = "";
    public float keyMargins_Hor = 4.0f;
    public float keyMargins_Ver = 4.0f;
    public GameObject keyPrefab;

    private int totalRows = 5;
    private RectTransform rectTrans = null;
    private GameObject[][] Allkeys = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        /*
        rectTrans = GetComponent<RectTransform>();
        Allkeys = new GameObject[totalRows][];
        makeRow(rectTrans.rect.width, rectTrans.rect.height, 0, new string[] {"`", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "-", "=", "<--"},
                                                                new int[]    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1});
        makeRow(rectTrans.rect.width, rectTrans.rect.height, 1, new string[] {"Tab", "q", "w", "e", "r", "t", "y", "u", "i", "o", "p", "[", "]", "\\"},
                                                                new int[]    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1});
        makeRow(rectTrans.rect.width, rectTrans.rect.height, 2, new string[] {"CAPS", "a", "s", "d", "f", "g", "h", "j", "k", "l", ";", "'", "Enter"},
                                                                new int[]    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2});
        makeRow(rectTrans.rect.width, rectTrans.rect.height, 3, new string[] {"Shift", "z", "x", "c", "v", "b", "n", "m", ",", ".", "/", "Shift"},
                                                                new int[]    {2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2});
        makeRow(rectTrans.rect.width, rectTrans.rect.height, 4, new string[] {"Ctrl", "Alt", "Space", "Alt", "Ctrl"},
                                                                new int[]    {2, 2, 6, 2, 2});
        */
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void makeRow(float totalWidth, float totalHeight, int rowNumber, string[] keys, int[] sizes)
    {
        Allkeys[rowNumber] = new GameObject[keys.Length];
        int totalKeys = keys.Length;
        int totalSizes = sizes.Sum();
        float totalMarginSpace_Hor = keyMargins_Hor * (totalSizes + 2);
        float widthPerKey = (totalWidth - totalMarginSpace_Hor) / totalSizes;
        float totalMarginSpace_Ver = keyMargins_Ver * 2;
        
        float heightPerKey = (totalHeight - totalMarginSpace_Ver) / totalRows;
        for (int i = 0; i < totalKeys; i++)
        {
            Allkeys[rowNumber][i] = Instantiate(keyPrefab, transform);

            float ThisWidth = (widthPerKey * sizes[i]) + (keyMargins_Hor * (sizes[i]));
            Allkeys[rowNumber][i].GetComponent<RectTransform>().sizeDelta = new Vector2(ThisWidth, heightPerKey);

            float XPos = (ThisWidth/2)+(widthPerKey * sizes.Take(i).Sum())+(keyMargins_Hor*(sizes.Take(i).Sum()+1)); // (XPos in center, account for that) + (width of all keys before this one) + (initial margin)
            float YPos = (-1 * keyMargins_Ver) + (-1 * heightPerKey * (rowNumber+1));
            Allkeys[rowNumber][i].GetComponent<RectTransform>().anchoredPosition = new Vector2(XPos, YPos);
            
            
            Transform childTransform = Allkeys[rowNumber][i].transform.Find("Text (TMP)");
            childTransform.GetComponent<TextMeshProUGUI>().text = keys[i];
        }
    }
}