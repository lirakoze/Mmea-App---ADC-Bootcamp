using MmeaAppADC.Models;

namespace MmeaAppADC.ViewModels
{
    public class PostDetailViewModel : BaseViewModel
    {
        private Post post;

        public Post Post
        {
            get { return post; }
            set { post = value; OnPropertyChanged(); }
        }
        public PostDetailViewModel(Post p)
        {
            Post = p;
        }
        public PostDetailViewModel()
        {

        }

    }
}
