using Moments;
using UnityEngine;

namespace CodingChallenge
{
    public class GifRecord : MonoBehaviour
    {
        private bool started;

        public void StartRecordOnce()
        {
            if (!started)
            {
                FindObjectOfType<Recorder>().Record();
                started = true;
                Debug.Log("Start record");
            }
            else
            {
                StopRecord();
            }
        }

        public void StopRecord()
        {
            FindObjectOfType<Recorder>().Save();
            Debug.Log("End record");
            started = false;
        }
    }
}