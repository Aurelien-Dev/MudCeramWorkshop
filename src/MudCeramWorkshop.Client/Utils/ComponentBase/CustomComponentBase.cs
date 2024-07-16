using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace MudCeramWorkshop.Client.Utils.ComponentBase
{
    public abstract class CustomComponentBase : Microsoft.AspNetCore.Components.ComponentBase, IDisposable
    {
        [Inject] public NavigationManager NavigationManager { get; set; } = default!;
        [Inject] public IDialogService Dialog { get; set; } = default!;
        [Inject] public IHttpClientFactory HttpClientFactory { get; set; } = default!;

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