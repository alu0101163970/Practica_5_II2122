using System;
using System.Text;
using UnityEngine;
using UnityEngine.Windows.Speech;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
using StarterAssets;
#endif

public class KeywordScript : MonoBehaviour
{

    private GameObject player;
    private StarterAssetsInputs _input;


    private bool isActivated_ = false;
    private bool buttonPressed_ = false;

    [SerializeField]
    private string[] m_Keywords;

    private KeywordRecognizer m_Recognizer;


    void Start()
    {
        ////
        player = GameObject.Find("Player");
        _input = player.GetComponent<StarterAssetsInputs>();
        ////
        dictationRecognizer =  GameObject.Find("DictationRecognizer");
        m_Keywords = new string[] {"Derecha", "Izquierda", "Avanzar", "Adelante", "Parar", "Avanzar hacia atras", "Saltar", "Correr", "Caminar"};
        m_Recognizer = new KeywordRecognizer(m_Keywords);
        m_Recognizer.OnPhraseRecognized += OnPhraseRecognized;
    }

    private void OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendFormat("{0} ({1}){2}", args.text, args.confidence, Environment.NewLine);
        builder.AppendFormat("\tTimestamp: {0}{1}", args.phraseStartTime, Environment.NewLine);
        builder.AppendFormat("\tDuration: {0} seconds{1}", args.phraseDuration.TotalSeconds, Environment.NewLine);
        Debug.Log(builder.ToString());
        ExecuteAction(args.text);
    }

    void Update()
    {
        if (buttonPressed_)
        {
            StartStopKeywords();
            buttonPressed_ = false;
        }
    }

    private void OnDestroy()
    {
        m_Recognizer.Dispose();
    }

    public void pressButton()
    {
        buttonPressed_ = true;
    }

    private void StartStopKeywords ()
    {
        if(isActivated_)
        {
            PhraseRecognitionSystem.Shutdown();
            Debug.Log(PhraseRecognitionSystem.Status);
            m_Recognizer.Stop();
            isActivated_ = false;
            Debug.Log("Stop Keyword");
        }
        else
        {
            PhraseRecognitionSystem.Restart();
            Debug.Log(PhraseRecognitionSystem.Status);
            m_Recognizer.Start();
            isActivated_ = true;
            Debug.Log("Start Keywords");
        }
    }

    private void ExecuteAction(string keyword)
    {
        switch (keyword)
        {
            case "Derecha":
            {
                Debug.Log(keyword);
                _input.move = Vector2.right;
            }
            break;
            case "Izquierda":
            {
                Debug.Log(keyword);
                _input.move = Vector2.left;
            }
            break;
            case "Avanzar":
            {
                Debug.Log(keyword);
                _input.move = Vector2.up;
            }
            break;
            case "Parar":
            {
                Debug.Log(keyword);
                _input.move = Vector2.zero;
            }
            break;
            case "Avanzar hacia atras":
            {
                Debug.Log(keyword);
                _input.move = Vector2.down;
            }
            break;
            case "Saltar":
            {
                Debug.Log(keyword);
                _input.jump = true;
            }
            break;
            case "Correr":
            {
                Debug.Log(keyword);
                _input.sprint = true;
            }
            break;
            case "Caminar":
            {
                Debug.Log(keyword);
                _input.sprint = false;
            }
            break;
            default:
            break;
        }
    }
}