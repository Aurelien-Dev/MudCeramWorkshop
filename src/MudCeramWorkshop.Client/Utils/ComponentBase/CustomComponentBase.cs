using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace MudCeramWorkshop.Client.Utils.ComponentBase
{
    public abstract class CustomComponentBase : Microsoft.AspNetCore.Components.ComponentBase, IDisposable
    {
        [Inject] public NavigationManager NavigationManager { get; set; } = default!;

        protected CancellationToken ComponentDisposed => (_cancellationTokenSource ??= new()).Token;
        private CancellationTokenSource? _cancellationTokenSource;

        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
        }
    }
}