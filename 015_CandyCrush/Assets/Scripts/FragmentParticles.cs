using UnityEngine;
using System.Collections;

public class FragmentParticles : MonoBehaviour
{
    void OnEnable()
    {
        Messenger.AddListener(MessageTypes.PortalDestroyed, StopParticle);
    }
    
    void OnDisable()
    {
        Messenger.RemoveListener(MessageTypes.PortalDestroyed, StopParticle);
    }
    
    void StopParticle()
    {
        particleSystem.Stop();
    }
}
