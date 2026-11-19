using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace SinglePage;

public sealed class MainPageViewModel : INotifyPropertyChanged
{
    private int count;

    public MainPageViewModel()
    {
        IncrementCountCommand = new Command((IncrementCount) =>
        {
            count++;
            OnPropertyChanged(nameof(CounterText));
        });
        BrowseCommand = new Command(async () =>
        {
            FileResult? result = await FilePicker.PickAsync(new PickOptions());
            await Shell.Current.DisplayAlertAsync("Alert", "Returned from PickAsync", "OK");
        });
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public string CounterText => count switch
    {
        0 => "Click me",
        1 => $"Clicked {count} time",
        _ => $"Clicked {count} times"
    };

    public ICommand IncrementCountCommand { get; }

    public ICommand BrowseCommand { get; }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
