using System;
using System.Diagnostics;
using ApoFisher.DataBases;
using ApoFisher.Helpers;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.Input;

namespace ApoFisher.ViewModels;

public partial class LakeViewModel : ViewModelBase
{
    public int ViewportWidth { get => 300; }
    public int ViewportHeight { get => 80; }
    public int BouncerWidth { get => 100 / _lakeLvl; }
    public int BouncerHeight { get => ViewportHeight; }
    public int CursorWidth { get => 10; }
    public int CursorHeight { get => ViewportHeight; }
    public int ProgressMax { get; set; }

    private int _bouncerOffset = 0;
    public int BouncerOffset { get => _bouncerOffset; set => SetProperty(ref _bouncerOffset, value); }
    private double _cursorOffset = 0;
    public double CursorOffset { get => _cursorOffset; set => SetProperty(ref _cursorOffset, value); }
    private int _progress = 0;
    public int Progress { get => _progress; set => SetProperty(ref _progress, value); }

    private int _lakeLvl;
    private double _iterator = 0;

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

        ProgressMax = 100*_lakeLvl;

        SetupAnimation();
        SetupRandomizer();
        ResetFishing();
    }

    private void ResetFishing()
    {
        CursorOffset = (ViewportWidth / 2) - (CursorWidth / 2);
        BouncerOffset = 0;
    }

    private void SetupAnimation()
    {
        bool goBack = false;
        _timer.Tick += (sender, e) =>
        {
            if(_iterator < 2) _iterator+=0.05;

            #region Movement

            CursorOffset-=_iterator*_lakeLvl;
            if(CursorOffset < 0) CursorOffset = 0;

            if (goBack)
            {
                if(BouncerOffset <= 0)
                {
                    goBack = false;
                }
                BouncerOffset--;
            }
            else
            {
                if(BouncerOffset >= ViewportWidth - BouncerWidth)
                {
                    goBack = true;
                }
                BouncerOffset++;
            }

            #endregion

            #region Points

            // if cursor rectangle is within green rectangle
            if((CursorOffset >= BouncerOffset) && 
            (CursorOffset + CursorWidth) <= (BouncerOffset + BouncerWidth))
            {
                if(Progress < ProgressMax) Progress++;
            }
            else
            {
                if(Progress > 0) Progress--;
            }

            if(Progress >= ProgressMax)
            {
                Win();
            }

            #endregion
        };
    }

    private void Win()
    {
        _timer.Stop();
        try
        {
            Debug.WriteLine("You Caught a "+DropRandomizer<FishData>.Draw());
        }catch(Exception)
        {
            Debug.WriteLine("You caught rubbish...");
        }
    }

    private void SetupRandomizer()
    {
        DropRandomizer<FishData>.Reset();
        foreach(var fish in ItemsDB.Fishes)
        {
            DropRandomizer<FishData>.AddElement(fish, fish.chance);
        }
        DropRandomizer<FishData>.SetRanges();
    }

    [RelayCommand] public void StartFishing() 
    { 
        _timer.Start(); 
        Progress = 0; 
        ResetFishing(); 
    }

    [RelayCommand] public void BounceFish()
    {
        _iterator = 0;
        var hitStrength = 12;
        if(CursorOffset + hitStrength > (ViewportWidth - CursorWidth))
        {
            CursorOffset = (ViewportWidth - CursorWidth);
            return;
        }
        CursorOffset+=hitStrength;
    }
}