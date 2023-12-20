using UnityEngine;
using WebSocketSharp;

public class WebSocketClient : MonoBehaviour
{
    private WebSocket ws;
    private int currentQuestionId;

    [System.Serializable]
    public class AnswerMessage
    {
        public string type;
        public string answer;
        public int questionId;
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
        ws.OnOpen += (sender, e) => Debug.Log("Connecté au serveur WebSocket.");
        ws.OnMessage += (sender, e) =>
        {
            Debug.Log("Message reçu du serveur: " + e.Data);
            var message = JsonUtility.FromJson<ServerMessage>(e.Data);
            if (message.type == "question")
            {
                currentQuestionId = message.question.questionId;
                Debug.Log("Question ID reçue: " + currentQuestionId);
            }
        };
        ws.OnError += (sender, e) => Debug.LogError("Erreur WebSocket: " + e.Message);
        ws.OnClose += (sender, e) => Debug.Log("Déconnecté du serveur WebSocket.");
        ws.Connect();
    }

    public void SendAnswer(string answer)
    {
        if (ws.ReadyState == WebSocketState.Open)
        {
            AnswerMessage message = new AnswerMessage
            {
                type = "submitAnswer",
                answer = answer,
                questionId = currentQuestionId
            };
            string jsonMessage = JsonUtility.ToJson(message);
            Debug.Log("Envoi de la réponse: " + jsonMessage);
            ws.Send(jsonMessage);
        }
        else
        {
            Debug.LogError("La connexion WebSocket n'est pas ouverte.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) { Debug.Log("Touche A pressée."); SendAnswer("A"); }
        else if (Input.GetKeyDown(KeyCode.B)) { Debug.Log("Touche B pressée."); SendAnswer("B"); }
        else if (Input.GetKeyDown(KeyCode.C)) { Debug.Log("Touche C pressée."); SendAnswer("C"); }
        else if (Input.GetKeyDown(KeyCode.D)) { Debug.Log("Touche D pressée."); SendAnswer("D"); }
    }

    void OnApplicationQuit()
    {
        if (ws != null) ws.Close();
    }

    public void OnAnswerButtonClicked(string answer)
    {
        Debug.Log($"Réponse {answer} sélectionnée.");
        SendAnswer(answer);
    }

}
