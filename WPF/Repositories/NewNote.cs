namespace Notepad.Repositories
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows.Media;

    public class NewNote : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public int id { get; set; }
        public string heading { get; set; }

        public string content { get; set; }


        public string Date { get; set; }
        public Brush finished { get; set; }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Brush Finished;
        private string Heading;
        private string Content;
    }
}
