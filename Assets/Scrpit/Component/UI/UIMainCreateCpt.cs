using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class UIMainCreateCpt : BaseUIComponent,IUserDataView
{
    //文字
    public Text tvSubmit;
    public Text tvCancel;
    public Text tvHint;
    //按钮-提交
    public Button btSubmit;
    //按钮-取消
    public Button btCancel;
    //输入-名字
    public InputField ETName;
    //提示
    public GameToastCpt cptToast;

    //数据控制
    private UserDataController mUserDataController;

    private void Awake()
    {
        mUserDataController = new UserDataController(this,this);
    }

    private void Start()
    {
        if (btCancel != null)
            btCancel.onClick.AddListener(BTCancelOnClick);
        if (btSubmit != null)
            btSubmit.onClick.AddListener(BTSubmitOnClick);
        RefreshUI();
    }


    public override void RefreshUI()
    {
        base.RefreshUI();
        if (tvHint != null)
            tvHint.text = GameCommonInfo.GetTextById(29);
        if (tvSubmit != null)
            tvSubmit.text = GameCommonInfo.GetTextById(30);
        if (tvCancel != null)
            tvCancel.text = GameCommonInfo.GetTextById(31);
    }

    /// <summary>
    /// 退出按钮点击
    /// </summary>
    public void BTCancelOnClick()
    {
        uiManager.OpenUIAndCloseOtherByName("Data");
    }

    /// <summary>
    /// 提交按钮点击
    /// </summary>
    public void BTSubmitOnClick()
    {
        if (ETName == null)
        {
            return;
        }
        if (CheckUtil.StringIsNull(ETName.text)&&cptToast!=null)
        {
            cptToast.ToastHint(GameCommonInfo.GetTextById(28));
            return;
        }
        mUserDataController.CreateUserData(ETName.text);
    }

    #region 数据回调
    public void GetUserDataSuccess(UserDataBean userData)
    {
        throw new System.NotImplementedException();
    }

    public void GetUserDataFail(UserDataModel.UserDataFailEnum failEnum)
    {
        throw new System.NotImplementedException();
    }

    public void CreateUserDataSuccess(UserDataBean userData)
    {
        GameCommonInfo.gameUserId = userData.userId;
        SceneUtil.SceneChange("GameScene");
    }

    public void CreateUserDataFail(UserDataModel.UserDataFailEnum failEnum)
    {
        
    }

    public void SaveUserDataSuccess(UserDataBean userData)
    {
        throw new System.NotImplementedException();
    }

    public void SaveUserDataFail(UserDataModel.UserDataFailEnum failEnum)
    {
        throw new System.NotImplementedException();
    }

    public void DeleteUserDataSuccess(UserDataBean userData)
    {
        throw new System.NotImplementedException();
    }

    public void DeleteUserDataFail(UserDataModel.UserDataFailEnum failEnum)
    {
        throw new System.NotImplementedException();
    }

    public void ChangeUserDataSuccess(UserDataBean userData)
    {
        throw new System.NotImplementedException();
    }

    public void ChangeUserDataFail(UserDataModel.UserDataFailEnum failEnum)
    {
        throw new System.NotImplementedException();
    }
    #endregion
}