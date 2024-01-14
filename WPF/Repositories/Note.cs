namespace Notepad.Repositories
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Security.Policy;
    using System.Windows.Media;

    public class Note : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public int Id { get; set; }
        public string Heading
        {
            get => this.heading;
            set
            {
                this.heading = value;
                this.OnPropertyChanged(nameof(Heading));
            }

        }
        public string Content
        {
            get => this.content;
            set
            {
                this.content = value;
                this.OnPropertyChanged(nameof(Content));
            }
        }

        public string finished { get; set; }
        public Brush Color
        {
            get => color;
            set
            {

                color = value;
                this.OnPropertyChanged(nameof(Color));

            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Brush color;
        private string content;
        private string heading;
    }
}
