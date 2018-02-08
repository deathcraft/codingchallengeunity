using Moments;
using UnityEngine;

namespace CodingChallenge
{
    public class GifRecord : MonoBehaviour
    {
        private bool saved;
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
            if (!saved)
            {
                FindObjectOfType<Recorder>().Save();
                saved = true;
                Debug.Log("End record");
                started = false;
            }
        }
    }
}