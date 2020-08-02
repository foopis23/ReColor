using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;

public class ColorHistoryManager : MonoBehaviour
{
    [SerializeField]
    private GameObject colorHistorySquarePrefab;

    private Stack<GameObject> colorHistoryObjects;

    // Start is called before the first frame update
    private void Awake()
    {
        colorHistoryObjects = new Stack<GameObject>();
        GameEventSystem.Current.RegisterListener<PlayerPopColorInfo>(popHandler);
        GameEventSystem.Current.RegisterListener<PlayerPushColorInfo>(pushHandler);
    }

    private void OnDestroy()
    {
        GameEventSystem.Current.UnregisterListener<PlayerPopColorInfo>(popHandler);
        GameEventSystem.Current.UnregisterListener<PlayerPushColorInfo>(pushHandler);
    }

    public void popHandler(PlayerPopColorInfo info)
    {
        GameObject toDestroy = colorHistoryObjects.Pop();
        Destroy(toDestroy);
    }

    public void pushHandler(PlayerPushColorInfo info)
    {
        GameObject toAdd = Instantiate(colorHistorySquarePrefab, this.gameObject.transform);
        RectTransform rectTransform = toAdd.GetComponent<RectTransform>();
        UnityEngine.UI.Image image = toAdd.GetComponent<UnityEngine.UI.Image>();
        image.color = GameConstants.Current.ColorList.colors[info.Color];
        rectTransform.position = new Vector3(10 + (colorHistoryObjects.Count * 15), 10, 0);
        colorHistoryObjects.Push(toAdd);
    }
}
