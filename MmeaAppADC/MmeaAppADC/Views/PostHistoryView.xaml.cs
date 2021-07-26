
using MmeaAppADC.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MmeaAppADC.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PostHistoryView : ContentPage
    {
        public Post post { get; set; }
        public PostHistoryView()
        {
            InitializeComponent();
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