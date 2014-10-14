﻿using Espera.Core;
using Espera.Core.Management;
using Espera.Core.Settings;
using System;
using Rareform.Validation;
using ReactiveUI;

namespace Espera.View.ViewModels
{
    internal class SoundCloudViewModel : NetworkSongViewModel<SoundCloudSongViewModel, SoundCloudSong>
    {
        private readonly ViewSettings viewSettings;
        private SortOrder playbacksOrder;
        private SortOrder uploaderOrder;

        public SoundCloudViewModel(Library library, Guid accessToken, CoreSettings coreSettings, ViewSettings viewSettings, INetworkStatus networkStatus = null, INetworkSongFinder<SoundCloudSong> songFinder = null)
            : base(library, accessToken, coreSettings,
                song => new SoundCloudSongViewModel(song), networkStatus, songFinder ?? SoundCloudSongFinder.CachingInstance)
        {
            if (viewSettings == null)
                Throw.ArgumentNullException(() => viewSettings);

            this.viewSettings = viewSettings;

            this.OrderByUploaderCommand = ReactiveCommand.Create();
            this.OrderByUploaderCommand.Subscribe(_ => this.ApplyOrder(SortHelpers.GetOrderByUploader, ref this.uploaderOrder));

            this.OrderByPlaybacksCommand = ReactiveCommand.Create();
            this.OrderByPlaybacksCommand.Subscribe(_ => this.ApplyOrder(SortHelpers.GetOrderByPlaybacks, ref this.playbacksOrder));
        }

        public int DurationColumnWidth
        {
            get { return this.viewSettings.SoundCloudDurationColumnWidth; }
            set { this.viewSettings.SoundCloudDurationColumnWidth = value; }
        }

        public int LinkColumnWidth
        {
            get { return this.viewSettings.SoundCloudLinkColumnWidth; }
            set { this.viewSettings.SoundCloudLinkColumnWidth = value; }
        }

        public ReactiveCommand<object> OrderByPlaybacksCommand { get; private set; }

        public ReactiveCommand<object> OrderByUploaderCommand { get; private set; }

        public int PlaybacksColumnWidth
        {
            get { return this.viewSettings.SoundCloudplaybacksColumnWidth; }
            set { this.viewSettings.SoundCloudplaybacksColumnWidth = value; }
        }

        public int TitleColumnWidth
        {
            get { return this.viewSettings.SoundCloudTitleColumnWidth; }
            set { this.viewSettings.SoundCloudTitleColumnWidth = value; }
        }

        public int UploaderColumnWidth
        {
            get { return this.viewSettings.SoundCloudUploaderColumnWidth; }
            set { this.viewSettings.SoundCloudUploaderColumnWidth = value; }
        }
    }
}