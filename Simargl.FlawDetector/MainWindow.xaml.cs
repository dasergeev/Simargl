using System.ComponentModel; // Handles main view model property change notifications.
using System.Windows; // Provides the WPF window base type.
using System.Windows.Threading; // Handles deferred UI scrolling.
using Simargl.FlawDetector.ViewModels; // Provides the main window view model.

namespace Simargl.FlawDetector; // Defines the main window namespace.

/// <summary> // Documents the main window.
/// Represents the main simulator window. // Clarifies the role of the window.
/// </summary> // Ends XML documentation.
public partial class MainWindow : Window // Declares the main window type.
{ // Begins the class body.
    private readonly MainWindowViewModel viewModel; // Stores the main window view model instance.

    /// <summary> // Documents the constructor.
    /// Initializes the main window instance. // Clarifies the constructor purpose.
    /// </summary> // Ends XML documentation.
    public MainWindow() // Declares the main window constructor.
    { // Begins the constructor body.
        InitializeComponent(); // Initializes the visual tree.
        this.viewModel = new MainWindowViewModel(); // Creates the window view model.
        this.viewModel.PropertyChanged += OnViewModelPropertyChanged; // Subscribes to log text updates.
        DataContext = this.viewModel; // Assigns the data context.
    } // Ends the constructor body.

    /// <summary> // Documents the window close handler.
    /// Releases subscriptions created by the main window. // Clarifies the method purpose.
    /// </summary> // Ends XML documentation.
    /// <param name="e">The close event data.</param> // Documents the event data.
    protected override void OnClosed(EventArgs e) // Handles window closing.
    { // Begins the method body.
        this.viewModel.PropertyChanged -= OnViewModelPropertyChanged; // Releases the view model subscription.
        base.OnClosed(e); // Completes the standard close sequence.
    } // Ends the method body.

    /// <summary> // Documents the view model change handler.
    /// Scrolls the text log to the latest record after log text updates. // Clarifies the method purpose.
    /// </summary> // Ends XML documentation.
    /// <param name="sender">The changed view model.</param> // Documents the sender.
    /// <param name="eventArgs">The property change arguments.</param> // Documents the event arguments.
    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs eventArgs) // Handles view model changes.
    { // Begins the handler body.
        if (eventArgs.PropertyName != nameof(MainWindowViewModel.LogText)) // Checks whether the visible log text changed.
        { // Begins the irrelevant-property branch.
            return; // Stops when another property changed.
        } // Ends the irrelevant-property branch.

        Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => // Schedules scrolling after layout refresh.
        { // Begins the deferred action.
            LogTextBox.ScrollToEnd(); // Scrolls the text log to the latest visible line.
        })); // Ends the deferred action.
    } // Ends the handler body.
} // Ends the class body.
