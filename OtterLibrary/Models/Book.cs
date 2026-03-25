using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;

namespace OtterLibrary.Models
{
    public partial class Book:ObservableObject
    {
        public string? ISBN { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Description { get; set; }
        public string? Picture {  get; set; }
        [ObservableProperty]
        private bool _leased;
        [ObservableProperty]
        private User? _leasedTo;
        [ObservableProperty]
        private bool _isEditing;
        public string EditButtonText => IsEditing ? "Save" : "Edit";
        partial void OnIsEditingChanged(bool oldValue, bool newValue)
        {
            OnPropertyChanged(nameof(EditButtonText));
        }


    }
}
