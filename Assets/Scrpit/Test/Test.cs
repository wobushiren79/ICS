using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : BaseMonoBehaviour ,IUserDataView{

    public void CreateUserDataFail(string msg)
    {
       
    }

    public void CreateUserDataSuccess(UserDataBean userData)
    {
 
    }

    public void SaveUserDataFail(string msg)
    {
      
    }

    public void SaveUserDataSuccess(UserDataBean userData)
    {
  
    }

    // Use this for initialization
    void Start () {
        UserDataController dataController = new UserDataController(this,this);
        dataController.CreateUserData();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
