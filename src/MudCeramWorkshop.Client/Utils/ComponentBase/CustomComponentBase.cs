using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Globalization;

namespace MudCeramWorkshop.Client.Utils.ComponentBase
{
    public abstract class CustomComponentBase : Microsoft.AspNetCore.Components.ComponentBase, IDisposable
    {
        [Inject] public NavigationManager NavigationManager { get; set; } = default!;
        [Inject] public IDialogService Dialog { get; set; } = default!;
        [Inject] public IHttpClientFactory HttpClientFactory { get; set; } = default!;
        [Inject] public ISnackbar Snackbar { get; set; } = default!;

        [CascadingParameter] public Task<UserInfoState> UserInfoState { get; set; } = default!;

        protected static CultureInfo CurrentCultur => CultureInfo.CreateSpecificCulture("fr-fr");

        protected CancellationToken ComponentDisposed
        {
            get
            {
                if (_cancellationTokenSource == null)
                {
                    _cancellationTokenSource = new CancellationTokenSource();
                }
                return _cancellationTokenSource.Token;
            }
        }
        private CancellationTokenSource? _cancellationTokenSource;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
        }
    }
}