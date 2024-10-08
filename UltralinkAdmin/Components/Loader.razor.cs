using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltralinkAdmin.Services;

namespace UltralinkAdmin.Components
{
    public partial class Loader
    {
        public bool ShowDiv { get; set; } = false;

        protected override void OnInitialized()
        {
            LoaderService.OnMessage += MessageHandler;
        }

        public void Dispose()
        {
            LoaderService.OnMessage -= MessageHandler;
        }

        private void MessageHandler(bool directive)
        {
            ShowDiv = directive;
            StateHasChanged();
        }
    }
}
