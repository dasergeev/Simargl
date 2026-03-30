using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Microsoft.Win32;

namespace Simargl.Viewer;

partial class MainWindow
{
    private const int PreviewValueCount = 100;
    private readonly ObservableCollection<ChannelRow> _channels = new();
    private readonly ObservableCollection<ValueRow> _values = new();
    private TextBox? _headerBox;
    private TextBlock? _channelBox;
    private TextBlock? _statusBox;
    private DataGrid? _channelsGrid;

    protected override void OnInitialized(EventArgs e)
    {
        base.OnInitialized(e);
        BuildUi();
        SetStatus("Файл не открыт.");
    }

    private void BuildUi()
    {
        Title = "Simargl.Viewer — просмотр файла регистрации";
        WindowState = WindowState.Maximized;
        MinWidth = 1000d;
        MinHeight = 700d;

        var root = new Grid();
        root.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        root.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        root.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1d, GridUnitType.Star) });
        root.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

        var toolbar = new DockPanel { Margin = new Thickness(12d, 12d, 12d, 8d), LastChildFill = true };
        var openButton = new Button { Content = "Открыть файл", Padding = new Thickness(16d, 8d, 16d, 8d), Margin = new Thickness(0d, 0d, 12d, 0d), MinWidth = 140d };
        openButton.Click += OpenButton_Click;
        DockPanel.SetDock(openButton, Dock.Left);
        toolbar.Children.Add(openButton);
        toolbar.Children.Add(new TextBlock { VerticalAlignment = VerticalAlignment.Center, TextWrapping = TextWrapping.Wrap, Text = "Открывает файл формата TESTLAB и показывает заголовок, список каналов и первые фактические значения выбранного канала." });
        Grid.SetRow(toolbar, 0);
        root.Children.Add(toolbar);

        _headerBox = new TextBox { Margin = new Thickness(12d, 0d, 12d, 8d), IsReadOnly = true, TextWrapping = TextWrapping.Wrap, VerticalScrollBarVisibility = ScrollBarVisibility.Auto, HorizontalScrollBarVisibility = ScrollBarVisibility.Auto, MinHeight = 140d, FontFamily = new System.Windows.Media.FontFamily("Consolas") };
        Grid.SetRow(_headerBox, 1);
        root.Children.Add(_headerBox);

        var content = new Grid { Margin = new Thickness(12d, 0d, 12d, 8d) };
        content.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2d, GridUnitType.Star) });
        content.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(12d) });
        content.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3d, GridUnitType.Star) });
        Grid.SetRow(content, 2);
        root.Children.Add(content);

        _channelsGrid = CreateChannelsGrid();
        var left = new GroupBox { Header = "Каналы", Padding = new Thickness(8d), Content = _channelsGrid };
        Grid.SetColumn(left, 0);
        content.Children.Add(left);

        var splitter = new GridSplitter { Width = 8d, HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Stretch, ResizeBehavior = GridResizeBehavior.PreviousAndNext, Background = System.Windows.Media.Brushes.Transparent };
        Grid.SetColumn(splitter, 1);
        content.Children.Add(splitter);

        var right = new Grid();
        right.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        right.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1d, GridUnitType.Star) });
        _channelBox = new TextBlock { TextWrapping = TextWrapping.Wrap, FontFamily = new System.Windows.Media.FontFamily("Consolas") };
        var info = new GroupBox { Header = "Выбранный канал", Padding = new Thickness(8d), Margin = new Thickness(0d, 0d, 0d, 8d), Content = _channelBox };
        Grid.SetRow(info, 0);
        right.Children.Add(info);
        var preview = new GroupBox { Header = $"Первые {PreviewValueCount} фактических значений", Padding = new Thickness(8d), Content = CreateValuesGrid() };
        Grid.SetRow(preview, 1);
        right.Children.Add(preview);
        Grid.SetColumn(right, 2);
        content.Children.Add(right);

        _statusBox = new TextBlock { TextWrapping = TextWrapping.Wrap };
        var statusBorder = new Border { Margin = new Thickness(12d, 0d, 12d, 12d), Padding = new Thickness(8d), BorderThickness = new Thickness(1d), BorderBrush = SystemColors.ActiveBorderBrush, CornerRadius = new CornerRadius(4d), Child = _statusBox };
        Grid.SetRow(statusBorder, 3);
        root.Children.Add(statusBorder);

        Content = root;
        RefreshHeader(null);
        RefreshChannel(null);
    }

    private DataGrid CreateChannelsGrid()
    {
        var grid = new DataGrid { AutoGenerateColumns = false, IsReadOnly = true, SelectionMode = DataGridSelectionMode.Single, SelectionUnit = DataGridSelectionUnit.FullRow, CanUserAddRows = false, CanUserDeleteRows = false, HeadersVisibility = DataGridHeadersVisibility.Column, ItemsSource = _channels };
        grid.SelectionChanged += ChannelsGrid_SelectionChanged;
        grid.Columns.Add(Column("Название", nameof(ChannelRow.Name), 140d));
        grid.Columns.Add(Column("Описание", nameof(ChannelRow.Description), 260d));
        grid.Columns.Add(Column("Ед.", nameof(ChannelRow.Unit), 80d));
        grid.Columns.Add(Column("Дискретизация", nameof(ChannelRow.SampleRate), 110d));
        grid.Columns.Add(Column("Формат", nameof(ChannelRow.Format), 120d));
        grid.Columns.Add(Column("Длина", nameof(ChannelRow.Length), 100d));
        return grid;
    }

    private DataGrid CreateValuesGrid()
    {
        var grid = new DataGrid { AutoGenerateColumns = false, IsReadOnly = true, SelectionMode = DataGridSelectionMode.Single, SelectionUnit = DataGridSelectionUnit.FullRow, CanUserAddRows = false, CanUserDeleteRows = false, HeadersVisibility = DataGridHeadersVisibility.Column, ItemsSource = _values };
        grid.Columns.Add(Column("Индекс", nameof(ValueRow.Index), 100d));
        grid.Columns.Add(Column("Исходное значение", nameof(ValueRow.RawValue), 180d));
        grid.Columns.Add(Column("Фактическое значение", nameof(ValueRow.ActualValue), 220d));
        return grid;
    }

    private static DataGridTextColumn Column(string header, string property, double minWidth)
    {
        return new DataGridTextColumn { Header = header, Binding = new Binding(property), MinWidth = minWidth, Width = new DataGridLength(1d, DataGridLengthUnitType.Auto) };
    }

    private void OpenButton_Click(object? sender, RoutedEventArgs e)
    {
        var dialog = new OpenFileDialog { Title = "Открытие файла регистрации", Filter = "Все файлы|*.*", CheckFileExists = true, Multiselect = false };
        if (dialog.ShowDialog(this) == true)
        {
            LoadDocument(dialog.FileName);
        }
    }

    private void LoadDocument(string path)
    {
        try
        {
            var document = new Reader().Read(path);
            _channels.Clear();
            foreach (var channel in document.Channels)
            {
                _channels.Add(new ChannelRow(channel));
            }

            RefreshHeader(document);
            if (_channelsGrid is not null && _channels.Count > 0)
            {
                _channelsGrid.SelectedIndex = 0;
                _channelsGrid.ScrollIntoView(_channelsGrid.SelectedItem);
            }
            else
            {
                RefreshChannel(null);
            }

            SetStatus($"Открыт файл: {path}");
        }
        catch (FormatExceptionEx ex)
        {
            _channels.Clear();
            RefreshHeader(null);
            RefreshChannel(null);
            SetStatus($"Ошибка формата файла: {ex.Message}");
            MessageBox.Show(this, ex.Message, "Ошибка формата файла", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (Exception ex)
        {
            _channels.Clear();
            RefreshHeader(null);
            RefreshChannel(null);
            SetStatus($"Ошибка открытия файла: {ex.Message}");
            MessageBox.Show(this, ex.Message, "Ошибка открытия файла", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void ChannelsGrid_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        RefreshChannel((_channelsGrid?.SelectedItem as ChannelRow)?.Channel);
    }

    private void RefreshHeader(Document? document)
    {
        if (_headerBox is null)
        {
            return;
        }

        if (document is null)
        {
            _headerBox.Text = "Файл не открыт.";
            return;
        }

        var b = new StringBuilder();
        b.AppendLine($"Путь к файлу          : {document.FilePath}");
        b.AppendLine($"Сигнатура             : {document.Header.Signature}");
        b.AppendLine($"Объект испытаний      : {TextOrDash(document.Header.ObjectName)}");
        b.AppendLine($"Тип испытаний         : {TextOrDash(document.Header.TestType)}");
        b.AppendLine($"Место испытаний       : {TextOrDash(document.Header.Place)}");
        b.AppendLine($"Дата                  : {TextOrDash(document.Header.Date)}");
        b.AppendLine($"Время                 : {TextOrDash(document.Header.Time)}");
        b.AppendLine($"Количество каналов    : {document.Header.ChannelCount}");
        b.AppendLine($"Прочитано каналов     : {document.Channels.Count}");
        _headerBox.Text = b.ToString();
    }

    private void RefreshChannel(Channel? channel)
    {
        _values.Clear();
        if (_channelBox is null)
        {
            return;
        }

        if (channel is null)
        {
            _channelBox.Text = "Канал не выбран.";
            return;
        }

        var b = new StringBuilder();
        b.AppendLine($"Название              : {TextOrDash(channel.Description.Name)}");
        b.AppendLine($"Описание              : {TextOrDash(channel.Description.Description)}");
        b.AppendLine($"Единица измерения     : {TextOrDash(channel.Description.Unit)}");
        b.AppendLine($"Формат данных         : {channel.Description.Format}");
        b.AppendLine($"Длина массива         : {channel.Description.Length}");
        b.AppendLine($"Частота дискретизации : {channel.Description.SampleRate}");
        b.AppendLine($"Частота среза         : {channel.Description.Cutoff.ToString(CultureInfo.CurrentCulture)}");
        b.AppendLine($"Смещение              : {channel.Description.Offset.ToString(CultureInfo.CurrentCulture)}");
        b.AppendLine($"Масштаб               : {channel.Description.Scale.ToString(CultureInfo.CurrentCulture)}");
        b.AppendLine($"Прочитано значений    : {channel.RawValues.Length}");
        _channelBox.Text = b.ToString();

        var count = Math.Min(channel.RawValues.Length, PreviewValueCount);
        for (var i = 0; i < count; i++)
        {
            _values.Add(new ValueRow(i, channel.RawValues[i], channel.ActualValues[i]));
        }
    }

    private void SetStatus(string message)
    {
        if (_statusBox is not null)
        {
            _statusBox.Text = message;
        }
    }

    private static string TextOrDash(string value) => string.IsNullOrWhiteSpace(value) ? "—" : value;

    private sealed record Document(string FilePath, Header Header, IReadOnlyList<Channel> Channels);
    private sealed record Header(string Signature, string ObjectName, string TestType, string Place, string Date, string Time, ushort ChannelCount);
    private sealed record ChannelDescription(string Name, string Description, string Unit, float Offset, float Scale, float Cutoff, ushort SampleRate, byte ChannelType, DataFormat Format, uint Length);
    private sealed record Channel(ChannelDescription Description, double[] RawValues, double[] ActualValues);

    private enum DataFormat : byte
    {
        UInt8 = 1,
        UInt16 = 2,
        UInt32 = 3,
        Float32 = 4,
        Float64 = 8,
        Int8 = 17,
        Int16 = 18,
        Int32 = 19,
        Float32Alt = 20,
        Float64Alt = 24,
    }

    private sealed class FormatExceptionEx : Exception
    {
        public FormatExceptionEx(string message) : base(message) { }
        public FormatExceptionEx(string message, Exception inner) : base(message, inner) { }
    }

    private sealed class Reader
    {
        private static readonly Encoding TextEncoding = CreateEncoding();

        public Document Read(string filePath)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(filePath);
            try
            {
                using var stream = File.OpenRead(filePath);
                using var reader = new BinaryReader(stream, Encoding.UTF8, leaveOpen: false);
                var header = ReadHeader(reader);
                var descriptions = ReadDescriptions(reader, header.ChannelCount);
                var channels = ReadChannels(reader, descriptions);
                return new Document(filePath, header, channels);
            }
            catch (FormatExceptionEx)
            {
                throw;
            }
            catch (EndOfStreamException ex)
            {
                throw new FormatExceptionEx("Файл завершился раньше, чем ожидалось по спецификации формата.", ex);
            }
            catch (IOException ex)
            {
                throw new FormatExceptionEx("Не удалось прочитать файл регистрации.", ex);
            }
        }

        private static Encoding CreateEncoding()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            return Encoding.GetEncoding(1251);
        }

        private static Header ReadHeader(BinaryReader r)
        {
            var signature = ReadString(r, 8);
            var objectName = ReadString(r, 61);
            var testType = ReadString(r, 121);
            var place = ReadString(r, 121);
            var date = ReadString(r, 11);
            var time = ReadString(r, 9);
            var channelCount = r.ReadUInt16();
            if (r.ReadBytes(17).Length != 17)
            {
                throw new FormatExceptionEx("Не удалось полностью прочитать зарезервированную часть заголовка файла.");
            }

            if (!string.Equals(signature, "TESTLAB", StringComparison.Ordinal))
            {
                throw new FormatExceptionEx($"Ожидалась сигнатура TESTLAB, но получено значение '{signature}'.");
            }

            return new Header(signature, objectName, testType, place, date, time, channelCount);
        }

        private static List<ChannelDescription> ReadDescriptions(BinaryReader r, ushort count)
        {
            var list = new List<ChannelDescription>(count);
            for (var i = 0; i < count; i++)
            {
                var name = ReadString(r, 13);
                var description = ReadString(r, 121);
                var unit = ReadString(r, 13);
                var offset = r.ReadSingle();
                var scale = r.ReadSingle();
                var cutoff = r.ReadSingle();
                var sampleRate = r.ReadUInt16();
                var channelType = r.ReadByte();
                var formatCode = r.ReadByte();
                var length = r.ReadUInt32();
                if (r.ReadBytes(25).Length != 25)
                {
                    throw new FormatExceptionEx($"Не удалось полностью прочитать зарезервированную часть описания канала с индексом {i}.");
                }

                if (channelType != 0)
                {
                    throw new FormatExceptionEx($"Канал с индексом {i} содержит неподдерживаемый тип {channelType}. Ожидалось значение 0.");
                }

                list.Add(new ChannelDescription(name, description, unit, offset, scale, cutoff, sampleRate, channelType, ParseFormat(formatCode, i), length));
            }

            return list;
        }

        private static List<Channel> ReadChannels(BinaryReader r, IReadOnlyList<ChannelDescription> descriptions)
        {
            var channels = new List<Channel>(descriptions.Count);
            for (var i = 0; i < descriptions.Count; i++)
            {
                var d = descriptions[i];
                var raw = ReadRawValues(r, d, i);
                var actual = new double[raw.Length];
                for (var j = 0; j < raw.Length; j++)
                {
                    actual[j] = d.Scale * (raw[j] - d.Offset);
                }

                var endMarker = r.ReadUInt16();
                if (endMarker != ushort.MaxValue)
                {
                    throw new FormatExceptionEx($"Канал с индексом {i} завершён неверным признаком конца массива {endMarker}. Ожидалось значение 65535.");
                }

                channels.Add(new Channel(d, raw, actual));
            }

            return channels;
        }

        private static double[] ReadRawValues(BinaryReader r, ChannelDescription d, int channelIndex)
        {
            if (d.Length > int.MaxValue)
            {
                throw new FormatExceptionEx($"Канал с индексом {channelIndex} содержит слишком большой массив данных длиной {d.Length}.");
            }

            var length = checked((int)d.Length);
            var values = new double[length];
            for (var i = 0; i < length; i++)
            {
                values[i] = d.Format switch
                {
                    DataFormat.UInt8 => r.ReadByte(),
                    DataFormat.UInt16 => r.ReadUInt16(),
                    DataFormat.UInt32 => r.ReadUInt32(),
                    DataFormat.Int8 => r.ReadSByte(),
                    DataFormat.Int16 => r.ReadInt16(),
                    DataFormat.Int32 => r.ReadInt32(),
                    DataFormat.Float32 or DataFormat.Float32Alt => r.ReadSingle(),
                    DataFormat.Float64 or DataFormat.Float64Alt => r.ReadDouble(),
                    _ => throw new FormatExceptionEx($"Формат данных {d.Format} не поддерживается для чтения."),
                };
            }

            return values;
        }

        private static DataFormat ParseFormat(byte code, int channelIndex)
        {
            return code switch
            {
                1 => DataFormat.UInt8,
                2 => DataFormat.UInt16,
                3 => DataFormat.UInt32,
                4 => DataFormat.Float32,
                8 => DataFormat.Float64,
                17 => DataFormat.Int8,
                18 => DataFormat.Int16,
                19 => DataFormat.Int32,
                20 => DataFormat.Float32Alt,
                24 => DataFormat.Float64Alt,
                _ => throw new FormatExceptionEx($"Канал с индексом {channelIndex} содержит неподдерживаемый код формата данных {code}."),
            };
        }

        private static string ReadString(BinaryReader r, int length)
        {
            var bytes = r.ReadBytes(length);
            if (bytes.Length != length)
            {
                throw new FormatExceptionEx($"Не удалось полностью прочитать строковое поле длиной {length} байт.");
            }

            var zeroIndex = Array.IndexOf(bytes, (byte)0);
            var actualLength = zeroIndex >= 0 ? zeroIndex : bytes.Length;
            return TextEncoding.GetString(bytes, 0, actualLength).Trim();
        }
    }

    private sealed class ChannelRow
    {
        public ChannelRow(Channel channel)
        {
            Channel = channel;
            Name = TextOrDash(channel.Description.Name);
            Description = TextOrDash(channel.Description.Description);
            Unit = TextOrDash(channel.Description.Unit);
            SampleRate = channel.Description.SampleRate.ToString(CultureInfo.CurrentCulture);
            Format = channel.Description.Format.ToString();
            Length = channel.Description.Length.ToString(CultureInfo.CurrentCulture);
        }

        public Channel Channel { get; }
        public string Name { get; }
        public string Description { get; }
        public string Unit { get; }
        public string SampleRate { get; }
        public string Format { get; }
        public string Length { get; }
    }

    private sealed class ValueRow
    {
        public ValueRow(int index, double rawValue, double actualValue)
        {
            Index = index;
            RawValue = rawValue.ToString("G17", CultureInfo.CurrentCulture);
            ActualValue = actualValue.ToString("G17", CultureInfo.CurrentCulture);
        }

        public int Index { get; }
        public string RawValue { get; }
        public string ActualValue { get; }
    }
}
