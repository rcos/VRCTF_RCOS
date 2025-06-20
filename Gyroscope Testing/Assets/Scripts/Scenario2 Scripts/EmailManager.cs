using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class EmailManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject emailItemPrefab;
    public Transform emailListContent;
    public TMP_InputField searchInput;

    [Header("Email Settings")]
    public int numberOfEmails = 20;
    public int maxSubjectLength = 35;

    private List<string> words = new List<string>();
    private List<EmailItem> allEmailItems = new List<EmailItem>();
    private bool alreadyGenerated = false;

    void Start()
    {
        LoadWords();
        
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
            GameObject emailGO = Instantiate(emailItemPrefab, emailListContent);
            EmailItem emailItem = emailGO.GetComponent<EmailItem>();

            string label = $"Email #{i}";
            string subject = Truncate(GenerateSentence(3, 6), maxSubjectLength);
            string content = GenerateRandomParagraph();

            emailItem.LabelText.text = label;
            emailItem.SubjectText.text = subject;
            emailItem.FullContent = content;

            allEmailItems.Add(emailItem);
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

        int randomIndex = Random.Range(0, allEmailItems.Count);
        EmailItem emailItem = allEmailItems[randomIndex];

        emailItem.LabelText.text = "CTF Code";
        emailItem.SubjectText.text = "Your tax refund was processed";
        emailItem.FullContent = "Congratulations! Here's your CTF Code: FLAG{this_is_your_flag}";
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

}
