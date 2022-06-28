namespace School.Pages;

public class SelectionPage : Page
{
    readonly Dictionary<int, string> _selection;

    public SelectionPage(Dictionary<int, string> selection, string title)
    {
        _selection = selection;
        Title = title;
    }

    protected override void UpdateOptions()
    {
        Options.Clear();

        foreach (var item in _selection)
            Options.Add(item.Value,
                        () =>
                        {
                            ReturnValue = item.Key;
                            Exit = true;
                        });
    }
}