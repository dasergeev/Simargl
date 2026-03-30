using RailTest.Satellite.Autonomic.Services;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace RailTest.Satellite.Autonomic.Registrar
{
    /// <summary>
    /// Представляет службу аудитора регистратора.
    /// </summary>
    public class AuditorService : AutonomicService
    {
        /// <summary>
        /// Поле для хранения коллекции имён процессов.
        /// </summary>
        private readonly string[] _ProcessNames;

        /// <summary>
        /// Поле для хранения массива процессов.
        /// </summary>
        private readonly Process[] _Processes;

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        public AuditorService() :
            base(ServiceID.Auditor)
        {
            _ProcessNames = new string[]
            {
                "QuantumX.exe",
                "Recorder.exe",
                "Teltonika.exe"
            };
            _Processes = new Process[_ProcessNames.Length];
        }

        /// <summary>
        /// Выполняет основную работу.
        /// </summary>
        protected override void Invoke()
        {
            while (IsWork)
            {
                for (int i = 0; i != _ProcessNames.Length; ++i)
                {
                    string name = _ProcessNames[i];
                    Process process = _Processes[i];
                    if (process is null)
                    {
                        try
                        {
                            process = Process.Start(Path.Combine(RegistrarEnviron.BinaryPath.FullName, name));
                            Logger.WriteLine($"Процесс {name} запущен.");
                            _Processes[i] = process;
                        }
                        catch
                        {

                        }
                    }
                    else
                    {
                        bool isNeedRestart;
                        try
                        {
                            isNeedRestart = !process.Responding;
                        }
                        catch
                        {
                            isNeedRestart = true;
                        }
                        if (isNeedRestart)
                        {
                            Logger.WriteLine($"Процесс {name} завершён.");
                            _Processes[i] = null;
                        }
                    }
                }
                Thread.Sleep(500);
            }
        }
    }
}
