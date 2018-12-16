using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class UIMainDataCpt : BaseUIComponent,IUserDataView
{
    //返回按钮
    public Button btBack;

    public GameObject listUserDataContent;
    public GameObject itemOldDataModel;
    public GameObject itemNewDataModel;

    //数据控制
    private UserDataController mUserDataController;

    private void Awake()
    {
        mUserDataController = new UserDataController(this,this);
    }

    private void Start()
    {
        RefreshData();
        if (btBack != null)
            btBack.onClick.AddListener(BTBackOnClick);
    }

    /// <summary>
    /// 刷新数据
    /// </summary>
    public void RefreshData()
    {
        if(listUserDataContent!=null)
          CptUtil.RemoveChildsByActive(listUserDataContent.transform);
        mUserDataController.GetAllUserData();
        CreateNewUserItem();
    }

    /// <summary>
    /// 返回按钮
    /// </summary>
    public void BTBackOnClick()
    {
        uiManager.OpenUIAndCloseOtherByName("Start");
    }


    #region 用户数据回调
    public void GetUserDataSuccess(UserDataBean userData)
    {
        CreateOldUserItem(userData);
    }

    public void GetUserDataFail(UserDataModel.UserDataFailEnum failEnum)
    {
     
    }

    public void CreateUserDataSuccess(UserDataBean userData)
    {
      
    }

    public void CreateUserDataFail(UserDataModel.UserDataFailEnum failEnum)
    {
       
    }

    public void SaveUserDataSuccess(UserDataBean userData)
    {
    
    }

    public void SaveUserDataFail(UserDataModel.UserDataFailEnum failEnum)
    {
    
    }

    public void DeleteUserDataSuccess(UserDataBean userData)
    {
     
    }

    public void DeleteUserDataFail(UserDataModel.UserDataFailEnum failEnum)
    {
        throw new System.NotImplementedException();
    }
    #endregion

    /// <summary>
    /// 创建老数据按钮
    /// </summary>
    /// <param name="userData"></param>
    private void CreateOldUserItem(UserDataBean userData)
    {
        GameObject oldItem=  Instantiate<GameObject>(itemOldDataModel, itemOldDataModel.transform);
        oldItem.name = userData.userName;
        oldItem.SetActive(true);
        oldItem.transform.parent = listUserDataContent.transform;
        //设置名字
        Text tvName=  CptUtil.GetCptInChildrenByName<Text>(oldItem,"Name");
        if(tvName!=null)
            tvName.text = userData.userName;
        //设置分数
        Text tvScore= CptUtil.GetCptInChildrenByName<Text>(oldItem, "Score");
        if (tvScore != null)
        {
            string outNumber;
           UnitUtil.UnitEnum outUnit;
                UnitUtil.DoubleToStrUnit(userData.userScore,out outNumber,out outUnit);
            tvScore.text = outNumber + " " + outUnit;
        }
        //设置删除按钮
        Button btDelete = CptUtil.GetCptInChildrenByName<Button>(oldItem, "Delete");
        btDelete.onClick.AddListener(delegate ()
        {
            mUserDataController.DeleteUserData(userData.userId);
            RefreshData();
        });
        //开始游戏
        Button btStart = oldItem.GetComponent<Button>();
        btStart.onClick.AddListener(delegate ()
        {

        });
    }

    /// <summary>
    /// 创建新数据按钮
    /// </summary>
    private void CreateNewUserItem()
    {
        GameObject newItem = Instantiate<GameObject>(itemNewDataModel, itemNewDataModel.transform);
        newItem.SetActive(true);
        newItem.transform.parent = listUserDataContent.transform;
        //创建按钮
        Button btCreate = newItem.GetComponent<Button>();
        btCreate.onClick.AddListener(delegate ()
        {
            mUserDataController.CreateUserData("Text2");
            RefreshData();
        });
    }
}