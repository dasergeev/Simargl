using System.Collections.Concurrent;

namespace Simargl.Mobile.MobileRegistrar
{
    [CLSCompliant(false)]
    public partial class MainPage : ContentPage
    {

        private readonly ConcurrentQueue<System.Numerics.Vector3> _Accelerations;

        public MainPage()
        {
            InitializeComponent();
            _Accelerations = [];
            var cancellationToken = ((App)Application.Current!).CancellationToken;

            //  Запуск асинхронной работы.
            _ = Task.Run(async delegate
            {
                await InvokeAsync(cancellationToken).ConfigureAwait(false);
            }, cancellationToken);

            Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
            Accelerometer.Start(SensorSpeed.Fastest);


        }

        private void Accelerometer_ReadingChanged(object? sender, AccelerometerChangedEventArgs e)
        {
            _Accelerations.Enqueue(e.Reading.Acceleration);
        }

        private async Task InvokeAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                System.Text.StringBuilder output = new();
                output.AppendLine($"Ticks {DateTime.Now}");

                List<System.Numerics.Vector3> accelerations = [];
                while (_Accelerations.TryDequeue(out var value))
                {
                    accelerations.Add(value);
                }

                output.Append("Accelerations: ");
                if (accelerations.Count > 0)
                {
                    float x = accelerations.Select(x => x.X).Average();
                    float y = accelerations.Select(x => x.Y).Average();
                    float z = accelerations.Select(x => x.Z).Average();
                    output.AppendLine($"({x}; {y}; {z})");
                }
                else
                {
                    output.AppendLine("null");
                }


                output.Append("Geolocation: ");
                try
                {
                    Location? location = null;
                    await MainThread.InvokeOnMainThreadAsync(async delegate
                    {
                        location = await Geolocation.GetLocationAsync(
                            new GeolocationRequest(GeolocationAccuracy.Medium),
                            cancellationToken).ConfigureAwait(false);
                    }).ConfigureAwait(false);

                    if (location is not null)
                    {
                        output.AppendLine($"{location.Latitude}, {location.Longitude}, {location.Speed}");
                    }
                    else
                    {
                        output.AppendLine("null");
                    }
                }
                catch (System.Exception ex)
                {
                    output.AppendLine($"{ex}");
                }


                /*

                

        if (location != null)
        {
            // Выводим полученные координаты
            Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}");
        }
                */

                await MainThread.InvokeOnMainThreadAsync(async delegate
                {
                    await Task.CompletedTask;
                    _OutputLabel.Text = output.ToString();
                    SemanticScreenReader.Announce(_OutputLabel.Text);

                }).ConfigureAwait(false);

                //MainThread.BeginInvokeOnMainThread(delegate
                //{
                //});

                await Task.Delay(1000, cancellationToken).ConfigureAwait(false);
            }
        }
    }

}
