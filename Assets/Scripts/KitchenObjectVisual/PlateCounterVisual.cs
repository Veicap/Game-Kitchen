using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounterVisual : MonoBehaviour
{
    [SerializeField] private Transform plateViusal;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private PlateCounter plateCounter;

    private List<GameObject> PlateList;
    private void Awake()
    {
        PlateList = new List<GameObject>();
    }

    private void Start()
    {
        plateCounter.OnSpawnPlate += PlateCounter_OnSpawnPlate;
        plateCounter.OnDestroyPlate += PlateCounter_OnDestroyPlate;
    }

    private void PlateCounter_OnDestroyPlate(object sender, System.EventArgs e)
    {
        GameObject plateDestroyed = PlateList[PlateList.Count - 1];
        PlateList.Remove(plateDestroyed); 
        Destroy(plateDestroyed);
    }

    private void PlateCounter_OnSpawnPlate(object sender, System.EventArgs e)
    {
        Transform plateVisualTransform = Instantiate(plateViusal, counterTopPoint);
        float offsetPosY = 0.1f;
        plateVisualTransform.localPosition = new Vector3(0, offsetPosY * PlateList.Count , 0);
        PlateList.Add(plateVisualTransform.gameObject);
    }
    
}
