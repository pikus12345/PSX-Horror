using TMPro;
using UnityEngine;
using VContainer;

namespace NLB.Interaction
{
    public class PlayerInteractorView : MonoBehaviour
    {
        [SerializeField] private TMP_Text hintText;
        [SerializeField] private PlayerInteractor interactor;

        private void OnEnable()
        {
            interactor.OnStartView += HandleStartView;
            interactor.OnExitView += HandleExitView;
        }
        private void OnDisable()
        {
            interactor.OnStartView -= HandleStartView;
            interactor.OnExitView -= HandleExitView;
        }
        private void HandleStartView(string hint)
        {
            hintText.text = hint;
        }
        private void HandleExitView()
        {
            hintText.text = "";
        }
    }
}