using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Game.CrossTheRoad
{

    public class CanvasManager : MonoBehaviour
    {

        public Image[] imgHearts;
        public Text txtDucklingCnt;

        public void ReduceLife(int cnt)
        {
            for(var i = cnt; cnt <= 0; cnt--)
            {
                imgHearts[i].enabled = false;
            }
        }

        public void SetDucklingCount(int cnt)
        {
            txtDucklingCnt.text = cnt.ToString();
        }
    }

}