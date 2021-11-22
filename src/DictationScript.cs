using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

public class DictationScript : MonoBehaviour
{
    private bool isActivated_ = false;
    private bool buttonPressed_ = false;
    private GameObject uiSpace;
    private Text Subtitulos;
    [SerializeField]
    public Text m_Hypotheses;

    //[SerializeField]
    //public Text m_Recognitions;

    private DictationRecognizer m_DictationRecognizer;

    void Start()
    {
        uiSpace = GameObject.Find("Subtitulos");
        Subtitulos = uiSpace.GetComponent<Text>();
        Subtitulos.text = "";
        m_DictationRecognizer = new DictationRecognizer();
        m_DictationRecognizer.DictationResult += Result;
        m_DictationRecognizer.DictationHypothesis += Hypothesis;
        m_DictationRecognizer.DictationComplete += DictComplete;
        m_DictationRecognizer.DictationError += DictError;
    }

    void Update()
    {
        if (buttonPressed_)
        {
            if(isActivated_)
            {
                m_DictationRecognizer.Stop();
                isActivated_ = false;
                Debug.Log("Stop Dictation");
                // PhraseRecognitionSystem.Restart();
                // Debug.Log(PhraseRecognitionSystem.Status);
            }
            else
            {
                // PhraseRecognitionSystem.Shutdown();
                // Debug.Log(PhraseRecognitionSystem.Status);
                m_DictationRecognizer.Start();
                isActivated_ = true;
                Debug.Log("Start Dictation");
            }
            buttonPressed_ = false;
        }
        if(isActivated_)
        {   
            if (Subtitulos.text.Length >= 100)
            {
                Subtitulos.text = "";
            }
        }
        
    }

    void OnDestroy()
    {
        m_DictationRecognizer.Dispose();
    }

    void Result(string text, UnityEngine.Windows.Speech.ConfidenceLevel confidence)
    {
        //m_Recognitions.text += text + " ";
        Debug.LogFormat("Dictation result: {0}; confidence = {1}", text, confidence);
    }

    void Hypothesis(string text)
    {
        Debug.LogFormat("Dictation hypothesis: {0}", text);
        Subtitulos.text = "";
        m_Hypotheses.text += text;
    }

    void DictComplete(UnityEngine.Windows.Speech.DictationCompletionCause completionCause)
    {
        if (completionCause != DictationCompletionCause.Complete)
                Debug.LogErrorFormat("Dictation completed unsuccessfully: {0}.", completionCause);
    }

    void DictError (string error, int hresult)
    {
        Debug.LogErrorFormat("Dictation error: {0}; HResult = {1}.", error, hresult);
    }

    public void pressButton()
    {
        buttonPressed_ = true;
    }
}