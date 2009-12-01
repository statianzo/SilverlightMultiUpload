using System.ComponentModel;

namespace SilverlightMultiUploader.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        private string _displayName;

        public virtual string DisplayName
        {
            get { return _displayName; }
            set
            {
                _displayName = value;
                OnPropertyChanged("DisplayName");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                var args = new PropertyChangedEventArgs(propertyName);
                PropertyChanged(this, args);
            }
        }
    }
}