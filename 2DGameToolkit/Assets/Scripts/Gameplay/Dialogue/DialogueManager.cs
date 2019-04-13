using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private Text m_DialogeText;
    [SerializeField] private Text m_NameText;
    [SerializeField] private Animator m_Animator;

    //private Queue<Dialogue.Sentence> m_Sentences;
    private static string ms_DialogueDirectory = "/Dialogues/";

    void Start ()
    {
        //m_Sentences = new Queue<Dialogue.Sentence> ();
        TriggerDialogue("bob.xml");
    }

    //public void StartDialogue (Dialogue dialogue)
    //{
    //    m_Animator.SetBool ("IsOpen", true);
    //    m_Sentences.Clear ();

    //    foreach (Dialogue.Sentence sentence in dialogue.m_Sentences)
    //    {
    //        m_Sentences.Enqueue (sentence);
    //    }

    //    DisplayNextSentence ();
    //}

    //public void DisplayNextSentence ()
    //{
    //    if (m_Sentences.Count == 0)
    //    {
    //        EndDialogue ();
    //        return;
    //    }
    //    StopAllCoroutines ();
    //    //Dialogue.Sentence sentence = m_Sentences.Dequeue ();
    //    m_NameText.text = sentence.m_Name;
    //    StartCoroutine (TypeSentence (sentence.m_Sentence));
    //}

    IEnumerator TypeSentence (string sentence)
    {
        m_DialogeText.text = "";
        foreach (char letter in sentence.ToCharArray ())
        {
            m_DialogeText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue ()
    {
        m_Animator.SetBool ("IsOpen", false);
    }

    public void TriggerDialogue (string tag)
    {
        string filename = Application.streamingAssetsPath + ms_DialogueDirectory + tag;
        Dialogue.Dialogue graphDeserialized = XMLSerializerHelper.Deserialize<Dialogue.Dialogue>(filename);
        //StartDialogue(dialogue);
    }
}

public class DialogueManagerProxy : UniqueProxy<DialogueManager>
{
}

