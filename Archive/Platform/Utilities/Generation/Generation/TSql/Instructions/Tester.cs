using System.Text;

namespace Apeiron.Platform.Utilities.Generation.TSql.Instructions;

internal class Node
{

}

internal class SimpleNode :
    Node
{
    public SimpleNode(string text)
    {
        _ = text;
    }
}


internal static class Tester
{
    public static readonly List<Node> Nodes = new()
    {
        new SimpleNode("CREATE DATABASE database_name"),
        new SimpleNode("[ CONTAINMENT = { NONE | PARTIAL } ]"),
        new SimpleNode("[ ON"),
        new SimpleNode("      [ PRIMARY ] <filespec> [ ,...n ]"),
        new SimpleNode("      [ , <filegroup> [ ,...n ] ]"),
        new SimpleNode("      [ LOG ON <filespec> [ ,...n ] ]"),
        new SimpleNode("]"),
        new SimpleNode("[ COLLATE collation_name ]"),
        new SimpleNode("[ WITH <option> [,...n ] ]"),
        new SimpleNode("[;]"),
    };
    private const string _Syntax = @"
<option> ::=
{
      FILESTREAM ( <filestream_option> [,...n ] )
    | DEFAULT_FULLTEXT_LANGUAGE = { lcid | language_name | language_alias }
    | DEFAULT_LANGUAGE = { lcid | language_name | language_alias }
    | NESTED_TRIGGERS = { OFF | ON }
    | TRANSFORM_NOISE_WORDS = { OFF | ON}
    | TWO_DIGIT_YEAR_CUTOFF = <two_digit_year_cutoff>
    | DB_CHAINING { OFF | ON }
    | TRUSTWORTHY { OFF | ON }
    | PERSISTENT_LOG_BUFFER=ON ( DIRECTORY_NAME='<Filepath to folder on DAX formatted volume>' )
}

<filestream_option> ::=
{
      NON_TRANSACTED_ACCESS = { OFF | READ_ONLY | FULL }
    | DIRECTORY_NAME = 'directory_name'
}

<filespec> ::=
{
(
    NAME = logical_file_name ,
    FILENAME = { 'os_file_name' | 'filestream_path' }
    [ , SIZE = size [ KB | MB | GB | TB ] ]
    [ , MAXSIZE = { max_size [ KB | MB | GB | TB ] | UNLIMITED } ]
    [ , FILEGROWTH = growth_increment [ KB | MB | GB | TB | % ] ]
)
}

<filegroup> ::=
{
FILEGROUP filegroup name [ [ CONTAINS FILESTREAM ] [ DEFAULT ] | CONTAINS MEMORY_OPTIMIZED_DATA ]
    <filespec> [ ,...n ]
}
";

    public static void Invoke()
    {
        StringBuilder syntax = new(_Syntax);
        syntax.Replace("\n", string.Empty);
        syntax.Replace("\r", string.Empty);


        Console.WriteLine(syntax);
    }
}

