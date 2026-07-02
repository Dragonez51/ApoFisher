using System;
using System.Diagnostics;
using System.Numerics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using Avalonia.Rendering.Composition;
using CommunityToolkit.Mvvm.Input;

namespace ApoFisher.ViewModels;

public partial class LakeViewModel : ViewModelBase
{
    private readonly int VIEWPORT_WIDTH = 300;
    private readonly int VIEWPORT_HEIGHT = 80;

    // private Vector2KeyFrameAnimation _animation;

    private Rectangle _bouncer;

    private Canvas _fishingViewport;
    public Canvas FishingViewport { get => _fishingViewport; set => SetProperty(ref _fishingViewport, value); }

    public LakeViewModel(int lvl)
    {
        Debug.WriteLine("[LakeViewModel] Entered Lake "+lvl);
        
        _fishingViewport = new Canvas();
        _bouncer = new Rectangle();
        _bouncer.Width = VIEWPORT_WIDTH/2;
        _bouncer.Height = VIEWPORT_HEIGHT;
        _bouncer.Fill = Brush.Parse("#0a0");
        _bouncer[Canvas.LeftProperty] = 12.0;

        SetupViewport();
        _fishingViewport.Children.Add(_bouncer);
        SetupAnimation();
    }

    private void SetupViewport()
    {
        _fishingViewport.Width = VIEWPORT_WIDTH;
        _fishingViewport.Height = VIEWPORT_HEIGHT;
        
        _fishingViewport.Background = Brush.Parse("#400");
    }

    private void SetupAnimation()
    {
        var visual = ElementComposition.GetElementVisual(new StackPanel());
        // var visual = ElementComposition.GetElementVisual(_bouncer);
        // if(visual is null) throw new Exception("[LakeViewModel](SetupAnimation) visual is null!");
        var compositor = visual?.Compositor;

        var animation = compositor?.CreateVector3DKeyFrameAnimation();
        animation?.Duration = TimeSpan.FromSeconds(2);
        animation?.InsertKeyFrame(0f, new Vector3D(0, 0, 0));
        animation?.InsertKeyFrame(1f, new Vector3D(VIEWPORT_WIDTH-(VIEWPORT_WIDTH/2), 0, 0));
        
        visual?.StartAnimation("Offset", animation);
    }

    [RelayCommand] public void StartFishing()
    {
        Debug.WriteLine("Fishing...");
        // ElementComposition.GetElementVisual(_bouncer)?.StartAnimation("Offset", _animation);
    }
}