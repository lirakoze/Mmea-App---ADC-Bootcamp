﻿
using MmeaAppADC.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MmeaAppADC.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FeedView : ContentView
    {
        public Post post { get; set; }
        public FeedView()
        {
            InitializeComponent();
            BindingContext = new ViewModels.FeedViewModel();
        }

        private void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            Navigation.PushModalAsync(new UserPostView());
        }

        private void PostsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var posts = e.CurrentSelection;
            for (int i = 0; i < posts.Count; i++)
            {
                post = posts[i] as Post;
            }
            Navigation.PushModalAsync(new PostDetailView(post));
        }
    }
}