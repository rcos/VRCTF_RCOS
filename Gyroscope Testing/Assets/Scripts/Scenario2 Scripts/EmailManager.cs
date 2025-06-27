using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class EmailManager : MonoBehaviour
{
    [Header("UI References")]
    // References to UI pages
    public GameObject emailListPage;
    public GameObject emailDetailPage;
    public Transform emailListContent;
    public TMP_InputField searchInput;

    [Header("Email Settings")]
    public int numberOfEmails;
    public int maxSubjectLength;

    private List<string> words = new List<string>();
    private List<EmailItem> allEmailItems = new List<EmailItem>();
    private bool alreadyGenerated = false;

    // Email detail UI 
    public TMP_Text detailSubjectText;
    public TMP_Text detailFromText;
    public TMP_Text detailToText;
    public TMP_Text detailContentText;

    void Start()
    {
        // Add listener to search bar
        if (searchInput != null)
            searchInput.onValueChanged.AddListener(FilterEmails);
    }

    void LoadWords()
    {
        TextAsset wordFile = Resources.Load<TextAsset>("words");

        string[] lines = wordFile.text.Split(new[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);

        foreach (string word in lines)
        {
            string cleaned = word.Trim().ToLower();
            if (cleaned.Length > 2 && cleaned.Length < 12)
                words.Add(char.ToUpper(cleaned[0]) + cleaned.Substring(1));
        }

        Debug.Log($"Loaded {words.Count} words.");
    }

    void GenerateEmails()
    {
        for (int i = 1; i <= numberOfEmails; i++)
        {
            // 1. Create Button as root
            GameObject emailGO = new GameObject($"Email_{i}", typeof(RectTransform), typeof(Button), typeof(Image));
            emailGO.transform.SetParent(emailListContent, false);

            // Background
            Image bg = emailGO.GetComponent<Image>();
            bg.color = new Color(1f, 1f, 1f, 0.05f);

            Button button = emailGO.GetComponent<Button>();

            LayoutElement layout = emailGO.AddComponent<LayoutElement>();
            layout.preferredHeight = 80;

            // 2. Add EmailItem
            EmailItem emailItem = emailGO.AddComponent<EmailItem>();

            // 3. Generate data
            string label = $"Email{i}";
            string subject = Truncate(GenerateSentence(3, 6), maxSubjectLength);
            string content = GenerateRandomParagraph();

            emailItem.LabelName = label;
            emailItem.SubjectLine = subject;
            emailItem.FullContent = content;

            // 4. Label Text
            GameObject labelGO = new GameObject("LabelText", typeof(RectTransform));
            labelGO.transform.SetParent(emailGO.transform, false);
            TMP_Text labelText = labelGO.AddComponent<TextMeshProUGUI>();
            labelText.fontSize = 10;
            labelText.alignment = TextAlignmentOptions.TopLeft;
            labelText.enableWordWrapping = false;
            labelText.text = label;
            emailItem.LabelText = labelText;

            RectTransform labelRT = labelGO.GetComponent<RectTransform>();
            labelRT.anchorMin = new Vector2(0, 0.5f);
            labelRT.anchorMax = new Vector2(1, 1);
            labelRT.offsetMin = new Vector2(10, -10);
            labelRT.offsetMax = new Vector2(-10, -5);

            // 5. Subject Text
            GameObject subjectGO = new GameObject("SubjectText", typeof(RectTransform));
            subjectGO.transform.SetParent(emailGO.transform, false);
            TMP_Text subjectText = subjectGO.AddComponent<TextMeshProUGUI>();
            subjectText.fontSize = 16;
            subjectText.color = Color.gray;
            subjectText.alignment = TextAlignmentOptions.BottomLeft;
            subjectText.enableWordWrapping = true;
            subjectText.text = subject;
            emailItem.SubjectText = subjectText;

            RectTransform subjectRT = subjectGO.GetComponent<RectTransform>();
            subjectRT.anchorMin = new Vector2(0, 0);
            subjectRT.anchorMax = new Vector2(1, 0.5f);
            subjectRT.offsetMin = new Vector2(10, 5);
            subjectRT.offsetMax = new Vector2(-10, 10);

            // 6. Click event
            button.onClick.AddListener(() => ShowEmailDetail(emailItem));

            allEmailItems.Add(emailItem);
             // Debug log
            Debug.Log($"[Email {i}] Label: {label}");
            Debug.Log($"[Email {i}] Subject: {subject}");
            Debug.Log($"[Email {i}] Content (first 25 chars): {content.Substring(0, Mathf.Min(25, content.Length))}...");
    }

        Debug.Log($"Generating {numberOfEmails} emails");
    }


    void InsertCTFEmail()
    {   
        if (allEmailItems.Count == 0)
        {
            Debug.LogWarning("No emails to insert CTF into!");
            return;
        }

        int halfIndex = allEmailItems.Count / 2;
        int randomIndex = Random.Range(halfIndex, allEmailItems.Count);
        Debug.Log(randomIndex);
        EmailItem emailItem = allEmailItems[randomIndex];

        emailItem.LabelText.text = "RPI";
        emailItem.SubjectText.text = "CTF Code";
        emailItem.FullContent = "Congratulations! Here's your code: 12345";
    }

    string GenerateSentence(int minWords, int maxWords)
    {
        int wordCount = Random.Range(minWords, maxWords + 1);
        List<string> sentenceWords = new List<string>();

        for (int i = 0; i < wordCount; i++)
        {
            sentenceWords.Add(words[Random.Range(0, words.Count)]);
        }

        string sentence = string.Join(" ", sentenceWords);
        return char.ToUpper(sentence[0]) + sentence.Substring(1) + ".";
    }

    string GenerateRandomParagraph()
    {
        int sentenceCount = Random.Range(3, 6);
        List<string> sentences = new List<string>();

        for (int i = 0; i < sentenceCount; i++)
        {
            sentences.Add(GenerateSentence(6, 10));
        }

        return string.Join(" ", sentences);
    }

    string Truncate(string text, int maxLength)
    {
        return text.Length > maxLength ? text.Substring(0, maxLength - 3) + "..." : text;
    }

    public void FilterEmails(string query)
    {
        string lowerQuery = query.ToLower();

        foreach (EmailItem email in allEmailItems)
        {
            bool matches = email.FullContent.ToLower().Contains(lowerQuery);
            email.gameObject.SetActive(matches);
        }
    }

    public void ShowEmailScreen()
    {
    if (!alreadyGenerated)
    {   
        if (words.Count == 0)
        {
            Debug.Log("Reloading words...");
            LoadWords();
        }

        Debug.Log("Calling GenerateEmails");
        GenerateEmails();
        Debug.Log("Calling InsertCTFEmail");
        InsertCTFEmail();

        alreadyGenerated = true;
    }

    Debug.Log("showEmailScreen successfully.");
    }   


    void ShowEmailDetail(EmailItem item)
    {
        emailListPage.SetActive(false);
        emailDetailPage.SetActive(true);

        detailSubjectText.text = item.SubjectLine;
        detailFromText.text = $"{item.LabelName} <no-reply@{item.LabelName.ToLower()}.com>";
        detailToText.text = "to me";
        detailContentText.text = item.FullContent;
    }

    public void BackToList()
    {
        emailListPage.SetActive(true);
        emailDetailPage.SetActive(false);
    }
}
