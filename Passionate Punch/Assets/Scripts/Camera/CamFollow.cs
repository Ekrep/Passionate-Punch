using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public enum CamCurrentType
    {
        Static,
        Dynamic
    }

    [SerializeField]
    private CamCurrentType _currentType;
    private Vector3 _distance;
    private GameObject _followedbyCamObject;

    private float _camFirstPosX;
    private void OnEnable()
    {
        GameManager.OnSendCharacter += GameManager_OnSendCharacter;
        GameManager.OnShakeCam += GameManager_OnShakeCam;
        GameManager.OnStopShakeCam += GameManager_OnStopShakeCam;
    }

    private void GameManager_OnStopShakeCam()
    {
        _currentType = CamCurrentType.Static;
        gameObject.transform.position = new Vector3(_camFirstPosX, gameObject.transform.position.y, gameObject.transform.position.z);
        
    }

    private void GameManager_OnShakeCam(float shakeRange)
    {
        _currentType = CamCurrentType.Dynamic;
        ShakeCam(shakeRange);
        
    }

    private void GameManager_OnSendCharacter(CharacterBaseStateMachine obj)
    {
        _followedbyCamObject = obj.gameObject;
    }

    private void OnDisable()
    {
        GameManager.OnSendCharacter -= GameManager_OnSendCharacter;
        GameManager.OnShakeCam -= GameManager_OnShakeCam;
        GameManager.OnStopShakeCam -= GameManager_OnStopShakeCam;
    }


    void Start()
    {
        _distance = _followedbyCamObject.transform.position - gameObject.transform.position;
        _camFirstPosX = gameObject.transform.position.x;
    }

    private void LateUpdate()
    {
        switch (_currentType)
        {
            case CamCurrentType.Static:
                gameObject.transform.position = _followedbyCamObject.transform.position - _distance;
                break;
            case CamCurrentType.Dynamic:
                break;
            default:
                break;
        }

        
    }


    public void ShakeCam(float shakeRange)
    {
        
        float randomValue = Mathf.Sin(Random.Range(0, 271));
        gameObject.transform.position = new Vector3(Mathf.Clamp(gameObject.transform.position.x + randomValue, _camFirstPosX-shakeRange, _camFirstPosX+shakeRange), gameObject.transform.position.y, gameObject.transform.position.z);
    }
}
