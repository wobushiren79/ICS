using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainElementCpt : MonoBehaviour {

    public SpriteRenderer ivTable;

    public List<Sprite> listTableData;
	// Use this for initialization
	void Start () {
        if (listTableData != null && ivTable != null)
            ivTable.sprite = RandomUtil.GetRandomDataByList(listTableData);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
