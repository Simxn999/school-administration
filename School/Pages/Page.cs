using School.Model;
using static System.Console;

namespace School.Pages;

public class Page
{
    public const int Indent = 2;
    public bool Exit = false;

    public Page()
    {
        Initialize();
    }

    public int ReturnValue { get; set; }
    protected string Title { get; set; } = "School";
    protected Dictionary<string, Action> Options { get; set; } = new();
    protected static Dictionary<string, (int, int)> Positions { get; set; } = new();
    protected bool HasPreviousPage { get; set; }
    protected bool HasNextPage { get; set; }
    protected Action Content { get; set; } = () => { };
    int OptionsCount { get; set; }
    int TotalCount => Options.Count;
    int PageCount { get; set; }
    int CurrentPage { get; set; }
    int StartIndex { get; set; }

    void Initialize()
    {
        PageCount = 1;
        CurrentPage = 1;
        StartIndex = 0;
        HasPreviousPage = false;
        HasNextPage = false;
        OptionsCount = 9;

        Validate();
    }

    void Validate()
    {
        if (TotalCount > 9)
            PageCount = TotalCount / 9 + 1;

        if (CurrentPage < 1)
            CurrentPage = 1;

        if (CurrentPage > PageCount)
            CurrentPage = PageCount;

        OptionsCount = TotalCount - (CurrentPage - 1) * 9;
        if (OptionsCount > 9)
            OptionsCount = 9;

        StartIndex = (CurrentPage - 1) * 9;
        HasPreviousPage = CurrentPage > 1;
        HasNextPage = CurrentPage < PageCount;
    }

    public void Run()
    {
        GetInput();
    }

    protected virtual void UpdateOptions() { }

    void GetInput()
    {
        while (true)
        {
            UpdateOptions();
            Validate();
            var keys = Options.Keys.Where(k => !k.Contains("#enter#") || !k.Contains("#delete#")).ToList();
            var currentOptions = Options.Where(option =>
                                        {
                                            var index = keys.IndexOf(option.Key);

                                            if (option.Key.Contains("#enter#") || option.Key.Contains("#delete#"))
                                                return false;

                                            return index >= StartIndex && index <= StartIndex + OptionsCount - 1;
                                        })
                                        .ToList()
                                        .ConvertAll(option => option.Value);

            Print();
            if (Exit)
                break;

            var key = ReadKey(true).Key;

            if (key is >= ConsoleKey.D1 and <= ConsoleKey.D9)
            {
                if (!InvokeNumberedOption(currentOptions, key))
                    continue;

                break;
            }

            switch (key)
            {
                case ConsoleKey.LeftArrow:
                    if (HasPreviousPage)
                        continue;

                    SetPage(CurrentPage - 1);
                    break;

                case ConsoleKey.RightArrow:
                    if (!HasNextPage)
                        continue;

                    SetPage(CurrentPage + 1);
                    break;

                case ConsoleKey.Q:
                    Environment.Exit(0);
                    break;

                case ConsoleKey.Escape:
                    if (GetType().Name.Equals("HomePage"))
                        continue;

                    if (HasPreviousPage)
                        goto case ConsoleKey.LeftArrow;

                    break;

                case ConsoleKey.Enter:
                    var enter = Options.Keys.ToList().FindIndex(k => k.Contains("#enter#"));
                    if (enter == -1) continue;

                    Options.Values.ElementAt(enter).Invoke();

                    if (!Exit)
                        Run();
                    break;

                case ConsoleKey.Delete:
                    var delete = Options.Keys.ToList().FindIndex(k => k.Contains("#delete#"));
                    if (delete == -1) continue;

                    Options.Values.ElementAt(delete).Invoke();

                    if (!Exit)
                        Run();
                    break;

                default:
                    continue;
            }

            break;
        }
    }

    bool InvokeNumberedOption(IEnumerable<Action> options, ConsoleKey key)
    {
        if (OptionsCount <= key - ConsoleKey.D1)
            return false;

        options.ElementAt(key - ConsoleKey.D1).Invoke();

        if (!Exit)
            Run();

        return true;
    }

