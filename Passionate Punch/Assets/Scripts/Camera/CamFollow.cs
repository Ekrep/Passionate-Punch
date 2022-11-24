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
    private float _camFirstPosZ;
    private void OnEnable()
    {
        GameManager.OnSendCharacter += GameManager_OnSendCharacter;
        GameManager.OnShakeCam += GameManager_OnShakeCam;
        GameManager.OnStopShakeCam += GameManager_OnStopShakeCam;
    }

    private void GameManager_OnStopShakeCam()
    {
        StopCoroutine(ShakeCam(0));
        _currentType = CamCurrentType.Static;
        gameObject.transform.position = new Vector3(_camFirstPosX, gameObject.transform.position.y,_camFirstPosZ);
        
    }

    private void GameManager_OnShakeCam(float shakeRange)
    {
        _camFirstPosZ = gameObject.transform.position.z;
        _camFirstPosX = gameObject.transform.position.x;
        _currentType = CamCurrentType.Dynamic;
        StartCoroutine(ShakeCam(shakeRange));
        
    }

    private void GameManager_OnSendCharacter(CharacterBaseStateMachine obj)
    {
        _followedbyCamObject = obj.gameObject;
        _distance = _followedbyCamObject.transform.position - gameObject.transform.position;
    }

    private void OnDisable()
    {
        GameManager.OnSendCharacter -= GameManager_OnSendCharacter;
        GameManager.OnShakeCam -= GameManager_OnShakeCam;
        GameManager.OnStopShakeCam -= GameManager_OnStopShakeCam;
    }


    void Start()
    {
        
        
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


    IEnumerator ShakeCam(float shakeRange)
    {
       
        float randomValueX =Random.Range(-shakeRange,shakeRange);
        float randomValueZ = Random.Range(-shakeRange, shakeRange);
        gameObject.transform.position = new Vector3(Mathf.Clamp(gameObject.transform.position.x + randomValueX, _camFirstPosX-shakeRange, _camFirstPosX+shakeRange), gameObject.transform.position.y, Mathf.Clamp(gameObject.transform.position.z + randomValueZ, _camFirstPosZ - shakeRange, _camFirstPosZ + shakeRange));

        yield return new WaitForEndOfFrame();

        gameObject.transform.position = new Vector3(Mathf.Clamp(gameObject.transform.position.x - randomValueX, _camFirstPosX - shakeRange, _camFirstPosX + shakeRange), gameObject.transform.position.y, Mathf.Clamp(gameObject.transform.position.z - randomValueZ, _camFirstPosZ - shakeRange, _camFirstPosZ + shakeRange));
    }

    
}
