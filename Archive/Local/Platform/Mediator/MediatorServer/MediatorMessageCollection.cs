using Apeiron.Platform.MediatorLibrary.Messages;
using System.Collections.Concurrent;

namespace Apeiron.Platform.MediatorServer;

/// <summary>
/// Представляет коллекцию сообщений.
/// </summary>
public class MediatorMessageCollection : ConcurrentDictionary<string, GeneralMessage>
{

}
