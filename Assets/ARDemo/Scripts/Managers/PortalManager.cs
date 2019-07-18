using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : BasicManager<PortalManager>
{
    [SerializeField]
    private GameObject _endGameScreen;
    [SerializeField]
    private GameObject _portal;
    [SerializeField]
    private GameObject _doggo;
    private bool _isOpened;
    public void OpenPortal()
    {
        _portal.SetActive(true);
        _doggo.SetActive(true);
        _isOpened = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_isOpened)
            return;

        if (other.GetComponent<MainCharacterController>() != null)
        {
            _endGameScreen.SetActive(true);
        }
    }
}
