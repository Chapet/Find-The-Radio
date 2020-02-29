using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public GameObject scrollView;
    public GameObject slotPrefab;
    public CanvasGroup canvasGroup;
    public GameObject contentPanel;


    private float fadeDuration = 0.3f;

    public void Show(List<Item> inventory)
    {
        foreach(Item i in inventory)
        {
            GameObject obj = Instantiate(slotPrefab);
            obj.GetComponent<InventorySlot>().AddItem(i);
            obj.transform.SetParent(contentPanel.transform, false);
        }
        scrollView.SetActive(true);
        StartCoroutine(DoFade(0, 1));
    }

    public void Clear()
    {
        foreach (Transform child in contentPanel.transform)
        {
            Destroy(child.gameObject);
        }
        if (scrollView.activeSelf)
        {
            StartCoroutine(DoFade(1, 0));
            scrollView.SetActive(false);
        }
    }

    private IEnumerator DoFade(float start, float end)
    {
        float counter = 0f;

        while (counter < fadeDuration)
        {
            counter += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, counter / fadeDuration);

            yield return null;
        }
    }
}
