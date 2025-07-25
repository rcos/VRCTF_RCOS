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
    public GameObject emailStripPrefab; 

    [Header("Email Settings")]
    public int numberOfEmails;
    public int maxSubjectLength;
    public static EmailManager Instance { get; private set; }

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


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
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
            // 1. Instantiate prefab
            GameObject emailGO = Instantiate(emailStripPrefab, emailListContent);
            emailGO.name = $"Email_{i}";

            // 2. Get components
            EmailItem emailItem = emailGO.GetComponent<EmailItem>();

              if (emailItem == null)
                {
                    Debug.LogError($"EmailItem missing on prefab for Email {i}");
                    continue;
                }

                if (emailItem.LabelText == null || emailItem.SubjectText == null)
                {
                    Debug.LogError($"Missing Text components on Email {i}");
                    continue;
                }

            Button button = emailGO.GetComponent<Button>();

            // 3. Generate content
            string label = $"Email{i}";
            string subject = Truncate(GenerateSentence(3, 6), maxSubjectLength);
            string content = GenerateRandomParagraph();

            // 4. Set values
            emailItem.LabelName = label;
            emailItem.SubjectLine = subject;
            emailItem.FullContent = content;

            emailItem.LabelText.text = label;
            emailItem.SubjectText.text = subject;

            // 5. Add click behavior
            button.onClick.AddListener(() => {
                Debug.Log("Clicked email index: " + i);
                ShowEmailDetail(emailItem);
            });
            
            // 6. Track
            allEmailItems.Add(emailItem);

            Debug.Log($"[Email {i}] Label: {label}");
            Debug.Log($"[Email {i}] Subject: {subject}");
            Debug.Log($"[Email {i}] Content (first 25 chars): {content.Substring(0, Mathf.Min(25, content.Length))}...");
        }

        Debug.Log($"Generated {numberOfEmails} emails.");
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

    public void FilterEmails(string searchQuery)
    {
        searchQuery = searchQuery.ToLower();

        foreach (Transform email in emailListContent.transform)
        {
            EmailItem emailItem = email.GetComponent<EmailItem>();
            bool matches = emailItem.FullContent.ToLower().Contains(searchQuery);
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
