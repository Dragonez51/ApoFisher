using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using ApoFisher.ViewModels;

namespace ApoFisher;

public class ViewLocator : IDataTemplate
{
    public Control Build(object? data)
    {
        if (data is null)
            throw new Exception("[ViewLocator] data is null!");
        
        var viewName = data.GetType().FullName!.Replace("ViewModel", "View", StringComparison.InvariantCulture);
        var type = Type.GetType(viewName);

        if (type is null)
            throw new Exception("[ViewLocator] type is null!");
        
        var control = (Control)Activator.CreateInstance(type)!;
        control.DataContext = data;
        return control;
    }

    public bool Match(object? data) => data is ViewModelBase;
}