using static LogicLbrary1.Models.Constants;

namespace LogicLbrary1.UserInterface1;

public class CoreTabState
{
    public TabType ActiveTab { get; private set; } = TabType.Play;

    public event Action? OnChange;

    public void SetTab(TabType tab)
    {
        ActiveTab = tab;
        OnChange?.Invoke();
    }
}
