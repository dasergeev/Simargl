namespace Simargl.Mobile.MobileRegistrar
{
    public partial class App : Application
    {
        private readonly CancellationTokenSource _CancellationTokenSource;

        public App()
        {
            _CancellationTokenSource = new();
            CancellationToken = _CancellationTokenSource.Token;

            InitializeComponent();
        }

        public CancellationToken CancellationToken { get; }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }

        
    }
}