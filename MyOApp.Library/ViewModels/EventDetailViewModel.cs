using MyOApp.Library.Models; 
using System.Threading.Tasks;

namespace MyOApp.Library.ViewModels
{
    public class EventDetailViewModel : PropertyChangedBase
    {
        Event _model;
        public Event Model
        {
            get { return _model; }
            set { _model = value; }
        }


        public EventDetailViewModel()
        {

        }

        public EventDetailViewModel(Event Event, bool isNew = false)
        {
            _model = Event;
            _isNew = isNew;

            LoadDataModel();
        }

        bool _isNew;
        public bool IsNew { get { return _isNew; } }

        public async void LoadDataModel()
        {
            Name = _model.Name;

            MapViewModel = new MapOverviewViewModel(Model.Map);
            await MapViewModel.LoadMaps();

            IsDirty = false;
            IsReadOnly = !_isNew;
        }

        protected override void RaisePropertyChanged(string property)
        {
            base.RaisePropertyChanged(property);

            switch (property)
            {
                case "IsDirty":
                case "IsReadOnly":
                    return;

                default:
                    IsDirty = true;
                    break;
            }
        }

        public bool IsDirty { get; set; }
        public bool IsReadOnly { get; set; }

        public string Name { get; set; }

        public async Task Save()
        {
            _model.Name = Name;

            await Platform.DataAccess.UpdateEvent(_model);

            _isNew = false;
        }

        public async Task Delete()
        {
            if (!_isNew)
                await Platform.DataAccess.DeleteEvent(_model);
        }



        public MapOverviewViewModel MapViewModel
        {
            get;
            set;
        }
    }
}
