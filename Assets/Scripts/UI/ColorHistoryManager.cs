using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorHistoryManager : MonoBehaviour
{
    [SerializeField]
    private GameObject colorHistorySquarePrefab;
    [SerializeField]
    private Vector2 marginSize;
    [SerializeField]
    private Vector2 squareSize;

    private Stack<GameObject> colorHistoryObjects;
    private RectTransform parent;
    private int colorHistorySize;


    // Start is called before the first frame update
    private void OnEnable()
    {
        colorHistoryObjects = new Stack<GameObject>();
        parent = GetComponent<RectTransform>();
        GameEventSystem.Current.RegisterListener<PlayerPopColorInfo>(popHandler);
        GameEventSystem.Current.RegisterListener<PlayerPushColorInfo>(pushHandler);
    }

    private void Start()
    {
        colorHistorySize = GameConstants.Current.getLevelData().historySize;
        parent.sizeDelta = new Vector2((marginSize.x + squareSize.x / 2) + (colorHistorySize * squareSize.x + marginSize.x * (colorHistorySize - 1)), squareSize.y + marginSize.y * 2);
    }

    private void OnDestroy()
    {
        if (GameEventSystem.Current != null)
        {
            GameEventSystem.Current.UnregisterListener<PlayerPopColorInfo>(popHandler);
            GameEventSystem.Current.UnregisterListener<PlayerPushColorInfo>(pushHandler);
        }
    }

    public void popHandler(PlayerPopColorInfo info)
    {
        GameObject toDestroy = colorHistoryObjects.Pop();
        StartCoroutine(DestroyColorHistorObject(toDestroy));
    }

    IEnumerator DestroyColorHistorObject(GameObject toDestroy)
    {
        toDestroy.GetComponent<Animator>().SetTrigger("Pop Color");
        yield return new WaitForSeconds(0.6f);
        Destroy(toDestroy);
    }

    public void pushHandler(PlayerPushColorInfo info)
    {
        GameObject toAdd = Instantiate(colorHistorySquarePrefab, this.gameObject.transform);
        RectTransform rectTransform = toAdd.GetComponent<RectTransform>();
        UnityEngine.UI.Image image = toAdd.GetComponent<UnityEngine.UI.Image>();
        image.color = GameConstants.Current.ColorList.colors[info.Color];
        rectTransform.position = new Vector2((marginSize.x + squareSize.x/2) + (colorHistoryObjects.Count * squareSize.x + marginSize.x * colorHistoryObjects.Count), squareSize.y/2 + marginSize.y);
        colorHistoryObjects.Push(toAdd);
    }
}
