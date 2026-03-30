using System.Text;


//Console.WriteLine(System.Reflection.Assembly.GetExecutingAssembly().Location);
//Console.WriteLine(Application.ExecutablePath);


const string extension = ".pdf";
StringBuilder output = new();

var directory = new FileInfo(Application.ExecutablePath).Directory!;

string outputName = $"!↬[{directory.Name}] {DateTime.Now:yyyy-MM-dd HH_mm_ss}.txt";


foreach (var file in directory.GetFiles("*.*", SearchOption.AllDirectories))
{
    string name = file.Name;
    int extLength = file.Extension.Length;
    if (file.Extension.Equals(extension, StringComparison.CurrentCultureIgnoreCase))
    {
        output.Append(name.AsSpan(0, name.Length - extLength));
        output.Append('\t');
        output.AppendLine(file.Directory!.Name);
    }
}

File.WriteAllText(
    Path.Combine(directory.FullName, outputName),
    output.ToString());