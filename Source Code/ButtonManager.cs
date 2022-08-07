using UnityEngine;
using System.Collections;
using Photon.Pun;

public class ButtonManager : MonoBehaviour
{
    private bool canPress = true;
    
    private void OnTriggerEnter(Collider other)
    {
        if (canPress && other.name == "LeftHandTriggerCollider")
        {
            canPress = false;
            GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(67, true, 0.05f);
            GorillaTagger.Instance.StartVibration(true, GorillaTagger.Instance.tapHapticStrength / 2f, GorillaTagger.Instance.tapHapticDuration);
            if (PhotonNetwork.InRoom && GorillaTagger.Instance.myVRRig != null)
            {
                /*PhotonView.Get(GorillaTagger.Instance.myVRRig).RPC("PlayHandTap", RpcTarget.Others, 67, true, 0.05f);*/
            }
            StartCoroutine(ButtonPressed());
        }
    }

    IEnumerator ButtonPressed()
    {
        yield return new WaitForSeconds(0.5f);
        ThatWasEasy.Plugin.ButtonPressFunction();
        canPress = true;
    }
}