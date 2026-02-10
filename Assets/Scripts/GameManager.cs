using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] GameObject audioPanel;
    [SerializeField] Slider musicSlider;

    [SerializeField] AudioMixer mixer;
    [SerializeField] string exposedMusicParamName;

    public void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score;
    }

    public void ToggleAudio()
    {
        audioPanel.SetActive(!audioPanel.activeSelf);
    }

    public void SetMusicVolume()
    {
        float volumeInDb = Mathf.Log10(Mathf.Max(musicSlider.value, 0.00001f)) * 20f;
        mixer.SetFloat(exposedMusicParamName, volumeInDb);
    }
}
