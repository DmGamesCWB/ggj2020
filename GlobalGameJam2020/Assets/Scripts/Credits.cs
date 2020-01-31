using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public void PlayAnim()
    {
        GetComponent<Animation>();
        StartCoroutine(LoadAfterAnim());
    }

    private IEnumerator LoadAfterAnim()
    {
        yield return new WaitForSeconds(01);
        AudioManager.instance.StopBG(Sound.SoundTypes.CreditsTheme);
        SceneManager.LoadScene("MainMenu");
    }

    private void PlayCreditsBG()
    {
        AudioManager.instance.StopBG(Sound.SoundTypes.MainTheme);
        AudioManager.instance.PlayBG(Sound.SoundTypes.CreditsTheme);
    }
}
