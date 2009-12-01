using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Controls;
using System.Windows.Input;
using SilverlightMultiUploader.Framework;
using System.Windows;

namespace SilverlightMultiUploader.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly object _baton = new object();
        private readonly NotifyingQueue<FileInfo> _fileQueue;
        private readonly Uri _uploadUri;
        private bool _isUploading;
        private string _uploadButtonText;
        private ICommand _uploadFilesCommand;

        public MainPageViewModel()
        {
            _fileQueue = new NotifyingQueue<FileInfo>();
            _fileQueue.QueueEmpty += (sender, args) => IsUploading = false;
            if (!InitParamsManager.InitParams.ContainsKey("uploadUri"))
                throw new InvalidOperationException("An 'uploadUri' init param is required");
            string uploadAddress = InitParamsManager.InitParams["uploadUri"];

            _uploadUri = new Uri(Application.Current.Host.Source, uploadAddress);
        }
        private void OpenWriteCompleted(object sender, OpenWriteCompletedEventArgs args)
        {
            FileInfo info;
            if (_fileQueue.Count > 0)
                lock (_baton)
                    if (_fileQueue.Count > 0)
                        info = _fileQueue.Dequeue();
                    else
                        return;
            else
                return;
            using (Stream output = args.Result)
            using (FileStream input = info.OpenRead())
                input.WriteTo(output, 4096);
        }
        public ICommand UploadFilesCommand
        {
            get { return _uploadFilesCommand ?? (_uploadFilesCommand = new RelayCommand(x => UploadFiles())); }
        }

        public string UploadButtonText
        {
            get { return _uploadButtonText ?? (_uploadButtonText = "Browse..."); }
            set
            {
                _uploadButtonText = value;
                OnPropertyChanged("UploadButtonText");
            }
        }

        public bool IsUploading
        {
            get { return _isUploading; }
            set
            {
                _isUploading = value;
                OnPropertyChanged("IsUploading");
            }
        }

        private void UploadFiles()
        {
            var ofd = new OpenFileDialog { Multiselect = true, Filter = "All Files|*.*" };
            if (ofd.ShowDialog().GetValueOrDefault())
            {
                IsUploading = true;
                lock (_baton)
                    foreach (FileInfo file in ofd.Files)
                        _fileQueue.Enqueue(file);

                int fileCount = ofd.Files.Count();
                for (int i = 0; i < fileCount; i++)
                    GetWebClient().OpenWriteAsync(_uploadUri, "POST");
            }
        }
        private WebClient GetWebClient()
        {
            var webClient = new WebClient();
            webClient.OpenWriteCompleted += OpenWriteCompleted;
            return webClient;
        }
    }
}