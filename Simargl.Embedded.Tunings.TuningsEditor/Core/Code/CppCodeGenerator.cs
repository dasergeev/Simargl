using Simargl.Concurrent;
using Simargl.Embedded.Tunings.TuningsEditor.Core.Code.Writers;
using Simargl.Embedded.Tunings.TuningsEditor.Main;
using Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes;
using Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes.Params;
using Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes.Sections;
using Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes.Values;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Resources;

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
public sealed partial class CppCodeGenerator(Heart heart, AsyncAction<CodeGenerator> finalizer, CancellationToken token) :
    CodeGenerator(heart, finalizer, "\t", token)
{
    /// <summary>
    /// Асинхронно выполняет основную работу.
    /// </summary>
    protected override sealed async Task InvokeAsync()
    {
        Uri uri = new("pack://application:,,,/Sources/Cpp/Tunings.hpp", UriKind.Absolute);
        StreamResourceInfo sri = Application.GetResourceStream(uri);
        if (sri == null) throw new FileNotFoundException("Resource not found", uri.ToString());
        using var reader = new StreamReader(sri.Stream, Encoding.UTF8);
        string text = await reader.ReadToEndAsync(CancellationToken).ConfigureAwait(false);

        text = ClearGenRegex().Replace(text, string.Empty);
        text = text.Replace("__gen_namespace", Project.RootNode.GeneratorNode.Namespace.Value);
        text = text.Replace("__gen_Knot", Project.RootNode.GeneratorNode.CppMainName.Value);
        text = text.Replace("__gen_DataSize", Project.RootNode.GetDataSize().ToString());
        text = text.Replace("__gen_Data", await GetDataTypeAsync());

        await WriteLineAsync(text).ConfigureAwait(false);



        ////  Пролог.
        //await WriteLineAsync("#pragma once").ConfigureAwait(false);
        //await WriteLineAsync().ConfigureAwait(false);
        //await WriteLineAsync("#include <array>").ConfigureAwait(false);
        //await WriteLineAsync().ConfigureAwait(false);


        ////  Начало пространства имён.
        //await WriteSummary("Представляет пространство имён, которое содержит типы для работы с настраиваемыми параметрами.").ConfigureAwait(false);
        //await WriteLineAsync($"namespace {Project.RootNode.GeneratorNode.Namespace.Value}").ConfigureAwait(false);
        //await WriteLineAsync("{").ConfigureAwait(false);
        //await UpIndentAsync().ConfigureAwait(false);

        ////  Объявление основного класса.
        //await WriteMainDeclarationAsync().ConfigureAwait(false);

        ////  Завершение пространства имён.
        //await DownIndentAsync().ConfigureAwait(false);
        //await WriteLineAsync("}").ConfigureAwait(false);
    }

    [GeneratedRegex(@"//\s*__GEN_BEGIN[\s\S]*?//\s*__GEN_END", RegexOptions.Multiline)]
    private static partial Regex ClearGenRegex();

    /// <summary>
    /// Возвращает тип данных.
    /// </summary>
    /// <returns>
    /// Тип данных.
    /// </returns>
    private async Task<string> GetDataTypeAsync()
    {
        //  Создание средства записи кода.
        CppCodeWriter writer = new();

        //  Запись пролога.
        writer.WriteLine($"struct alignas(1) {Project.RootNode.GeneratorNode.CppMainName.Value}::Data");
        writer.UpIndent();
        writer.WriteLine("{");
        writer.UpIndent();

        //  Список идентификаторов разделов.
        List<string> sectionIdentifiers = [];

        //  Перебор разделов.
        foreach (Node node in Project.RootNode.SpecNode.Nodes)
        {
            //  Размер раздела.
            int sectionSize = 0;

            //  Проверка токена отмены.
            await IsNotCancelledAsync(CancellationToken).ConfigureAwait(false);

            //  Проверка раздела.
            if (node is not SectionNode sectionNode ||
                sectionNode.Format == NodeFormat.ControlSection)
            {
                //  Переход к следующему разделу.
                continue;
            }

            //  Получение идентификатора раздела.
            string sectionIdentifier = $"{sectionNode.Identifier.Value}";

            //  Проверка идентификатора.
            if (sectionIdentifiers.Contains(sectionIdentifier))
            {
                //  Завершение работы.
                throw new InvalidOperationException($"Обнаружен повторяющийся идентификатор раздела: {sectionIdentifier}");
            }

            //  Добавление идентификатора раздела.
            sectionIdentifiers.Add(sectionIdentifier);

            //  Получение типа секции.
            string sectionType = $"{sectionIdentifier}Section";

            //  Запись пролога.
            writer.WriteAccess("public");
            writer.WriteSummary($"Представляет раздел \"{sectionNode.Name.Value}\". {sectionNode.Description.Value}");
            writer.WriteLine($"struct alignas(1) {sectionType}");
            writer.WriteLine("{");
            writer.UpIndent();

            //  Список идентификаторов параметров.
            List<string> paramIdentifiers = [];

            //  Перебор узлов параметров.
            foreach (Node item in sectionNode.Nodes)
            {
                //  Проверка узла-параметра.
                if (item is not ParamNode paramNode)
                {
                    //  Переход к следующему параметру.
                    continue;
                }

                //  Получение идентификатора параметра.
                string paramIdentifier = $"{paramNode.Identifier.Value}";

                //  Проверка идентификатора.
                if (paramIdentifiers.Contains(paramIdentifier))
                {
                    //  Завершение работы.
                    throw new InvalidOperationException(
                        $"Обнаружен повторяющийся идентификатор параметра в разделе {sectionIdentifier}: {paramIdentifier}");
                }

                //  Добавление идентификатора параметра.
                paramIdentifiers.Add(paramIdentifier);

                //  Получение типа параметра.
                string paramType = $"{paramIdentifier}Param";

                //  Проверка типа параметра.
                if (paramNode is SimpleParamNode simpleParamNode)
                {
                    //  Корректировка типа параметра.
                    paramType = simpleParamNode.ValueType.Value;
                }
                else if (paramNode is EnumParamNode enumParamNode)
                {
                    //  Запись пролога.
                    writer.WriteAccess("public");
                    writer.WriteSummary($"Представляет параметр \"{enumParamNode.Name.Value}\". {enumParamNode.Description.Value}");
                    writer.WriteLine($"enum class alignas(1) {paramType} :");
                    writer.UpIndent();
                    writer.WriteLine(enumParamNode.ValueType.Value);
                    writer.DownIndent();
                    writer.WriteLine("{");
                    writer.UpIndent();

                    //  Получение типа.
                    SimpleType simpleType = SimpleType.FromString(enumParamNode.ValueType.Value);

                    //  Флаг первого значения.
                    bool isFirst = true;

                    //  Перебор значений.
                    foreach (EnumValueNode valueNode in enumParamNode.Nodes.Cast<EnumValueNode>())
                    {
                        //  Получение значения.
                        string value = valueNode.Value.Value;

                        //  Проверка значения.
                        if (!simpleType.IsValue(value))
                        {
                            //  Завершение работы.
                            throw new InvalidOperationException(
                                $"Обнаружено недопустимое значение в перечислении {paramIdentifier} в разделе {sectionIdentifier}:" +
                                $"{valueNode.Identifier.Value} = {value}");
                        }

                        //  Проверка первого значения.
                        if (isFirst)
                        {
                            //  Сборос флага.
                            isFirst = false;
                        }
                        else
                        {
                            //  Перевод каретки.
                            writer.WriteLine();
                        }

                        writer.WriteSummary($"Представляет значение \"{valueNode.Name.Value}\". {valueNode.Description.Value}");
                        writer.WriteLine($"{valueNode.Identifier.Value} = {value},");
                    }

                    //  Запись эпилога.
                    writer.DownIndent();
                    writer.WriteLine("};");
                }
                else if (paramNode is BitFieldParamNode bitFieldParamNode)
                {
                    writer.WriteAccess("public");
                    writer.WriteLine($"struct alignas(1) {paramType} {{ {bitFieldParamNode.ValueType.Value}  _Value; }};");
                }
                else if (paramNode is ArrayParamNode arrayParamNode)
                {
                    //  Корректировка типа параметра.
                    paramType = $"{arrayParamNode.ValueType.Value}<{arrayParamNode.Size.Value}>";
                }
                else
                {
                    //  Завершение работы.
                    throw new InvalidOperationException(
                        $"Обнаружен неизвестный тип параметра в разделе {sectionIdentifier}: {paramNode.GetType()}");
                }

                ////  Получение типа секции.
                //string sectionType = $"{sectionIdentifier}Section";

                //  Запись поля.
                writer.WriteAccess("public");
                writer.WriteSummary($"Поле для хранения параметра \"{paramNode.Name.Value}\". {paramNode.Description.Value}");
                writer.WriteLine($"{paramType} {paramIdentifier};");


                //  Корректировка размера раздела.
                sectionSize += paramNode.GetDataSize();
            }

            //  Запись эпилога.
            writer.DownIndent();
            writer.WriteLine("};");

            //  Проверка размера.
            if (sectionSize == 0)
            {
                //  Завершение работы.
                throw new InvalidOperationException($"Обнаружен пустой раздел: {sectionIdentifier}");
            }

            //  Запись проверки.
            writer.WriteAccess("private");
            writer.WriteLine($"static_assert(sizeof({sectionType}) == {sectionSize}, \"Размер структуры {sectionType} должен быть равен {sectionSize}.\");");

            //  Запись поля.
            writer.WriteAccess("public");
            writer.WriteSummary($"Поле для хранения раздела \"{sectionNode.Name.Value}\".");
            writer.WriteLine($"{sectionType} {sectionIdentifier};");
        }

        //  Запись эпилога.
        writer.DownIndent();
        writer.Write("};");

        //  Возврат строки.
        return writer.ToString();
    }

    //  /// <summary>
    //  /// Асинхронно выполняет запись объявления основного класса.
    //  /// </summary>
    //  /// <returns>
    //  /// Задача, выполняющая запись.
    //  /// </returns>
    //  private async Task WriteMainDeclarationAsync()
    //  {
    //      //  Начало объявления.
    //      await WriteSummary("Представляет узел настраиваемых параметров.").ConfigureAwait(false);
    //      await WriteLineAsync($"class {Project.RootNode.GeneratorNode.CppMainName.Value}").ConfigureAwait(false);
    //      await WriteLineAsync("{").ConfigureAwait(false);
    //      await UpIndentAsync().ConfigureAwait(false);

    //      await WriteAccess("public");
    //      await WriteLineAsync("enum").ConfigureAwait(false);
    //      await WriteLineAsync("{").ConfigureAwait(false);
    //      await UpIndentAsync().ConfigureAwait(false);
    //      await WriteSummary("Постоянная, определяющая размер данных.").ConfigureAwait(false);
    //      await WriteLineAsync($"DataSize = {Project.RootNode.GetDataSize()},").ConfigureAwait(false);
    //      await DownIndentAsync().ConfigureAwait(false);
    //      await WriteLineAsync("};").ConfigureAwait(false);


    //      /*

    //      	public:
    //enum
    //{
    //	/// <summary>
    //	/// Постоянная, определяющая размер данных.
    //	/// </summary>
    //	DataSize = 25312,
    //};

    //      */

    //      await WriteAccess("public");
    //      await WriteSummary("Представляет данные узла настраиваемых параметров.").ConfigureAwait(false);
    //      await WriteLineAsync("class Data;").ConfigureAwait(false);



    //      //  Завершение объявления.
    //      await DownIndentAsync().ConfigureAwait(false);
    //      await WriteLineAsync("};").ConfigureAwait(false);
    //  }

    //  /// <summary>
    //  /// Выполняет запись доступа.
    //  /// </summary>
    //  /// <param name="value">
    //  /// Значение доступа.
    //  /// </param>
    //  /// <returns>
    //  /// Задача, выполняющая запись.
    //  /// </returns>
    //  private async Task WriteAccess(string value)
    //  {
    //      await DownIndentAsync().ConfigureAwait(false);
    //      await WriteLineAsync($"{value}:").ConfigureAwait(false);
    //      await UpIndentAsync().ConfigureAwait(false);
    //  }
}
