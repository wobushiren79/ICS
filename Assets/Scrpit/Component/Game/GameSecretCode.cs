using UnityEngine;
using UnityEditor;

public class GameSecretCode : SecretCode
{
    public GameDataCpt gameDataCpt;
    public override void SecretCodeHandler(string code)
    {
        switch (code)
        {
            case "IAMRICH":
                gameDataCpt.userData.userScore = gameDataCpt.userData.userScore*2;
                break;
        }
    }
}