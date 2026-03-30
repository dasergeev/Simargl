namespace Apeiron.QuantumX;
public class HBMStream
{
    /*
    public static void MyStreamMetaParser(QuantumXDevice client, string method, JsonNode prm)
    {
        // Подписываемся на все имеющиеся сигналы
        if (method == Common.META_METHOD_AVAILABLE)
        {
            List<string> signalReferences = new();
            var signals = prm.AsArray();
            foreach (JsonNode? sig in signals)
            {
                signalReferences.Add(sig!.ToString());
            }
            try
            {
                var result = client.SubscribeAsync(signalReferences);
            }
            catch { }
        }
        else if (method == Common.META_METHOD_UNAVAILABLE)
        {

        }
        else if (method == Common.META_METHOD_ALIVE)
        {

        }
        else if (method == Common.META_METHOD_FILL)
        {

        }
        else
        {
            // Неизвестный метод. Хрюкнем в лог.
            Console.WriteLine("{0}:  unknown stream Meta method", method);
        }
    }

    private static int Temp = 0;

    private static void OnNewDataArray(SubscribedSignal subscribedSignal, ulong ntpTimeStamp, double[] values, ulong count)
    {
        if (values[0] == 7999999895928832)
            return;

        if ((Temp % 400) < 20)
        {
            Console.Write("{0}: {1:X}", subscribedSignal.SignalReference, ntpTimeStamp);
            for (ulong i = 0; i < count; ++i)
            {
                Console.Write(" {0}", values[i]);
            }
            Console.WriteLine();

        }
        Temp++;

    }
    */
    private static  void Main()
    {
        /*
        QuantumXDevice streamClient = new()
        {
            //  Установка обработчика мета информации.
            StreamMetaParser = MyStreamMetaParser
        };

        //  Установка обработчика новых данных.
        streamClient.SignalContainer.DataAsDoubleParser = OnNewDataArray;
        streamClient.Start("192.168.1.105");
        Console.ReadKey();
        await streamClient.StopAsync(default);
        Console.ReadKey();*/
    }
}
