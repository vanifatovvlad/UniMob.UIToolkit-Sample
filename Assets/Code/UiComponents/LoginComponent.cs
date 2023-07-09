using System;
using Code.Store;
using UniMob;
using UniMob.UIToolkit;
using UnityEngine;
using UnityEngine.UIElements;

namespace Code.UiComponents
{
    public class LoginComponent : UiTemplateComponent
    {
        private readonly ViewStore _viewStore;
        private readonly Action _afterLogin;

        [Atom] private string UserName { get; set; } = string.Empty;
        [Atom] private string UserPassword { get; set; } = string.Empty;
        [Atom] private string Message { get; set; } = "Login with 'user' and '1234'";

        public LoginComponent(ViewStore viewStore, Action afterLogin)
            : base(Resources.Load<VisualTreeAsset>("Login"))
        {
            _viewStore = viewStore;
            _afterLogin = afterLogin;
        }

        public override void Init(VisualElement root)
        {
            root.Q<TextField>("user-name").Render(Lifetime, () => UserName);
            root.Q<TextField>("user-pass").Render(Lifetime, () => UserPassword);
            root.Q<Label>("message").Render(Lifetime, () => Message);

            root.Q<TextField>("user-name").OnChange(Lifetime, v => UserName = v);
            root.Q<TextField>("user-pass").OnChange(Lifetime, v => UserPassword = v);

            root.Q<Button>().OnClick(Lifetime, () => Login());
        }

        private void Login()
        {
            Message = "Verifying credentials...";

            _viewStore.PerformLogin(UserName, UserPassword, authenticated =>
            {
                if (authenticated)
                {
                    Message = "Login accepted";
                    _afterLogin?.Invoke();
                }
                else
                {
                    Message = "Login failed";
                }
            });
        }
    }
}