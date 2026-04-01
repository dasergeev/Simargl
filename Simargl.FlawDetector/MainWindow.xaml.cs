using System.Collections.Specialized; // Handles log collection change notifications.
using System.Windows.Threading; // Handles deferred UI scrolling.
using Simargl.FlawDetector.ViewModels; // Provides the main window view model.

namespace Simargl.FlawDetector; // Defines the main window namespace.

/// <summary> // Documents the main window.
/// Represents the main simulator window. // Clarifies the role of the window.
/// </summary> // Ends XML documentation.
partial class MainWindow // Declares the main window type.
{ // Begins the class body.
    /// <summary> // Documents the constructor.
    /// Initializes the main window instance. // Clarifies the constructor purpose.
    /// </summary> // Ends XML documentation.
    public MainWindow() // Declares the main window constructor.
    { // Begins the constructor body.
        InitializeComponent(); // Initializes the visual tree.
        MainWindowViewModel viewModel = new MainWindowViewModel(); // Creates the window view model.
        viewModel.LogEntries.CollectionChanged += OnLogEntriesCollectionChanged; // Subscribes to log updates.
        DataContext = viewModel; // Assigns the data context.
    } // Ends the constructor body.

    /// <summary> // Documents the log collection change handler.
    /// Scrolls the log list to the latest record after the UI updates. // Clarifies the handler purpose.
    /// </summary> // Ends XML documentation.
    /// <param name="sender">The changed log collection.</param> // Documents the sender.
    /// <param name="eventArgs">The collection change arguments.</param> // Documents the event arguments.
    private void OnLogEntriesCollectionChanged(object? sender, NotifyCollectionChangedEventArgs eventArgs) // Handles log updates.
    { // Begins the handler body.
        Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => // Schedules scrolling after layout refresh.
        { // Begins the deferred action.
            if (LogListBox.Items.Count == 0) // Checks whether the log contains records.
            { // Begins the empty-log branch.
                return; // Stops when there is nothing to scroll to.
            } // Ends the empty-log branch.

            object? lastItem = LogListBox.Items[LogListBox.Items.Count - 1]; // Gets the latest log record.
            LogListBox.ScrollIntoView(lastItem); // Scrolls to the latest log record.
        })); // Ends the deferred action.
    } // Ends the handler body.
} // Ends the class body.
