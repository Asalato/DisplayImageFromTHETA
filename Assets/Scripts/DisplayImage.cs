// 参考：https://qiita.com/mczkzk/items/c21cba8228fbeb7c2bb0

using System;
using UnityEngine;

public class DisplayImage : MonoBehaviour
{
    [SerializeField] private string deviceName;
    [SerializeField] private GameObject sphere;

    private Material _sphereMaterial;
    private WebCamTexture _webCamTexture;
    
    private void Start()
    {
        _sphereMaterial = sphere.GetComponent<Renderer>().material;
        
        Init();
    }

    private void OnDestroy()
    {
        if (_webCamTexture != null)
            _webCamTexture.Stop();
    }

    /// <summary>
    /// Initialize System (First Time Only)
    /// </summary>
    private void Init()
    {
        var device = new WebCamDevice();

        if (!FindDevice(ref device))
        {
            Debug.LogError($"Cannot find device: {deviceName}");
            return;
        }
        
        _webCamTexture = new WebCamTexture(device.name);
        _webCamTexture.Play();

        _sphereMaterial.mainTexture = _webCamTexture;
    }

    private bool FindDevice(ref WebCamDevice target)
    {
        var devices = WebCamTexture.devices;
        
        foreach (var device in devices)
        {
            if (!device.name.Contains(deviceName)) continue;
            target = device;
            return true;
        }

        return false;
    }
}
