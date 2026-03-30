using Simargl.Concurrent;
using Simargl.Embedded.Tunings.TuningsEditor.Main;

namespace Simargl.Embedded.Tunings.TuningsEditor.Core.Code;

/// <summary>
/// Представляет генератор кода C++.
/// </summary>
/// <param name="heart">
/// Сердце приложения.
/// </param>
/// <param name="finalizer">
/// Метод завершающий работу.
/// </param>
/// <param name="token">
/// Токен отмены.
/// </param>
public sealed class CSharpCodeGenerator(Heart heart, AsyncAction<CodeGenerator> finalizer, CancellationToken token) :
    CodeGenerator(heart, finalizer, "    ", token)
{
    /// <summary>
    /// Асинхронно выполняет основную работу.
    /// </summary>
    /// <returns>
    /// Задача, выполняющая основную работу.
    /// </returns>
    protected override sealed async Task InvokeAsync()
    {


        await Task.CompletedTask;

        //await Task.Delay(-1, cancellationToken);
    }

}
