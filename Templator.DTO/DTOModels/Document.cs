using System;
using System.ComponentModel.DataAnnotations.Schema;
using Templator.DTO.DTOModels.Base;
using Templator.DTO.Models;

namespace Templator.DTO.DTOModels
{
    public class Document : Entity
    {
        [NotMapped]
        private bool _isSelected;

        [NotMapped]
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }

        public string FileName { get; set; }

        public Status Status { get; set; } = Status.Unchecked;

        public virtual Template Template { get; set; }

        public DateTime DateTimeInitial { get; set; } = DateTime.Now;

        //Dictionary<string, string>
        public string JSONValues { get; set; }
    }
}
