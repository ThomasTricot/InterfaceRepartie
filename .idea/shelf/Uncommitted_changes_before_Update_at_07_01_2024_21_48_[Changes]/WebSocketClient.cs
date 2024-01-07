using UnityEngine;
using WebSocketSharp;
using PimDeWitte.UnityMainThreadDispatcher;
using TMPro;

public class WebSocketClient : MonoBehaviour
{
    private WebSocket ws;
    private int currentQuestionId;
    private int idTable=1;
    
    public Color selectedTextColor = Color.yellow;

    [System.Serializable]
    public class AnswerMessage
    {
        public string type;
        public string answer;
        public int questionId;
        public int tableId;
    }

    [System.Serializable]
    public class ServerMessage
    {
        public string type;
        public QuestionData question;
    }

    [System.Serializable]
    public class QuestionData
    {
        public int questionId;
    }

    void Start()
    {
        ws = new WebSocket("ws://localhost:8080");
        ws.OnOpen += (sender, e) => Debug.Log("Connect� au serveur WebSocket.");
        ws.OnMessage += (sender, e) =>
        {
            Debug.Log("Message re�u du serveur: " + e.Data);
            var message = JsonUtility.FromJson<ServerMessage>(e.Data);
            if (message.type == "question")
            {
                currentQuestionId = message.question.questionId;
                Debug.Log("Question ID re�ue: " + currentQuestionId);
                UnityMainThreadDispatcher.Instance().Enqueue(() => GameController.Instance.PlayWarningSoundDirectly());
            }
        };

        ws.OnError += (sender, e) => Debug.LogError("Erreur WebSocket: " + e.Message);
        ws.OnClose += (sender, e) => Debug.Log("D�connect� du serveur WebSocket.");
        ws.Connect();
    }

    public void SendAnswer(string answer, int tableID)
    {
        if (ws.ReadyState == WebSocketState.Open)
        {
            AnswerMessage message = new AnswerMessage
            {
                type = "submitAnswer",
                answer = answer,
                questionId = currentQuestionId,
                tableId = tableID
            };
            string jsonMessage = JsonUtility.ToJson(message);
            Debug.Log("Envoi de la r�ponse: " + jsonMessage);
            ws.Send(jsonMessage);
        }
        else
        {
            Debug.LogError("La connexion WebSocket n'est pas ouverte.");
        }
    }

    public void SubmitFinal(int tableID)
    {
        if (ws.ReadyState == WebSocketState.Open)
        {
            var message = new
            {
                type = "submitFinal",
                tableID = tableID
            };
            string jsonMessage = JsonUtility.ToJson(message);
            Debug.Log("Envoi de submitFinal: " + jsonMessage);
            ws.Send(jsonMessage);
        }
        else
        {
            Debug.LogError("La connexion WebSocket n'est pas ouverte.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) { Debug.Log("Touche A press�e."); SendAnswer("A", idTable); }
        else if (Input.GetKeyDown(KeyCode.B)) { Debug.Log("Touche B press�e."); SendAnswer("B", idTable); }
        else if (Input.GetKeyDown(KeyCode.C)) { Debug.Log("Touche C press�e."); SendAnswer("C", idTable); }
        else if (Input.GetKeyDown(KeyCode.D)) { Debug.Log("Touche D press�e."); SendAnswer("D", idTable); }
    }

    void OnApplicationQuit()
    {
        if (ws != null) ws.Close();
    }

    public void OnAnswerButtonClicked(string answer)
    {
        if (!GameController.Instance.answerSelected) // Utilise l'indicateur de GameController
        {
            Debug.Log($"R�ponse {answer} s�lectionn�e. id table {idTable}");
            SendAnswer(answer,idTable);
            GameController.Instance.answerSelected = true; // Met � jour l'indicateur dans GameController

            // R�active les audios (sauf warning) dans GameController
            GameController.Instance.ToggleSound();

            // Change la couleur du texte du bouton
            TextMeshProUGUI textComp = GetComponentInChildren<TextMeshProUGUI>();
            if (textComp != null)
            {
                textComp.color = selectedTextColor;
            }
        }
    }

    private void HandleQuestionReceived()
    {
        Debug.Log("test");
        GameController.Instance.ToggleSound(); // Bascule le son
        // Vous pouvez ajouter ici d'autres logiques sp�cifiques � la r�ception d'une question
    }

}
