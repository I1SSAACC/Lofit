using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public struct AddPlayerMessage : NetworkMessage { }
public struct RemovePlayerMessage : NetworkMessage { }

public struct SceneMessage : NetworkMessage
{
    public string sceneName;
    public LoadSceneMode loadMode;
}

public class MatchManager : NetworkBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private ButtonClickInformer _addToQueue;
    [SerializeField] private ButtonClickInformer _removeFromQueue;
    [SerializeField] private LoadSceneMode _sceneMode;
    [SerializeField] private int _requiredPlayers;

    private readonly List<NetworkConnectionToClient> _playersInQueue = new();
    private readonly List<Scene> _loadedScenes = new();

    private void OnEnable()
    {
        _addToQueue.Clicked += AddPlayerToQueue;
        _removeFromQueue.Clicked += RemovePlayerFromQueue;
    }

    private void OnDisable()
    {
        _addToQueue.Clicked -= AddPlayerToQueue;
        _removeFromQueue.Clicked -= RemovePlayerFromQueue;
    }

    public override void OnStartServer()
    {
        Debug.LogWarning("������ �������");
        NetworkServer.RegisterHandler<AddPlayerMessage>(OnAddedPlayer);
        NetworkServer.RegisterHandler<RemovePlayerMessage>(OnRemovedPlayer);
    }

    public override void OnStopServer()
    {
        Debug.LogWarning("������ ����������");
        NetworkServer.UnregisterHandler<AddPlayerMessage>();
        NetworkServer.UnregisterHandler<RemovePlayerMessage>();
    }

    public override void OnStartClient()
    {
        Debug.LogWarning("������ �������������");
        // ������������ ���������� ������ ��������� ��� �������� �����
        NetworkClient.RegisterHandler<SceneMessage>(OnSceneMessageReceived);
        // ������������ ��������� ������ ������
        NetworkClient.RegisterPrefab(_playerPrefab);
    }

    public override void OnStopClient()
    {
        Debug.LogWarning("��������� �������");
    }

    public override void OnStartLocalPlayer()
    {
        Debug.LogWarning("OnStartLocalPlayer()");
    }

    public override void OnStartAuthority()
    {
        Debug.LogWarning("OnStartAuthority()");
    }

    public override void OnStopAuthority()
    {
        Debug.LogWarning("OnStopAuthority()");
    }

    private void AddPlayerToQueue() =>
        NetworkClient.Send(new AddPlayerMessage());

    private void RemovePlayerFromQueue() =>
        NetworkClient.Send(new RemovePlayerMessage());

    private void OnAddedPlayer(NetworkConnectionToClient conn, AddPlayerMessage _)
    {
        _playersInQueue.Add(conn);

        if (_playersInQueue.Count >= _requiredPlayers)
            StartMatch();
    }

    private void OnRemovedPlayer(NetworkConnectionToClient connection, RemovePlayerMessage _)
    {
        if (connection != null)
            _playersInQueue.Remove(connection);
    }

    private void StartMatch()
    {
        Debug.Log($"_playersInQueue.Count = {_playersInQueue.Count}");
        // ������ ����� �����������
        List<NetworkConnectionToClient> connections = new List<NetworkConnectionToClient>(_playersInQueue);
        _playersInQueue.Clear();
        StartCoroutine(CreateMatch(connections));
    }

    private IEnumerator CreateMatch(List<NetworkConnectionToClient> connections)
    {
        // ��������� ����� ����� (��������, Playground) ���������� �� �������
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(Constants.Scenes.Match, LoadSceneMode.Additive);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        Scene loadedScene = SceneManager.GetSceneByName(Constants.Scenes.Match);
        if (loadedScene.isLoaded)
        {
            _loadedScenes.Add(loadedScene);
            // ������������� ����������� ����� ��� �������� �� �������
            SceneManager.SetActiveScene(loadedScene);
        }
        else
        {
            Debug.LogError("����� �� ���� ���������!");
            yield break;
        }

        // ���������� ��������� ��������, ����� ��� ����� ��������� ����� �����
        SceneMessage sceneMsg = new SceneMessage
        {
            sceneName = Constants.Scenes.Match,
            loadMode = _sceneMode
        };
        foreach (NetworkConnectionToClient conn in connections)
        {
            conn.Send(sceneMsg);
        }

        // ���� �������� ����� �� �������� ����� (��� ������������� ��������� ��������)
        yield return new WaitForSeconds(2f);

        // ���������� ������� � ����������� � �������� ����� �� �������
        foreach (NetworkConnectionToClient connection in connections)
        {
            MovePlayersInScene(connection);
            yield return new WaitForEndOfFrame();
        }
    }

    // ���������� ��������� ��� �������� ����� �� �������
    private void OnSceneMessageReceived(SceneMessage msg)
    {
        StartCoroutine(LoadAndActivateScene(msg));
    }

    private IEnumerator LoadAndActivateScene(SceneMessage msg)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(msg.sceneName, msg.loadMode);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        Scene loadedScene = SceneManager.GetSceneByName(msg.sceneName);
        if (loadedScene.IsValid() && loadedScene.isLoaded)
        {
            SceneManager.SetActiveScene(loadedScene);
            Debug.Log("������: ��������� � ������������ ����� " + msg.sceneName);
        }
        else
        {
            Debug.LogError("������: �� ������� ��������� ����� " + msg.sceneName);
        }
    }

    // �����, ������� �� ������� �������� ������ ����� �������� � �������� (�����) �����
    private void MovePlayersInScene(NetworkConnectionToClient connection)
    {
        // �������� ������ ������ ������
        GameObject oldPlayer = connection.identity.gameObject;

        // ������� ����� ������ ������
        GameObject newPlayer = Instantiate(_playerPrefab);
        // ���������� ����� ������ � �������� (�����) �����
        Scene newScene = SceneManager.GetActiveScene();
        if (newPlayer.scene != newScene)
            SceneManager.MoveGameObjectToScene(newPlayer, newScene);

        // �������� ������ ��� �����������, �������� ���������
        NetworkServer.ReplacePlayerForConnection(connection, newPlayer, ReplacePlayerOptions.KeepAuthority);

        Destroy(oldPlayer);
    }
}