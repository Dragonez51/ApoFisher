using System;
using System.Diagnostics;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.Input;

namespace ApoFisher.ViewModels;

public partial class LakeViewModel : ViewModelBase
{
    public int ViewportWidth { get => 300; }
    public int ViewportHeight { get => 80; }
    public int BouncerWidth { get => ViewportWidth/2; }
    public int BouncerHeight { get => ViewportHeight; }
    public int CursorWidth { get => 25; }
    public int CursorHeight { get => ViewportHeight; }

    private int _bouncerOffset = 0;
    public int BouncerOffset { get => _bouncerOffset; set => SetProperty(ref _bouncerOffset, value); }
    private int _cursorOffset = 0;
    public int CursorOffset { get => _cursorOffset; set => SetProperty(ref _cursorOffset, value); }

    private int _lakeLvl;

    private TimeSpan _framerate = TimeSpan.FromSeconds(1 / 60.0);
    private DispatcherTimer _timer;

    public LakeViewModel(int lvl)
    {
        Debug.WriteLine("[LakeViewModel] Entered Lake "+lvl);
        _lakeLvl = lvl;

        _timer = new DispatcherTimer
        {
            Interval = _framerate
        };

        SetupAnimation();
    }

    private void SetupAnimation()
    {
        bool goBack = false;
        _timer.Tick += (sender, e) =>
        {
            CursorOffset-=_lakeLvl;
            if(CursorOffset < 0 ) CursorOffset = 0;

            if (goBack)
            {
                if(BouncerOffset <= 0)
                {
                    goBack = false;
                }
                BouncerOffset--;
                return;
            }

            if(BouncerOffset >= ViewportWidth - BouncerWidth)
            {
                goBack = true;
            }
            BouncerOffset++;
        };
    }

    [RelayCommand] public void StartFishing() => _timer.Start();
    [RelayCommand] public void BounceFish()
    {
        var hitStrength = CursorWidth;
        if(CursorOffset + hitStrength > (ViewportWidth - CursorWidth))
        {
            CursorOffset = (ViewportWidth - CursorWidth);
            return;
        }
        CursorOffset+=CursorWidth;
    }
}