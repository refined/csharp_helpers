using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using CsharpHelpers.Property;

namespace CsharpHelpers.UI
{
    public class PagingViewModel<T> : NotifyPropertyChanged
    {
        public int ItemsPerPage { get; set; } = 15;
        public ObservableCollection<T> PageCollection { get; } = new ObservableCollection<T>();
        private List<T> _allCollection = new List<T>();

        private bool _isLoading;
        private int _currentPage = 1;
        private int _collectionSize;


        public int CollectionSize
        {
            get { return _collectionSize; }
            private set
            {
                _collectionSize = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(PagesCount));
            }
        }

        public int PagesCount => CollectionSize / ItemsPerPage + 1;

        public bool IsLoading
        {
            get { return _isLoading; }
            protected set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public int CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsNextButtonEnabled));
                OnPropertyChanged(nameof(IsPreviousButtonEnabled));
                OnPropertyChanged("PagesString");
            }
        }

        public bool IsNextButtonEnabled => _currentPage * ItemsPerPage < CollectionSize;
        public bool IsPreviousButtonEnabled => _currentPage != 1;
        public string PagesString => "Страница " + CurrentPage + " из " + PagesCount;

        private readonly Dispatcher _dispatcher = Dispatcher.CurrentDispatcher;

        public virtual async Task Update(IEnumerable<T> collection, CancellationToken token = default(CancellationToken))
        {
            _allCollection = collection.ToList();
            CollectionSize = _allCollection.Count;
            CurrentPage = 1;

            await Update(token);
        }

        private async Task Update(CancellationToken token = default(CancellationToken))
        {
            if (IsLoading) return;
            IsLoading = true;

            try
            {
                await _dispatcher.InvokeAsync(() =>
                {
                    PageCollection.Clear();

                    foreach (var item in _allCollection.Skip((CurrentPage - 1) * ItemsPerPage).Take(ItemsPerPage))
                    {
                        if (token.IsCancellationRequested) return;
                        PageCollection.Add(item);
                    }
                });
            }
            finally
            {
                IsLoading = false;
            }
        }

        public ICommand MoveNextCommand => new Command(MoveNext);

        public ICommand MovePreviousCommand => new Command(MovePrevious);
        //public ICommand MoveToPageCommand => new Command(MoveNext);

        public async void MoveNext()
        {
            CurrentPage++;
            await Update();
        }

        public async void MovePrevious()
        {
            CurrentPage--;
            await Update();
        }

        public async void MoveToPage(int pageNum)
        {
            CurrentPage = pageNum;
            await Update();
        }
    }
}
