namespace UltralinkAdmin.Services
{
    public interface ILoaderService
    {
        event Action<bool> OnMessage;

        void ShowTheLoader(bool directive);
    }

    public class LoaderService : ILoaderService
    {
        public event Action<bool> OnMessage;

        public void ShowTheLoader(bool directive)
        {
            OnMessage?.Invoke(directive);
        }
    }
}
