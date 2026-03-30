using Apeiron.Support;
using System.Runtime.InteropServices;
using System.ServiceProcess;

namespace Apeiron.Platform.Server.Services.Orchestrator.OrchestratorHostAgent;

/// <summary>
/// Обработчик служб на хосте.
/// </summary>
public static class ServiceOperator
{
    /// <summary>
    /// Перечисление содержит варианты действия со службой.
    /// </summary>
    public enum ServiceAction : byte
    {
        /// <summary>
        /// Действие запуска службы.
        /// </summary>
        Start = 1,

        /// <summary>
        /// Дествие остановки службы.
        /// </summary>
        Stop,

        /// <summary>
        /// Действие перезапуска службы.
        /// </summary>
        Restart,

        /// <summary>
        /// Действие возврата статуса службы.
        /// </summary>
        Status
    }

    /// <summary>
    /// Возвращает 
    /// </summary>
    public static bool ServiceRunningStatus(List<string> serviceNames)
    {
        // Проверка входящих параметров.
        Check.IsNotNull(serviceNames, nameof(serviceNames));

        try
        {
            if (ActionOperator(serviceNames, ServiceAction.Status))
                return true;
            else
                return false;
        }
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// Остановка службы.
    /// </summary>
    /// <param name="serviceNames">Список служб.</param>
    public static bool StopService(List<string> serviceNames)
    {
        // Проверка входящих параметров.
        Check.IsNotNull(serviceNames, nameof(serviceNames));

        try
        {
            if (ActionOperator(serviceNames, ServiceAction.Stop))
                return true;
            else
                return false;
        }
        catch (Exception)
        {
            throw;
        }

    }

    /// <summary>
    /// Остановка службы.
    /// </summary>
    /// <param name="serviceNames">Список служб.</param>
    public static bool StartService(List<string> serviceNames)
    {
        // Проверка входящих параметров.
        Check.IsNotNull(serviceNames, nameof(serviceNames));

        try
        {
            if (ActionOperator(serviceNames, ServiceAction.Start))
                return true;
            else
                return false;
        }
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// Перезапускает службы.
    /// </summary>
    /// <param name="serviceNames">Список служб.</param>
    /// <returns></returns>
    public static bool RestartService(List<string> serviceNames)
    {
        try
        {
            // Проверка входящих параметров.
            Check.IsNotNull(serviceNames, nameof(serviceNames));

            if (ActionOperator(serviceNames, ServiceAction.Restart))
                return true;
            else
                return false;
        }
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// Останавливает переданные службы.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Проверка совместимости платформы", Justification = "<Ожидание>")]
    public static bool ActionOperator(List<string> serviceNames, ServiceAction serviceAction)
    {
        // Проверка входящих параметров.
        Check.IsNotNull(serviceNames, nameof(serviceNames));

        //  Проверка платформы.
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            if (serviceNames.Count > 0)
            {
                int boolFlag = 0;
                // Получает список сервисов зарегистрированных в системе.
                ServiceController[] servicesOnHost = ServiceController.GetServices();

                foreach (var itemService in serviceNames)
                {
                    // Если в системе зарегистрирована служба с именем из списка.
                    if (servicesOnHost.Any(x => x.ServiceName == itemService))
                    {
                        try
                        {
                            using var serviceController = new ServiceController(itemService);

                            switch (serviceAction)
                            {
                                case ServiceAction.Start:
                                    if (serviceController.Status != ServiceControllerStatus.Running)
                                    {
                                        serviceController.Start();
                                        serviceController.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(10));
                                        boolFlag = 1;
                                    }                                    
                                    break;
                                case ServiceAction.Stop:
                                    if (serviceController.Status != ServiceControllerStatus.Stopped)
                                    {
                                        serviceController.Stop(true);
                                        serviceController.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(10));
                                        boolFlag = 1;
                                    }
                                    break;
                                case ServiceAction.Restart:
                                    if (serviceController.Status == ServiceControllerStatus.Running)
                                    {

                                        serviceController.Stop(true);
                                        serviceController.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(10));

                                        if (serviceController.Status == ServiceControllerStatus.Stopped)
                                        {
                                            serviceController.Start();
                                            serviceController.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(10));
                                            boolFlag = 1;
                                        }
                                    }                                    
                                    break;
                                case ServiceAction.Status:
                                    if (serviceController.Status == ServiceControllerStatus.Running)
                                    {
                                        boolFlag = 1;
                                    }
                                        break;
                                default:
                                    break;
                            }
                        }
                        catch (Exception ex)
                        {
                            if (ex is System.ServiceProcess.TimeoutException)
                            {
                                throw new System.ServiceProcess.TimeoutException("Превышено время ожидания выполнения действия со службой.");
                            }

                            throw;
                        }                       
                    }
                }
                
                return boolFlag > 0;
            }
            else
            {
                return false;
            }
        }
        else
        {
            throw new PlatformNotSupportedException("Данная ОС не поддерживается для управления службами.");
        }
    }    

}

