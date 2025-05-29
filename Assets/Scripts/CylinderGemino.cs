using NUnit.Framework;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class CylinderGemino : MonoBehaviour
{
    [SerializeField] private GameObject geminiObject;
    [SerializeField] private List<GameObject> geminiList;

    void Start()
    {
        this.geminiObject.SetActive(false);
        float secs = Random.Range(1.0f, 2.0f);
        this.StartCoroutine(this.TriggerEvery(secs));
    }

     void Update()
    {
        
    }

    private void OnDestroy()
    {
        this.geminiList.Clear();
    }

    private IEnumerator TriggerEvery(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        this.TriggerGemino();
        this.StartCoroutine(this.TriggerEvery(Random.Range(0.2f, 0.5f)));
    }

    private void TriggerGemino()
    {
        int number = Random.Range(1, 4);
        for (int i = 0; i < number; i++)
        {
            GameObject obj = ObjectUtils.SpawnDefault(geminiObject, this.transform, this.geminiObject.transform.localPosition);
            this.geminiList.Add(obj);
        }
    }

    public void RandomizeGeminiColor()
    {
        for (int i = 0; i < this.geminiList.Count; i++)
        {
            float r = Random.Range(0.0f, 1.0f);
            float g = Random.Range(0.0f, 1.0f);
            float b = Random.Range(0.0f, 1.0f);

            Color color = new Color(r, g, b);
            this.geminiList[i].GetComponent<MeshRenderer>().material.SetColor("_Color", color);
        }
    }
}
