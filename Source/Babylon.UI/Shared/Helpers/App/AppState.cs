using System;
using System.Threading.Tasks;
using Babylon.Model.Constants;

namespace Babylon.UI.Shared.Helpers.App
{
    public class AppState
    {
        public TypeActions.Action SelectedOperation { get; private set; }

        public event Func<TypeActions.Action, Task> OnChange;

        public async Task SetOperation(TypeActions.Action operation)
        {
            SelectedOperation = operation;
            await NotifyStateChanged();
        }

        public async Task NotifyStateChanged()
            => await OnChange?.Invoke(SelectedOperation)!;

    }
}