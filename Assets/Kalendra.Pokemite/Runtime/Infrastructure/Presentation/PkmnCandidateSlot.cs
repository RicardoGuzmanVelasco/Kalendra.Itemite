using System.Threading.Tasks;
using Kalendra.Itemite.Runtime.Infrastructure;
using Kalendra.Pokemite.Runtime.Domain;
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

        public async Task AnimateResult(PkmnVisualDto selected)
        {
            ShowResult(10, false);
            await Task.Delay(1000);
            HideResult();
        }
    }
}