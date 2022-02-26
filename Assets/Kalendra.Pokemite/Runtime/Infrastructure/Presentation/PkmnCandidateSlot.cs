using Kalendra.Itemite.Runtime.Infrastructure;
using UnityEngine;

namespace Kalendra.Pokemite.Runtime.Infrastructure.Presentation
{
    public class PkmnCandidateSlot : MonoBehaviour
    {
        [SerializeField] OutlinedLabel resultLabel;
        GameObject labelContainer;

        void Start()
        {
            HideResult();
        }

        public void ShowResult(int points, bool isWinnerResult)
        {
            resultLabel.Color = isWinnerResult ? Color.green : Color.red;
            resultLabel.Text = points.ToString();

            resultLabel.gameObject.SetActive(true);
        }

        public void HideResult()
        {
            resultLabel.gameObject.SetActive(false);
        }
    }
}