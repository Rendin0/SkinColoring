using System.IO;
using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class VidPlayer : MonoBehaviour
{
    [SerializeField] string fileName;
    private VideoPlayer _videoPlayer;

    private void Awake()
    {
        _videoPlayer = GetComponent<VideoPlayer>();
    }

    public void PlayVideo()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);
        _videoPlayer.url = filePath;
        _videoPlayer.Play();
    }

    public void StopVideo()
    {
        _videoPlayer.Stop();
    }

}