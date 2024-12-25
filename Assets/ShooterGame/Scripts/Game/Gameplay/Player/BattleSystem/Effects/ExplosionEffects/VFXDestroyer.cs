using UnityEngine;
using System.Collections;

public class VFXDestroyer : MonoBehaviour
{
    bool destroyNow;

    public void DestroyVFX(float time){
        Destroy(gameObject, time);
        
        StartCoroutine("VFXsizeChanger");
    }

    IEnumerator VFXsizeChanger(){
        if(!destroyNow)
        {
            yield return new WaitForSeconds(4);
        }

        destroyNow = true;
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z) * 0.95f;

        yield return new WaitForSeconds(0.01f);

        StartCoroutine("VFXsizeChanger");
    }
}