    void SetPage(int page)
    {
        CurrentPage = page;
        Validate();
        Run();
    }

    public void Print()
    {
        Clear();

        WriteLine(Title);

        CursorTop = 0;

        if (PageCount > 1)
            PrintText($"Page: {CurrentPage} / {PageCount}");

        for (var i = StartIndex;
             i < StartIndex + OptionsCount && !Options.ElementAt(i).Key.Contains("#enter#") && !Options.ElementAt(i).Key.Contains("#delete#");
             i++)
            PrintOption(Options.Keys.ElementAt(i), $"{i - StartIndex + 1}");

        var enterIndex = Options.Keys.ToList().FindIndex(0, k => k.Contains("#enter#"));
        if (enterIndex != -1)
            PrintOption(Options.Keys.ElementAt(enterIndex)[7..], "ENTER");

        var deleteIndex = Options.Keys.ToList().FindIndex(0, k => k.Contains("#delete#"));
        if (deleteIndex != -1)
            PrintOption(Options.Keys.ElementAt(deleteIndex)[8..], "DEL");

        if (HasPreviousPage)
            PrintOption("Previous Page", "<-");

        if (HasNextPage)
            PrintOption("Next Page", "->");

        PrintOption("Go Back", "ESC");

        PrintOption("Exit Application", "Q");

        CursorTop = 2;

        Content.Invoke();
    }

    public static void PrintOption(string title, string key, bool ltr = false)
    {
        var output = $"[{key}] - {title}";

        if (!ltr)
        {
            CursorLeft = WindowWidth - output.Length;
            output = $"{title} - [{key}]";
        }

        WriteLine(output);
    }

    public static void PrintText(string title, bool ltr = false)
    {
        if (!ltr)
            CursorLeft = WindowWidth - title.Length;

        WriteLine(title);
    }

    public static void PrintError(string message)
    {
        Clear();
        WriteLine(message);
        WriteLine("\nPress any key to continue...");
        ReadKey(true);
    }

    public static void PrintPerson(Person? person)
    {
        if (person is null)
            return;

        CursorLeft = Indent;
        Write("Name:");
        CursorLeft = Indent + 18;
        Positions.Add("Name", (CursorLeft, CursorTop));
        WriteLine(person.Name.Trim().Any() ? person.Name : "[UNDEFINED]");

        CursorLeft = Indent;
        Write("Surname:");
        CursorLeft = Indent + 18;
        Positions.Add("Surname", (CursorLeft, CursorTop));
        WriteLine(person.Surname.Trim().Any() ? person.Surname : "[UNDEFINED]");

        CursorLeft = Indent;
        Write("Phone Number:");
        CursorLeft = Indent + 18;
        Positions.Add("Phone", (CursorLeft, CursorTop));
        WriteLine(person.PhoneNumber ?? "[UNDEFINED]");

        CursorLeft = Indent;
        Write("Email:");
        CursorLeft = Indent + 18;
        Positions.Add("Email", (CursorLeft, CursorTop));
        WriteLine(person.Email ?? "[UNDEFINED]");

        WriteLine();
    }

    public static void PrintData(string title, string? text = "none")
    {
        CursorLeft = Indent;
        WriteLine(title);
        CursorLeft = Indent * 2;
        Positions.Add(title.Replace(':', ' ').Trim(), (CursorLeft, CursorTop));
        if (text is null || text.Trim().Length == 0)
        {
            WriteLine("[N/A]");
            return;
        }

        if (text.Equals("none"))
            return;

        WriteLine(text.Trim());
        WriteLine();
    }

    public static void PrintDataList(string title, List<string>? list)
    {
        PrintData(title);

        list ??= new List<string>();

        list.Sort();

        if (!list.Any())
            WriteLine("None");

        foreach (var item in list)
        {
            if (CursorLeft + item.Length >= WindowWidth * 0.75)
                SetCursorPosition(Indent * 2, CursorTop + 1);

            Write($"{item}");
            if (!item.Equals(list.Last()))
                Write(", ");
            else
                WriteLine();
        }

        WriteLine();
    }
}