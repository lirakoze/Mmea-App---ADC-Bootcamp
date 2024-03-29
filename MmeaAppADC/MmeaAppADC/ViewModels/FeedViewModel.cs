﻿using MmeaAppADC.Models;
using MmeaAppADC.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MmeaAppADC.ViewModels
{
    public class FeedViewModel : BaseViewModel
    {
        private DBservice _dBservice { get; set; }
        private ObservableCollection<Post> posts;
        public ObservableCollection<Post> Posts
        {
            get { return posts; }
            set { posts = value; OnPropertyChanged(); }
        }
        public Command Refresh { get; set; }
        private bool isRefreshing;

        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set { isRefreshing = value; OnPropertyChanged(); }
        }

        public FeedViewModel()
        {
            _dBservice = new DBservice();
            Refresh = new Command(async () => await RefreshAsync());
            Posts = new ObservableCollection<Post>();
            _ = GetPosts();
            IsRefreshing = false;

        }

        private async Task RefreshAsync()
        {
            Posts.Clear();
            await GetPosts();
            IsRefreshing = false;
        }

        private async Task GetPosts()
        {
            var list = new List<Post>();

            list = await _dBservice.GetPosts();
            foreach (var item in list)
            {
                Posts.Add(item);
            }
        }

    }
}
