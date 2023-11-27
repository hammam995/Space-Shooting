using Pool;
using System.Collections;
using UnityEngine;

public class HitVFX : MonoBehaviour
{
    public IPool<HitVFX> M_Pool
    {
        private get;
        set;
    }
    private void OnEnable()
    {
        StartCoroutine(ReturnToPool());
    }

    private IEnumerator ReturnToPool()
    {
        yield return new WaitForSeconds(2f);
        M_Pool.Return(this);
        
    }
}
